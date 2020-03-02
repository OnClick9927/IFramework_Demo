using IFramework.Net;
namespace FormSever.Net
{
    [NetMessage(2)]
    class LoginRequest : INetMessage
    {
        public string account;
        public string psd;
    }
    [NetMessage(3)]
    class LoginResponse : INetMessage
    {
        public string account;
        public string psd;
        public string name;
        public bool sucess;
        public string err;
    }
    class LoginHandler : NetMessageHandler
    {
        protected override void OnTcpConn(SocketToken token)
        {
        }
        protected override void OnTcpDisConn(SocketToken token)
        {
        }

        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {
            if (message is LoginRequest)
            {
                LoginRequest req = message as LoginRequest;
                string err;
                string name;
                bool bo = APP.datas.users.TryLogin(req.account, req.psd,out name,out err);
                net.SendTcpMessage(token, new LoginResponse()
                {
                    account = req.account,
                    psd = req.psd,
                    name = name,
                    sucess = bo,
                    err = err

                });
            }

        }



        protected override void OnUdpMessage(SocketToken token, INetMessage message)
        {
        }
    }
}
