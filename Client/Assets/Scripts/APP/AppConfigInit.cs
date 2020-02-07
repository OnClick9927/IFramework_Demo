/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-02-13
 *Description:    Description
 *History:        2020-02-13--
*********************************************************************************/
using IFramework;
using IFramework.Modules.Coroutine;
using IFramework.Modules.Loom;
using IFramework.Modules.Message;

namespace IFramework_Demo
{
    [OnFrameworkInitClass(1)]
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
            Framework.env1.modules.Coroutine = Framework.env1.modules.CreateModule<CoroutineModule>();
            Framework.env1.modules.Loom = Framework.env1.modules.CreateModule<LoomModule>();
            Framework.env1.modules.Message = Framework.env1.modules.CreateModule<MessageModule>();
            Framework.env1.modules.App = Framework.env1.modules.CreateModule<AppModule>();
        }
        private static void InitNetConfig()
        {
            NetConfig.TCPBuffersize = 1024;
            NetConfig.UDPBuffersize = 1024;

            NetConfig.UDPPort = 12345;
            NetConfig.TCPPort = 12346;

            NetConfig.BufferNumber = 64;
            NetConfig.TCPIP = "127.0.0.1";
            NetConfig.UDPIP = "127.0.0.1";
        }
        private static void InitLog()
        {
            Log.LogLevel = 0;
            Log.WarnningLevel = 50;
            Log.ErrLevel = 100;
            Log.Enable = true;
            Log.LogEnable = true;
            Log.WarnningEnable = false;
            Log.ErrEnable = true;
        }
    }
}
