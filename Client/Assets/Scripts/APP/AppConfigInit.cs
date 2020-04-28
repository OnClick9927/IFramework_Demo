/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-02-13
 *Description:    Description
 *History:        2020-02-13--
*********************************************************************************/
using IFramework;
using IFramework.Modules;
using IFramework.Modules.Coroutine;
using IFramework.Modules.Message;

namespace IFramework_Demo
{
    [OnEnvironmentInit(   EnvironmentType.Ev1)]
    public static class AppConfigInit
    {
        static AppConfigInit()
        {
            InitLog();
            InitNetConfig();
            InitFrameworkMoudles();
        }
        private static void InitFrameworkMoudles()
        {
            APP.env.modules.Coroutine = APP.env.modules.CreateModule<CoroutineModule>();
            APP.env.modules.Loom = APP.env.modules.CreateModule<LoomModule>();
            APP.env.modules.Message = APP.env.modules.CreateModule<MessageModule>();
            APP.env.modules.App = APP.env.modules.CreateModule<AppModule>();
        }
        private static void InitNetConfig()
        {
            NetConfig.TCPBuffersize = 8192;

            NetConfig.TCPPort = 12346;

            NetConfig.BufferNumber = 64;
            //NetConfig.TCPIP = "180.76.149.93";
            NetConfig.TCPIP = "127.0.0.1";

        }
        private static void InitLog()
        {
            Log.lev_L = 0;
            Log.lev_W = 50;
            Log.lev_E = 100;
            Log.enable = true;
            Log.enable_L = true;
            Log.enable_W = true;
            Log.enable_E = true;
        }
    }
}
