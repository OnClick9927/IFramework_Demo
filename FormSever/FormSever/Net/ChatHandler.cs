using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFramework;
using IFramework.Net;

namespace FormSever.Net
{
    [NetMessage(12)]
    class ChatBroadCast : INetMessage
    {
        public string acc;
        public string name;
        public string message;
    }
    class ChatHandler : NetMessageHandler
    {
        protected override void OnTcpConn(SocketToken token)
        {
            
        }

        protected override void OnTcpDisConn(SocketToken token)
        {
           
        }

        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {
            if (message is ChatBroadCast)
            {
                ChatBroadCast c = message as ChatBroadCast;
                net.SendTcpMessageToAll(message);
                Log.L(string.Format("Chat {0}/{1}   {2}  ", c.acc, c.name,c.message));

            }
        }
    }
}
