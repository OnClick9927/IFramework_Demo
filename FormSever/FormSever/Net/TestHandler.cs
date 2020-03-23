using IFramework.Net;
using IFramework;
namespace FormSever.Net
{
    [NetMessage(1)]
    class TestMessage : INetMessage
    {
        public int index = 666;

    }
    class TestHandler : NetMessageHandler
    {
        protected override void OnTcpConn(SocketToken token)
        {
           
        }

        protected override void OnTcpDisConn(SocketToken token)
        {
        }

        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {
            //Log.L((message as TestMessage).index);
            //net.SendTcpMessage(token, message);
        }


    }



   
}
