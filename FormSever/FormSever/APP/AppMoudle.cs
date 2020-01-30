using FormSever.Net;
using IFramework;
using IFramework.Moudles.APP;
namespace FormSever
{
    class AppMoudle : FrameworkAppMoudle
    {
        protected override bool needUpdate { get { return true; } }
        public NetSever netSever { get; private set; }
        private void InitNet()
        {
            netSever = new NetSever();
            var em = NetMessageHandleTool.TcpFunc.GetEnumerator();
            while (em.MoveNext())
                netSever.onTcpMessage += em.Current;
            em = NetMessageHandleTool.UdpFunc.GetEnumerator();
            while (em.MoveNext())
                netSever.onUdpMessage += em.Current;

            var em1 = NetMessageHandleTool.TcpConn.GetEnumerator();
            while (em.MoveNext())
                netSever.onTcpConn += em1.Current;

            em1 = NetMessageHandleTool.TcpDisConn.GetEnumerator();
            while (em.MoveNext())
                netSever.onTcpDisconn += em1.Current;

            Framework.Container.RegisterInstance(netSever);
            netSever.Run();
            Log.L("APP Run");
        }
        protected override void Awake()
        {
            InitNet();
        }

        protected override void OnDispose()
        {
            netSever?.Dispose();
        }

        protected override void OnUpdate()
        {
        }
    }
}
