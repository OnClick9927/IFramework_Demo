/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-24
 *Description:    Description
 *History:        2020-03-24--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IFramework;
using IFramework.Modules.Message;
using IFramework.Net;

namespace IFramework_Demo
{
	 class LoginMessageHandler : NetMessageHandler, IMessagePublisher
    {
        protected override void OnTcpConn()
        {
        }

        protected override void OnTcpDisConn()
        {
        }

        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {
            if (message is LoginResponse)
            {
                APP.message.Publish(this, 0, null, message);
            }
            if (message is loginBroadCast)
            {
                APP.message.Publish(this, 0, null, message);
            }
        }

       
    }
}
