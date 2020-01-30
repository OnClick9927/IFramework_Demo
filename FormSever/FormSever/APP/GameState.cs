using FormSever.Net;
using IFramework;

namespace FormSever
{
    public class GameState : IAPPState
    {
        public static NetSever netSever { get; private set; }

        public void OnEnter()
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

        public void OnExit()
        {
            netSever?.Dispose();
        }

        public void Update()
        {
        }
    }

}
