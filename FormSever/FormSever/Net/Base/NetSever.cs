using System;
using IFramework.Serialization;
using IFramework.Net;
using IFramework;
using System.Text;
using IFramework.Packets;
using System.Collections.Generic;
namespace FormSever.Net
{
    public class NetSever:IDisposable
    {
        private NetConnectionTokenPool tcpTokenPool;
        private NetConnectionTokenPool udpTokenPool;

        private TcpServerToken tcp;
        private UdpServerToken udp;

        private Dictionary<SocketToken, PacketReader> _tcpPkgs;
        private Dictionary<SocketToken, PacketReader> _udpPkgs;

        private Encoding encoding = Encoding.UTF8;

        public event Action<SocketToken> onTcpConn;
        public event Action<SocketToken> onTcpDisconn;

        public event Action<SocketToken, INetMessage> onTcpMessage;
        public event Action<SocketToken, INetMessage> onUdpMessage;

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

            _tcpPkgs = new Dictionary<SocketToken, PacketReader>();
            _udpPkgs = new Dictionary<SocketToken, PacketReader>();
           

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
            _tcpPkgs.Clear();
            _udpPkgs.Clear();
        }



        private void OnUdpRec(SocketToken token, BufferSegment seg)
        {

            var tok= udpTokenPool.GetTokenBySocketToken(token);
            if (tok == null)
                udpTokenPool.AddToken(new NetConnectionToken(token));

            PacketReader pr;
            if (!_udpPkgs.TryGetValue(token, out pr))
            {
                pr = new PacketReader(Configs.net.UDPBuffersize);
                _udpPkgs.Add(token, pr);
            }


            pr.Set(seg.buffer, seg.offset, seg.count);
            var pkgs = pr.Get();
            if (pkgs != null)
            {
                pkgs.ForEach((p) =>
                {
                    INetMessage msg = Json.ToObject(NetMessageTool.GetTypeByID(p.pkgID), encoding.GetString(p.message)) as INetMessage;
                    APP.env.modules.Loom.RunOnMainThread(() =>
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
            APP.env.modules.Loom.RunOnMainThread(() => {
                Log.L(string.Format("One Tcp Connect  {0}", token.endPoint.Address));
                onTcpConn?.Invoke(token);
            });
        }
        private void OnTcpDisConnect(SocketToken token)
        {
            tcpTokenPool.RemoveToken(token);
            APP.env.modules.Loom.RunOnMainThread(() => {
                Log.L(string .Format("One Tcp DisConnect  {0}", token.endPoint.Address));
                onTcpDisconn?.Invoke(token);
            });
        }
        private void OnTcpRec(SocketToken token, BufferSegment seg)
        {
            tcpTokenPool.RefreshConnectionToken(token);
            PacketReader pr;
            if (!_tcpPkgs.TryGetValue(token,out pr))
            {
                pr = new PacketReader(Configs.net.TCPBuffersize);
                _tcpPkgs.Add(token, pr);
            }

            pr.Set(seg.buffer, seg.offset, seg.count);

         
            var pkgs = pr.Get();
            if (pkgs != null)
            {
                for (int i = 0; i < pkgs.Count; i++)
                {
                    var p = pkgs[i];
                    Type type = NetMessageTool.GetTypeByID(p.pkgID);
                    string msgstr = encoding.GetString(p.message);
                    INetMessage msg = Json.ToObject(type, msgstr) as INetMessage;
                    APP.env.modules.Loom.RunOnMainThread(() =>
                    {
                        if (onTcpMessage != null)
                        {
                            onTcpMessage(token, msg);
                        }
                    });
                }
               
            }
        }



        private BufferSegment NetMessageToBufferSegment(INetMessage message)
        {
            var bytes = encoding.GetBytes(Json.ToJsonString(message));
            Packet pac = new Packet(1, NetMessageTool.GetIDByType(message.GetType()), 1, bytes);
            var  buffer = pac.Pack();
            return new BufferSegment(buffer, buffer.Length);
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
                SendUdpMessage(item.Token, message);
            }
        }
    }
}
