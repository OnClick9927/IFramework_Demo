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

            tcpTokenPool = new NetConnectionTokenPool(Configs.net.TCPCheckSpace);
            tcpTokenPool.ConnectionTimeout = Configs.net.TCPConnTimeOut;
            udpTokenPool = new NetConnectionTokenPool(Configs.net.UDPCheckSpace);
            udpTokenPool.ConnectionTimeout = Configs.net.UDPConnTimeOut;

            tcp = new TcpServerToken(Configs.net.TCPMaxConn, Configs.net.TCPBuffersize);
            tcp.onReceive = OnTcpRec;
            tcp.onAcccept = OnTcpAccept;
            tcp.onDisConnect = OnTcpDisConnect;

            udp = new UdpServerToken(Configs.net.UDPMaxConn, Configs.net.UDPBuffersize);
            udp.onReceive = OnUdpRec;

            tcpPkgReader = new PacketReader(Configs.net.TCPBuffersize * 2);
            udpPkgReader = new PacketReader(Configs.net.UDPBuffersize * 2);

        }
        public void Run()
        {
            tcp.Start(Configs.net.TCPPort);
            udp.Start(Configs.net.UDPPort);
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
            Framework.env0.modules.Loom.RunOnMainThread(() => {
                Log.E(token.endPoint.Address);
            });
            var tok= udpTokenPool.GetTokenBySocketToken(token);
            if (tok == null)
                udpTokenPool.AddToken(new NetConnectionToken(token));
            udpPkgReader.Set(seg.buffer, seg.offset, seg.count);
            var pkgs = udpPkgReader.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.pkgID), encoding.GetString(p.message)) as INetMessage;
                    Framework.env0.modules.Loom.RunOnMainThread(() =>
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
            Framework.env0.modules.Loom.RunOnMainThread(() => {
                Log.L(string.Format("One Tcp Connect  {0}", token.endPoint.Address));
                onTcpConn?.Invoke(token);
            });
        }
        private void OnTcpDisConnect(SocketToken token)
        {
            tcpTokenPool.RemoveToken(token);
            Framework.env0.modules.Loom.RunOnMainThread(() => {
                Log.L(string .Format("One Tcp DisConnect  {0}", token.endPoint.Address));
                onTcpDisconn?.Invoke(token);
            });
        }

        private void OnTcpRec(SocketToken token, BufferSegment seg)
        {
            tcpTokenPool.RefreshConnectionToken(token);
            tcpPkgReader.Set(seg.buffer, seg.offset, seg.count);
            var pkgs = tcpPkgReader.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.pkgID), encoding.GetString(p.message)) as INetMessage;
                    Framework.env0.modules.Loom.RunOnMainThread(() =>
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
            udp.SendAsync(NetMessageToBufferSegment(message), token.endPoint);
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
