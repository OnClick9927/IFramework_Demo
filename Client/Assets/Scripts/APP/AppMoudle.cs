/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-02-07
 *Description:    Description
 *History:        2020-02-07--
*********************************************************************************/
using System;
using IFramework;
using IFramework.Modules;
using UnityEngine;
using IFramework.Modules.Message;

namespace IFramework_Demo
{

    public struct FpsArg:IEventArgs
    {
        public float fps;
    }
    public struct ConnArg:IEventArgs
    {
        public bool conn;
    }
    public class AppModule : FrameworkAppModule,IMessagePublisher
    {
        public UIModule UI { get; private set; }
        protected override bool needUpdate { get { return true; } }
        public NetClient net { get; private set; }

        protected override void Awake()
        {
            InitAppModules();
            InitNet();
        }
        private void InitAppModules()
        {
            UI = Framework.env1.modules.CreateModule<UIModule>();
            UI.AddLoader(UILoader);
            UIPanel UILoader(Type type, string path, string name, UIPanelLayer layer)
            {
                GameObject go = Resources.Load<GameObject>(path);
                UIPanel p = go.GetComponent<UIPanel>();
                return p;
            }


            UI.SetUseMVVM(UIMap_MVVM.map);


            UI.Get<StatusPanel>(UIConfig.Name<StatusPanel>(), UIConfig.Path<StatusPanel>(), UIConfig.Layer<StatusPanel>());
            UI.Get<UpdatePanel>(UIConfig.Name<UpdatePanel>(), UIConfig.Path<UpdatePanel>(), UIConfig.Layer<UpdatePanel>());
        }

       

        private void InitNet()
        {
            net = new NetClient();

            var Hs = NetMessageHandleTool.Handlers;

            for (int i = 0; i < Hs.Count; i++)
            {
                net.onTcpMessage += Hs[i].OnTcpMessage;
                net.onTcpConn += Hs[i].OnTcpConn;
                net.onTcpDisConn += Hs[i].OnTcpDisConn;
                net.onUdpMessage += Hs[i].OnUdpMessage;

            }

            net.onTcpDisConn += NetClient_onTcpDisConn;
            net.onTcpConn += NetClient_onTcpConn;
            net.Run();
            void NetClient_onTcpConn()
            {
                APP.message.Publish(this, 0, new ConnArg() { conn = true });
            }

            void NetClient_onTcpDisConn()
            {
                try
                {
                    APP.message.Publish(this, 0, new ConnArg() { conn = false });

                    net.Run();
                }
                catch (Exception)
                {
                }
               
            }
        }

       

        protected override void OnDispose()
        {
            net?.Dispose();
        }
        public float updateInterval = 0.5F;
        private double lastInterval;
        private int frames = 0;
        private float fps;
        protected override void OnUpdate()
        {
            ++frames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow > lastInterval + updateInterval)
            {
                fps = (float)(frames / (timeNow - lastInterval));
                frames = 0;
                lastInterval = timeNow;
            }

            APP.message.Publish(this, 0, new FpsArg() { fps = fps });

        }
    }
}
