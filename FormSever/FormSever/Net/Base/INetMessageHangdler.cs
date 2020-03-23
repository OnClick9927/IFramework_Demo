using IFramework.Net;
using IFramework;
namespace FormSever.Net
{
    interface INetMessageHandler
    {
        void OnTcpMessage(SocketToken token, INetMessage message);
        void OnTcpConn(SocketToken token);
        void OnTcpDisConn(SocketToken token);

    }
    abstract class NetMessageHandler: INetMessageHandler
    {
        protected NetSever net { get { return (Framework.env0.modules.App as AppMoudle).net; } }
        protected abstract void OnTcpMessage(SocketToken token, INetMessage message);
        protected abstract void OnTcpConn(SocketToken token);
        protected abstract void OnTcpDisConn(SocketToken token);

        void INetMessageHandler.OnTcpMessage(SocketToken token, INetMessage message)
        {
            OnTcpMessage(token, message);
        }

      

        void INetMessageHandler.OnTcpConn(SocketToken token)
        {
            OnTcpConn(token);
        }

        void INetMessageHandler.OnTcpDisConn(SocketToken token)
        {
            OnTcpDisConn(token);
        }
    }
  
  
}
