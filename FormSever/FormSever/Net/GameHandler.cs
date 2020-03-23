using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFramework.Net;
using IFramework;
using FormSever.Game;

namespace FormSever.Net
{

    [NetMessage(6)]
    class PanRequest : INetMessage
    {
        public GameColum colomn;
        public string account;
        public string name;
        public int size;
    }
    [NetMessage(7)]
    class PanResponse :INetMessage
    {
        public List<GameColum> colums;
    }
    [NetMessage(8)]
    class AskPanRequest : INetMessage
    {
        public string account;
        public string name;
    }
    [NetMessage(9)]
    class PanBroadCast : INetMessage
    {
        public Game.GameColum colomn;
        public string account;
        public string name;
        public int size;
    }
    class GameHandler : NetMessageHandler
    {
        protected override void OnTcpConn(SocketToken token) { }

        protected override void OnTcpDisConn(SocketToken token) { }
        protected override void OnTcpMessage(SocketToken token, INetMessage message)
        {

            if (message is PanRequest)
            {
                 PanRequest req = message as PanRequest;
                 bool bo=  APP.gameMap.WriteColumn(req.colomn.posX, req.colomn.posY, req.colomn.color,req.size);

                Log.L(string.Format("User {0}/{1}   Pan   [{2},{3}]    {4}  {5}", req.account, req.name, req.colomn.posX, req.colomn.posY, req.colomn.color,req.size));
                net.SendTcpMessageToAll(new PanBroadCast()
                {
                    colomn=req.colomn,
                    account=req.account,
                    name=req.name
                    ,size=req.size
                });
            }
            if (message is AskPanRequest)
            {

                AskPanRequest req = message as AskPanRequest;

                APP.gameMap.BroadCastMap((resp) =>
                {
                    APP.net.SendTcpMessage(token,resp );


                });

                Log.L(string.Format("User {0}/{1}   aaskForMap  ", req.account, req.name));
            }
        }

       
    }
}
