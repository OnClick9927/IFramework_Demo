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
using IFramework.Modules.APP;
using UnityEngine;

namespace IFramework_Demo
{
    public class AppModule : FrameworkAppModule
    {
        public UIModule UIModule { get; private set; }
        protected override bool needUpdate { get { return true; } }
        public NetClient netClient;

        protected override void Awake()
        {
            InitAppModules();
            InitNet();
        }
        private void InitAppModules()
        {
            UIModule = Framework.env1.modules.CreateModule<UIModule>();
            //UIModule.AddLoader(UILoader);
            //UIPanel UILoader(Type type, string path, string name, UIPanelLayer layer, UIEventArgs arg)
            //{
            //    GameObject go = Resources.Load<GameObject>(path);
            //    UIPanel p= go.GetComponent<UIPanel>();
            //    return p;
            //}
            //UIModule.Get<AppCoverPanel>("UI/AppCoverPanel", UIPanelLayer.Common, "AppCover",UIEventArgs.Allocate<UIEventArgs>());
        }

       

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
                try
                {
                    netClient.Run();
                }
                catch (Exception)
                {
                }
               
            }
        }

       

        protected override void OnDispose()
        {
            netClient?.Dispose();
        }

        protected override void OnUpdate()
        {

        }
    }
}
