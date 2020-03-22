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
using IFramework.Net;

namespace IFramework_Demo
{
    interface INetMessageHandler
    {
        void OnTcpMessage(SocketToken token, INetMessage message);
        void OnUdpMessage(SocketToken token, INetMessage message);
        void OnTcpConn();
        void OnTcpDisConn();

    }
    abstract class NetMessageHandler : INetMessageHandler
    {
        protected NetClient net { get { return (Framework.env1.modules.App as AppModule).net; } }
        protected abstract void OnTcpMessage(SocketToken token, INetMessage message);
        protected abstract void OnUdpMessage(SocketToken token, INetMessage message);
        protected abstract void OnTcpConn();
        protected abstract void OnTcpDisConn();

        void INetMessageHandler.OnTcpMessage(SocketToken token, INetMessage message)
        {
            OnTcpMessage(token, message);
        }

        void INetMessageHandler.OnUdpMessage(SocketToken token, INetMessage message)
        {
            OnUdpMessage(token, message);
        }

        void INetMessageHandler.OnTcpConn()
        {
            OnTcpConn();
        }

        void INetMessageHandler.OnTcpDisConn()
        {
            OnTcpDisConn();
        }
    }

}
