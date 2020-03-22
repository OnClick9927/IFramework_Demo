/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using IFramework;
using IFramework.Net;

namespace IFramework_Demo
{
    [OnEnvironmentInit]
    static class NetMessageHandleTool
    {
       public  readonly static List<INetMessageHandler> Handlers;
        //public static IEnumerable<Action<SocketToken, INetMessage>> TcpFunc;
        //public static IEnumerable<Action<SocketToken, INetMessage>> UdpFunc;
        //public static IEnumerable<Action> TcpConn;
        //public static IEnumerable<Action> TcpDisConn;

        static NetMessageHandleTool()
        {
            Handlers = typeof(INetMessageHandler).GetSubTypesInAssemblys()
                .Select((type) => {
                    if (!type.IsAbstract)
                        return Activator.CreateInstance(type) as INetMessageHandler;
                    return null;
                }).ToList();

            Handlers.RemoveAll((h) => { return h == null; });
            //TcpFunc = Handlers.Select<INetMessageHangdler, Action<SocketToken, INetMessage>>((handler) => {
            //    return handler.OnTcpMessage;
            //});
            //UdpFunc = Handlers.Select<INetMessageHangdler, Action<SocketToken, INetMessage>>((handler) => {
            //    return handler.OnUdpMessage;
            //});
            //TcpConn = Handlers.Select<INetMessageHangdler, Action>((handler) => {
            //    return handler.OnTcpConn;
            //});
            //TcpDisConn = Handlers.Select<INetMessageHangdler, Action>((handler) => {
            //    return handler.OnTcpDisConn;
            //});
        }
    }
}
