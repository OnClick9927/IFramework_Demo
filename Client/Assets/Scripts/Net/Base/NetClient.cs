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
using IFramework.Packets;
using IFramework.Serialization;

namespace IFramework_Demo
{
    public class NetClient:IDisposable
	{
        PacketReader tcpPkgReader;

        Encoding encoding = Encoding.UTF8;

        public TcpClientToken tcp;
        public event Action<SocketToken, INetMessage> onTcpMessage;
        public event Action onTcpConn, onTcpDisConn;
        public NetClient ()
        {
            tcpPkgReader = new PacketReader(NetConfig.TCPBuffersize * 4096);
            
            tcp = new TcpClientToken(NetConfig.TCPBuffersize, NetConfig.BufferNumber);
            tcp.onConnect = OnTCPConnect;
            tcp.onDisConnect += OnTCPDisConnect;
            tcp.onReceive += OnTCPRec;
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
        }
        private BufferSegment NetMessageToBufferSegment(INetMessage message)
        {
            var bytes = encoding.GetBytes(Json.ToJsonString(message));
            Packet pac = new Packet(1, NetMessageTool.GetIDByType(message.GetType()), 1, bytes);
            bytes = pac.Pack();
            return new BufferSegment(bytes,0,bytes.Length);
        }

        public void SendTcpMessage(INetMessage message)
        {
            tcp.SendAsync(NetMessageToBufferSegment(message));
        }
       
        public void Dispose()
        {
            try
            {
                tcp.DisConnect();
                tcp.Dispose();
            }
            catch (Exception)
            {

            }
           
        }
    }
}
