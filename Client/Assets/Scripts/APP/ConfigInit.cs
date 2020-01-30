/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IFramework;
using IFramework.Moudles.Coroutine;
using IFramework.Moudles.Fsm;
using IFramework.Moudles.Loom;
using IFramework.Moudles.Message;

namespace IFramework_Demo
{
    [OnFrameworkInitClass]
    public static class ConfigInit
    {
        static ConfigInit()
        {
            InitLog();
            InitNetConfig();
            InitFrameworkMoudles();
        }
        private static void InitFrameworkMoudles()
        {
            Framework.moudles.Coroutine = Framework.moudles.CreateMoudle<CoroutineMoudle>();
            Framework.moudles.Loom = Framework.moudles.CreateMoudle<LoomMoudle>();
            Framework.moudles.Message = Framework.moudles.CreateMoudle<MessageMoudle>();
            Framework.moudles.App = Framework.moudles.CreateMoudle<AppMoudle>();
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
