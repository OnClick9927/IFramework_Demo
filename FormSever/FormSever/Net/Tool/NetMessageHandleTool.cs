using IFramework;
using IFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormSever.Net
{
    [OnFrameworkInitClass]
    static class NetMessageHandleTool
    {
        readonly static List<INetMessageHangdler> Handlers;
        public static IEnumerable<Action<SocketToken, INetMessage>> TcpFunc;
        public static IEnumerable<Action<SocketToken, INetMessage>> UdpFunc;
        public static IEnumerable<Action<SocketToken>> TcpConn;
        public static IEnumerable<Action<SocketToken>> TcpDisConn;

        static NetMessageHandleTool()
        {
            Handlers = typeof(INetMessageHangdler).GetSubTypesInAssemblys()
                .Select((type) => {
                    if (!type.IsAbstract)
                        return Activator.CreateInstance(type) as INetMessageHangdler;
                    return null;
                }).ToList();

            Handlers.RemoveAll((h) => { return h == null; });
            TcpFunc = Handlers.Select<INetMessageHangdler, Action<SocketToken, INetMessage>>((handler) => {
                return handler.OnTcpMessage;
            });
            UdpFunc = Handlers.Select<INetMessageHangdler, Action<SocketToken, INetMessage>>((handler) => {
                return handler.OnUdpMessage;
            });
            TcpConn = Handlers.Select<INetMessageHangdler, Action<SocketToken>>((handler) => {
                return handler.OnTcpConn;
            });
            TcpDisConn = Handlers.Select<INetMessageHangdler, Action<SocketToken>>((handler) => {
                return handler.OnTcpDisConn;
            });
        }
    }
}
