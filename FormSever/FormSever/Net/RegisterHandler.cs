using IFramework.Net;
using IFramework;
namespace FormSever.Net
{
    [NetMessage(4)]
    class RegisterRequest : INetMessage
    {
        public string account;
        public string psd;
        public string name;
    }
    [NetMessage(5)]
    class RegisterResponse : INetMessage
    {
        public string account;
        public string psd;
        public string name;
        public bool sucess;
        public string err;
    }
    class RegisterHandler : NetMessageHandler
    {
        protected override void OnTcpConn(SocketToken token) { }

        protected override void OnTcpDisConn(SocketToken token) { }

        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {
            if (message is RegisterRequest)
            {
                RegisterRequest req = message as RegisterRequest;
                string err;
                bool bo = APP.datas.users.AddUser(req.account, req.name, req.psd, out err);
                Log.L(string.Format("User Register  {0}  {1}  {2}  {3}", req.account, req.name, req.psd, err));
                net.SendTcpMessage(token, new RegisterResponse()
                {
                    account = req.account,
                    psd = req.psd,
                    name = req.name,
                    sucess = bo,
                    err = err
                });
            }
        }

        protected override void OnUdpMessage(SocketToken token, INetMessage message) { }
    }
}
