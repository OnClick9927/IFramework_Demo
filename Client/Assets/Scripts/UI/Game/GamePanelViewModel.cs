/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-24
 *Description:    Description
 *History:        2020-03-24--
*********************************************************************************/
using System;
using System.Collections.Generic;
using IFramework;
using UnityEngine;
using IFramework.Serialization;
using IFramework.UI;

namespace IFramework_Demo
{

	public class GamePanelViewModel : UIViewModel<GamePanelData>
	{
 		private Color _pickColor;
		public Color pickColor
		{
			get { return GetProperty(ref _pickColor, this.GetPropertyName(() => _pickColor)); }
			private set			{
				Tmodel.pickColor = value;
				SetProperty(ref _pickColor, value, this.GetPropertyName(() => _pickColor));
			}
		}

		private Vector2 _curSelectPos;
		public Vector2 curSelectPos
		{
			get { return GetProperty(ref _curSelectPos, this.GetPropertyName(() => _curSelectPos)); }
			private set			{
				Tmodel.curSelectPos = value;
				SetProperty(ref _curSelectPos, value, this.GetPropertyName(() => _curSelectPos));
			}
		}
        private string _chat;
        public string chat
        {
            get { return GetProperty(ref _chat, this.GetPropertyName(() => _chat)); }
            private set
            {
                Tmodel.chat = value;
                SetProperty(ref _chat, value, this.GetPropertyName(() => _chat));
            }
        }
        private int _pan_size;
        public int pan_size
        {
            get { return GetProperty(ref _pan_size, this.GetPropertyName(() => _pan_size)); }
            private set
            {
                Tmodel.pan_size = value;
                SetProperty(ref _pan_size, value, this.GetPropertyName(() => _pan_size));
            }
        }

        public const int MapSize = 100;
        private Texture2D _tx = new Texture2D(MapSize, MapSize, TextureFormat.RGBAFloat, false, true);
        public Texture2D tx
        {
            get { return GetProperty(ref _tx, this.GetPropertyName(() => _tx)); }
            private set
            {
                SetProperty(ref _tx, value, this.GetPropertyName(() => _tx));
            }
        }
        protected override void Initialize() 
        {
            
            base.Initialize();
            tx.filterMode = FilterMode.Point;
            APP.net.SendTcpMessage(new AskPanRequest()
            {
                account = APP.acc,
                name = APP.uname
            });
        }
        protected override void SyncModelValue()
		{
 			this.pickColor = Tmodel.pickColor;
			this.curSelectPos = Tmodel.curSelectPos;

		}
        protected override void SubscribeMessage()
        {
            base.SubscribeMessage();
            message.Subscribe<GamePanelView>(ListenView);
            APP.message.Subscribe<PanMessageHandler>(ListenNet);
        }

        private void ListenNet(Type publishType, int code, IEventArgs args, object[] param)
        {
            if (param[0] is PanResponse)
            {
                PanResponse res = param[0] as PanResponse;

                var cs = res.colums;

                int cnt = cs.Count;
                int w = MapSize; 
                int h = cnt / w;

                Color[] cols = new Color[cnt];
                for (int i = 0; i < cs.Count; i++)
                {
                    var c = cs[i];
                    Color color;
                    bool bo = StringConvert.TryConvert(c.color, out color);
                    cols[i] = color;
                }

                tx.SetPixels(0,cs[0].posX,w,h,cols);

                tx.Apply();
                //  tx.filterMode = FilterMode.Point;



            }



        }





        private void ListenView(Type publishType, int code, IEventArgs args, object[] param)
        {
            GamePanelViewArg arg = (GamePanelViewArg)args;
            GamePanelViewEveType type = (GamePanelViewEveType)code;
            switch (type)
            {
                case GamePanelViewEveType.Button_OK:
                    APP.net.SendTcpMessage(new PanRequest()
                    {
                        account = APP.acc,
                        name = APP.uname,
                        colomn = new GameColum() {
                            color=StringConvert.ConvertToString(pickColor),
                            posX=(int)curSelectPos.x,
                            posY= (int)curSelectPos.y
                        },
                        size = pan_size

                    });
                    break;

                case GamePanelViewEveType.Input_X:
                    arg.input_x = Mathf.Clamp(arg.input_x, 0, MapSize);
                    curSelectPos = new Vector2(arg.input_x, curSelectPos.y);
                    break;
                case GamePanelViewEveType.Input_Y:
                    arg.input_y = Mathf.Clamp(arg.input_y, 0, MapSize);
                    curSelectPos = new Vector2(curSelectPos.x, arg.input_y);
                    break;
                case GamePanelViewEveType.Color:
                    pickColor = arg.color;
                    break;
                case GamePanelViewEveType.Button_Chat:
                    APP.net.SendTcpMessage(new ChatBroadCast()
                    {
                        acc = APP.acc,
                        name = APP.uname,
                        message=chat
                      
                    });
                    chat = string.Empty;
                    break;
                case GamePanelViewEveType.Input_Chat:
                    chat = arg.input_chat;
                    break;
                case GamePanelViewEveType.Slider_Pan_size:
                    pan_size = arg.slider_panSize;
                    break;
                case GamePanelViewEveType.Button_ForMap:
                    APP.net.SendTcpMessage(new AskPanRequest()
                    {
                        account = APP.acc,
                        name = APP.uname
                    });
                    break;
            }
        }

        protected override void UnSubscribeMessage()
        {
            base.UnSubscribeMessage();
            message.UnSubscribe<GamePanelView>(ListenView);
            APP.message.UnSubscribe<PanMessageHandler>(ListenNet);

        }

    }
}
