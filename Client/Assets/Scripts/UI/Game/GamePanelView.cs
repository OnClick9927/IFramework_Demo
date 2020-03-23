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
using UnityEngine;

namespace IFramework_Demo
{
    public class GamePanelView : TUIView_MVVM<GamePanelViewModel, GamePanel>
	{
		protected override void BindProperty()
		{
			base.BindProperty();
            //ToDo
            handler.BindProperty(() =>
            {
                Vector2 pos = Tcontext.curSelectPos;

                float x = pos.x / (float)GamePanelViewModel.MapSize;
                float y= pos.y / (float)GamePanelViewModel.MapSize;
                if (Tpanel.slider_pox_x.value != x)
                {
                    Tpanel.slider_pox_x.value = x;
                }

                if (Tpanel.slider_pox_y.value != y)
                {
                    Tpanel.slider_pox_y.value = y;
                }
                Tpanel.SetKuang(pos);
            })
            .BindProperty(()=> {
                var tx = Tcontext.tx;
                Tpanel.map.texture = tx;
            })
            .BindProperty(()=> {
                Tpanel.input_chat.text = Tcontext.chat;
            })
            .BindProperty(()=> {
                Tpanel.input_size.text = Tcontext.pan_size.ToString();
            });
		}

		protected override void OnClear()
		{
          
        }

		protected override void OnLoad()
		{
            Tpanel.colorPicker.onValueChanged.AddListener((color) => {

                message.Publish(this, (int)GamePanelViewEveType.Color, new GamePanelViewArg()
                {
                    color = color
                });
            });
            Tpanel.slider_pox_x.onValueChanged.AddListener((val) => {

                Tpanel.input_pox_x.text = Math.Round(val * GamePanelViewModel.MapSize).ToString();
               
            });
            Tpanel.slider_pox_y.onValueChanged.AddListener((val) => {
                Tpanel.input_pox_y.text = Math.Round(val * GamePanelViewModel.MapSize).ToString();
            });
            Tpanel.input_pox_x.onValueChanged.AddListener((str) => {
                message.Publish(this, (int)GamePanelViewEveType.Input_X, new GamePanelViewArg()
                {
                    input_x = int.Parse(Tpanel.input_pox_x.text),
                });
            });
            Tpanel.input_pox_y.onValueChanged.AddListener((str) => {
                message.Publish(this, (int)GamePanelViewEveType.Input_Y, new GamePanelViewArg()
                {
                    input_y = int.Parse(Tpanel.input_pox_y.text),
                });
            });

            Tpanel.ok.onClick.AddListener(() => {
                message.Publish(this, (int)GamePanelViewEveType.Button_OK, new GamePanelViewArg()
                {
                });
            });
            Tpanel.chatBtn.onClick.AddListener(() => {
                message.Publish(this, (int)GamePanelViewEveType.Button_Chat, new GamePanelViewArg()
                {
                });
            });
            Tpanel.input_chat.onEndEdit.AddListener((str) =>
            {
                message.Publish(this, (int)GamePanelViewEveType.Input_Chat, new GamePanelViewArg()
                {
                    input_chat = str
                });
            });
            Tpanel.ask_map.onClick.AddListener(() => {
                message.Publish(this, (int)GamePanelViewEveType.Button_ForMap, new GamePanelViewArg()
                {
                });
            });
            Tpanel.slider_size.onValueChanged.AddListener((val) => {

                message.Publish(this, (int)GamePanelViewEveType.Slider_Pan_size, new GamePanelViewArg()
                {
                    slider_panSize = (int)val
                });
            });

        }

        protected override void OnPop(UIEventArgs arg)
		{
            Hide();
		}

		protected override void OnPress(UIEventArgs arg)
		{
            Hide();
		}

		protected override void OnTop(UIEventArgs arg)
		{
            Tpanel.nameText.text = APP.uname;
            Show();
		}

	}
}
