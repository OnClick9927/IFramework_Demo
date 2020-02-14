/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using System;
using System.Text;
using IFramework;
using IFramework.Net;
using IFramework.Serialization;

namespace IFramework_Demo
{
    public class NetClient:IDisposable
	{
        PacketReader tcpPkgReader;
        PacketReader udpPkgReader;

        Encoding encoding = Encoding.UTF8;

        public TcpClientToken tcp;
        public UdpClientToken udp;
        public event Action<SocketToken, INetMessage> onTcpMessage;
        public event Action<SocketToken, INetMessage> onUdpMessage;
        public event Action onTcpConn, onTcpDisConn;
        public NetClient ()
        {
            tcpPkgReader = new PacketReader(NetConfig.TCPBuffersize * 2);
            udpPkgReader = new PacketReader(NetConfig.UDPBuffersize * 2);
            
            tcp = new TcpClientToken(NetConfig.TCPBuffersize, NetConfig.BufferNumber);
            udp = new UdpClientToken(NetConfig.UDPBuffersize);
            tcp.onConnect = OnTCPConnect;
            tcp.onDisConnect += OnTCPDisConnect;
            tcp.onReceive += OnTCPRec;
            udp.onReceive += OnUDPRec;
        }

        private void OnUDPRec(SocketToken token, BufferSegment seg)
        {
            udpPkgReader.Set(seg.buffer, seg.offset, seg.count);
            var pkgs = udpPkgReader.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.pkgID), encoding.GetString(p.message)) as INetMessage;
                    Framework.env1.modules.Loom.RunOnMainThread(() =>
                    {
                        onUdpMessage?.Invoke(token, msg);
                    });
                });
            }
        }
        private void OnTCPRec(SocketToken token, BufferSegment seg)
        {
            tcpPkgReader.Set(seg.buffer, seg.offset, seg.count);
            var pkgs = tcpPkgReader.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.pkgID), encoding.GetString(p.message)) as INetMessage;
                    Framework.env1.modules.Loom.RunOnMainThread(() =>
                    {
                        onTcpMessage?.Invoke(token, msg);
                    });

                });
            }
        }

        private void OnTCPDisConnect(SocketToken token)
        {
            Framework.env1.modules.Loom.RunOnMainThread(() =>
            {
                onTcpDisConn?.Invoke();
            });
            
        }
        private void OnTCPConnect(SocketToken token, bool connected)
        {
            Framework.env1.modules.Loom.RunOnMainThread(() =>
            {
                onTcpConn?.Invoke();
            });
           
        }

        public void Run()
        {
            tcp.ConnectAsync(NetConfig.TCPPort, NetConfig.TCPIP);
            udp.Connect(NetConfig.UDPPort, NetConfig.UDPIP);
        }
        private BufferSegment NetMessageToBufferSegment(INetMessage message)
        {
            var bytes = encoding.GetBytes(Json.ToJsonString(message));
            Packet pac = new Packet(1, NetMessageTool.GetIDByType(message.GetType()), 1, bytes);
            bytes = pac.Pack();
            return new BufferSegment(bytes);
        }

        public void SendTcpMessage(INetMessage message)
        {
            tcp.SendAsync(NetMessageToBufferSegment(message));
        }
        public void SendUdpMessage(INetMessage message)
        {
            udp.Send(NetMessageToBufferSegment(message));
        }
        public void Dispose()
        {
            try
            {
                tcp.DisConnect();
                tcp.Dispose();
                udp.Disconnect();
                udp.Dispose();
            }
            catch (Exception)
            {

            }
           
        }
    }
}
