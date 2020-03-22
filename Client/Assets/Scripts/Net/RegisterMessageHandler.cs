/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-23
 *Description:    Description
 *History:        2020-03-23--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IFramework;
using IFramework.Modules.Message;
using IFramework.Net;

namespace IFramework_Demo
{
     class RegisterMessageHandler : NetMessageHandler, IMessagePublisher
    {
        protected override void OnTcpConn()
        {
        }

        protected override void OnTcpDisConn()
        {
        }

        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {
            if (message is RegisterResponse)
            {
                APP.message.Publish(this, 0, null, message);
            }
        }

        protected override void OnUdpMessage(SocketToken token, INetMessage message)
        {
        }
    }
}
