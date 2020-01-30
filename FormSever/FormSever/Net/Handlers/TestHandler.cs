using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFramework.Net;

namespace FormSever.Net.Handlers
{
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
        }

        protected override void OnUdpMessage(SocketToken token, INetMessage message)
        {
        }
    }
}
