using IFramework.Net;
using IFramework;
namespace FormSever.Net
{
    interface INetMessageHangdler
    {
        void OnTcpMessage(SocketToken token, INetMessage message);
        void OnUdpMessage(SocketToken token, INetMessage message);
        void OnTcpConn(SocketToken token);
        void OnTcpDisConn(SocketToken token);

    }
    abstract class NetMessageHandler: INetMessageHangdler
    {
        protected NetSever netSever { get { return (Framework.env0.modules.App as AppMoudle).netSever; } }
        protected abstract void OnTcpMessage(SocketToken token, INetMessage message);
        protected abstract void OnUdpMessage(SocketToken token, INetMessage message);
        protected abstract void OnTcpConn(SocketToken token);
        protected abstract void OnTcpDisConn(SocketToken token);

        void INetMessageHangdler.OnTcpMessage(SocketToken token, INetMessage message)
        {
            OnTcpMessage(token, message);
        }

        void INetMessageHangdler.OnUdpMessage(SocketToken token, INetMessage message)
        {
            OnUdpMessage(token, message);
        }

        void INetMessageHangdler.OnTcpConn(SocketToken token)
        {
            OnTcpConn(token);
        }

        void INetMessageHangdler.OnTcpDisConn(SocketToken token)
        {
            OnTcpDisConn(token);
        }
    }

}
