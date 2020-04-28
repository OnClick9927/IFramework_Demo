using FormSever.Net;
using IFramework;
using IFramework.Modules;
using IFramework.NodeAction;
namespace FormSever
{
    class AppMoudle : FrameworkAppModule
    {
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
            }
            net.Run();
        }

        public Data.Datas datas { get; private set; }
        public Game.GameMap gameMap { get; private set; }

        private void InitDatas()
        {
            datas = new Data.Datas();
            gameMap = new Game.GameMap();
            datas.Load();
            gameMap.Load();
        }
        protected override void Awake()
        {
            InitNet();
            InitDatas();

           

        }

        protected override void OnDispose()
        {
            datas.Save();
            gameMap.Save();
            net?.Dispose();
        }

        protected override void OnUpdate()
        {
        }
    }
}
