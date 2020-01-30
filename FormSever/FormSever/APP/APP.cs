using IFramework;
using IFramework.Moudles.Fsm;
using IFramework.Moudles.Loom;
using IFramework.Moudles.Message;
using IFramework.Moudles.Coroutine;
using FormSever.Net;
using System.Collections;

namespace FormSever
{
    partial class APP
    {
        private static void InitFrameworkMoudles()
        {
            Framework.moudles.Loom = Framework.moudles.CreateMoudle<LoomMoudle>();
            Framework.moudles.Message = Framework.moudles.CreateMoudle<MessageMoudle>();
            Framework.moudles.Coroutine= Framework.moudles.CreateMoudle<CoroutineMoudle>();

        }
        private static void InitLog()
        {
            LogConfig.FontSize = 15;
            LogConfig.LogLevel = 0;
            LogConfig.WarnningLevel = 50;
            LogConfig.ErrLevel = 100;
            LogConfig.Enable = true;
            LogConfig.LogEnable = true;
            LogConfig.WarnningEnable = false;
            LogConfig.ErrEnable = true;
        }
        private static void InitNet()
        {
            NetConfig.UDPBuffersize = 1024;
            NetConfig.TCPBuffersize = 1024;
            NetConfig.UDPPort = 12345;
            NetConfig.TCPPort = 12346;
            NetConfig.TCPMaxConn = 100;
            NetConfig.UDPMaxConn = 100;
            NetConfig.TCPCheckSpace = 3;
            NetConfig.TCPConnTimeOut = 2;
            NetConfig.UDPConnTimeOut = 3;
            NetConfig.UDPCheckSpace = 3;
        }
    }
    partial class APP
    {
        public static NetSever netSever { get { return GameState.netSever; } }

        static FsmMoudle fsm { get { return Framework.moudles.Fsm; } set { Framework.moudles.Fsm = value; } }
        const string _GoToGnae = "GoToGnae";
        const string _Quit = "Quit";

        private static void InitFsm()
        {
            fsm = Framework.moudles.CreateMoudle<FsmMoudle>();
            IdleState idle = new IdleState();
            GameState game = new GameState();
            QuitState quit = new QuitState();

            fsm.EnterState = idle;
            fsm.ExitState = quit;

            fsm.SubscribeState(idle);
            fsm.SubscribeState(game);
            fsm.SubscribeState(quit);


            var val = fsm.CreateConditionValue<bool>(_GoToGnae, false);
            var condition = fsm.CreateCondition(val, true, ConditionCompareType.EqualsWithCompare);
            fsm.CreateTransition(idle, game).BindCondition(condition);

            var val1 = fsm.CreateConditionValue<bool>(_Quit, false);
            var condition1 = fsm.CreateCondition(val1, true, ConditionCompareType.EqualsWithCompare);

            fsm.CreateTransition(game, quit).BindCondition(condition1);

            fsm.Start();
        }
        public static void GoToGame()
        {
            fsm.SetBool(_GoToGnae, true);
            Framework.moudles.Coroutine.StartCoroutine(check());
            IEnumerator check()
            {
                while (fsm.CurrentState.GetType() != typeof(GameState))
                {
                    yield return null;
                }
                fsm.SetBool(_GoToGnae, false);
            }
        }
       

        public static void Quit()
        {
            fsm.SetBool(_Quit, true);
        }
    }
    [OnFrameworkInitClass]
    public static partial class APP
    {
        static APP()
        {
            InitLog();
            InitNet();

            InitFrameworkMoudles();
            InitFsm();
        }

       
    }



}
