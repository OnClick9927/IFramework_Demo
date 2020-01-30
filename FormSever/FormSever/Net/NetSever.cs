using System;
using IFramework.Serialization;
using IFramework.Net;
using IFramework;
using System.Text;
namespace FormSever.Net
{
    public class NetSever:IDisposable
    {
        NetConnectionTokenPool tcpTokenPool;
        NetConnectionTokenPool udpTokenPool;

        TcpServerToken tcp;
        UdpServerToken udp;

        PacketReader tcpPkgReader;
        PacketReader udpPkgReader;

        Encoding encoding = Encoding.UTF8;


        public NetSever()
        {

            tcpTokenPool = new NetConnectionTokenPool(NetConfig.TCPCheckSpace);
            tcpTokenPool.ConnectionTimeout = NetConfig.TCPConnTimeOut;
            udpTokenPool = new NetConnectionTokenPool(NetConfig.UDPCheckSpace);
            udpTokenPool.ConnectionTimeout = NetConfig.UDPConnTimeOut;

            tcp = new TcpServerToken(NetConfig.TCPMaxConn, NetConfig.TCPBuffersize);
            tcp.onReceive = OnTcpRec;
            tcp.onAcccept = OnTcpAccept;
            tcp.onDisConnect = OnTcpDisConnect;

            udp = new UdpServerToken(NetConfig.UDPMaxConn, NetConfig.UDPBuffersize);
            udp.onReceive = OnUdpRec;

            tcpPkgReader = new PacketReader(NetConfig.TCPBuffersize * 2);
            udpPkgReader = new PacketReader(NetConfig.UDPBuffersize * 2);

        }
        public void Run()
        {
            tcp.Start(NetConfig.TCPPort);
            udp.Start(NetConfig.UDPPort);
        }
        public void Dispose()
        {
            tcp.Stop();
            tcp.Dispose();
            udp.Stop();
            udp.Dispose();
        }


        public event Action<SocketToken> onTcpConn;
        public event Action<SocketToken> onTcpDisconn;

        public event Action<SocketToken, INetMessage> onTcpMessage;
        public event Action<SocketToken, INetMessage> onUdpMessage;

        private void OnUdpRec(SocketToken token, BufferSegment seg)
        {
            Framework.moudles.Loom.RunOnMainThread(() => {
                Log.E(token.EndPoint.Address);
            });
            var tok= udpTokenPool.GetTokenBySocketToken(token);
            if (tok == null)
                udpTokenPool.AddToken(new NetConnectionToken(token));
            udpPkgReader.Set(seg.Buffer, seg.Offset, seg.Len);
            var pkgs = udpPkgReader.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.ID), encoding.GetString(p.MsgBuff)) as INetMessage;
                    Framework.moudles.Loom.RunOnMainThread(() =>
                    {
                        if (onUdpMessage != null)
                        {
                            onUdpMessage(token, msg);
                        }
                    });
                });
            }
        }


        private void OnTcpAccept(SocketToken token)
        {
            tcpTokenPool.AddToken(new NetConnectionToken(token));
            Framework.moudles.Loom.RunOnMainThread(() => {
                Log.E(string.Format("One Tcp DisConnent  {0}", token.EndPoint.Address));
                onTcpConn?.Invoke(token);
            });
        }
        private void OnTcpDisConnect(SocketToken token)
        {
            tcpTokenPool.RemoveToken(token);
            Framework.moudles.Loom.RunOnMainThread(() => {
                Log.E(string .Format("One Tcp Connent  {0}", token.EndPoint.Address));
                onTcpDisconn?.Invoke(token);
            });
        }

        private void OnTcpRec(SocketToken token, BufferSegment seg)
        {
            tcpTokenPool.RefreshConnectionToken(token);
            tcpPkgReader.Set(seg.Buffer, seg.Offset, seg.Len);
            var pkgs = tcpPkgReader.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.ID), encoding.GetString(p.MsgBuff)) as INetMessage;
                    Framework.moudles.Loom.RunOnMainThread(() =>
                    {
                        if (onTcpMessage != null)
                        {
                            onTcpMessage(token, msg);
                        }
                    });

                });
            }
        }



        private BufferSegment NetMessageToBufferSegment(INetMessage message)
        {
            var bytes = encoding.GetBytes(Json.ToJsonString(message));
            Packet pac = new Packet(1, NetMessageTool.GetIDByType(message.GetType()), 1, bytes);
            bytes = pac.Pack();
            return new BufferSegment(bytes);
        }
        public void SendTcpMessage(SocketToken token, INetMessage message)
        {
            tcp.SendAsync(token, NetMessageToBufferSegment(message));
        }
        public void SendUdpMessage(SocketToken token, INetMessage message)
        {
            udp.Send(NetMessageToBufferSegment(message), token.EndPoint);
        }
        public void SendTcpMessageToAll(INetMessage message)
        {
            var ie = tcpTokenPool.ReadNext();
            foreach (var item in ie)
            {
                SendTcpMessage(item.Token, message);
            }
        }
        public void SendUdpMessageToAll(INetMessage message)
        {
            var ie = udpTokenPool.ReadNext();
            foreach (var item in ie)
            {
                SendTcpMessage(item.Token, message);
            }
        }
    }
}
