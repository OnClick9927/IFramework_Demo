using FormSever.Net;
using IFramework;
using IFramework.Modules;
namespace FormSever
{
    class AppMoudle : FrameworkAppModule
    {
        protected override bool needUpdate { get { return true; } }
        public NetSever net { get; private set; }
        private void InitNet()
        {
            net = new NetSever();
            var Hs = NetMessageHandleTool.Handlers;

            for (int i = 0; i < Hs.Count; i++)
            {
                net.onTcpMessage += Hs[i].OnTcpMessage;
                net.onTcpConn += Hs[i].OnTcpConn;
                net.onTcpDisconn += Hs[i].OnTcpDisConn;
                net.onUdpMessage += Hs[i].OnUdpMessage;
            }
            net.Run();
        }

        public Data.Datas datas { get; private set; }

        private void InitDatas()
        {
            datas = new Data.Datas();
            datas.Load();
        }
        protected override void Awake()
        {
            InitNet();
            InitDatas();
        }

        protected override void OnDispose()
        {
            net?.Dispose();
        }

        protected override void OnUpdate()
        {
        }
    }
}
