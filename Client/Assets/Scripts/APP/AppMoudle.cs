/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-02-07
 *Description:    Description
 *History:        2020-02-07--
*********************************************************************************/
using IFramework;
using IFramework.Moudles.APP;

namespace IFramework_Demo
{
    public class AppMoudle : FrameworkAppMoudle
    {
        protected override bool needUpdate { get { return true; } }
        protected override void Awake()
        {
            InitNet();
        }
        public NetClient netClient;
        private void InitNet()
        {
            netClient = new NetClient();
            var em = NetMessageHandleTool.TcpFunc.GetEnumerator();
            while (em.MoveNext())
                netClient.onTcpMessage += em.Current;
            em = NetMessageHandleTool.UdpFunc.GetEnumerator();
            while (em.MoveNext())
                netClient.onUdpMessage += em.Current;
            var em1 = NetMessageHandleTool.TcpConn.GetEnumerator();
            while (em.MoveNext())
                netClient.onTcpConn += em1.Current;
            em1 = NetMessageHandleTool.TcpDisConn.GetEnumerator();
            while (em.MoveNext())
                netClient.onTcpDisConn += em1.Current;
            netClient.onTcpDisConn += NetClient_onTcpDisConn;
            netClient.onTcpConn += NetClient_onTcpConn;
            netClient.Run();
            void NetClient_onTcpConn()
            {

            }

            void NetClient_onTcpDisConn()
            {
                netClient.Run();
            }
        }

       

        protected override void OnDispose()
        {
            netClient.Dispose();
        }

        protected override void OnUpdate()
        {
            Log.L(netClient.tcp.IsConnected);
        }
    }
}
