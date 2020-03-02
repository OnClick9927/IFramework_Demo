/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-22
 *Description:    Description
 *History:        2020-03-22--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IFramework;

namespace IFramework_Demo
{
	public class StatusPanelView : TUIView_MVVM<StatusPanelViewModel, StatusPanel>
	{
		protected override void BindProperty()
		{
			base.BindProperty();
            //ToDo
            handler.BindProperty(() => {
                bool conn = Tcontext.connected;
                if (conn)
                {
                    Tpanel.connected.color = UnityEngine.Color.green;
                }
                else
                {
                    Tpanel.connected.color = UnityEngine.Color.red;
                }
            })
            .BindProperty(()=> {
                float fps = Tcontext.fps;
                Tpanel.fps.text = string.Format("Fps : {0} ", fps);

            });
		}

		protected override void OnClear()
		{
		}

		protected override void OnLoad()
		{
		}

		protected override void OnPop(UIEventArgs arg)
		{
		}

		protected override void OnPress(UIEventArgs arg)
		{
		}

		protected override void OnTop(UIEventArgs arg)
		{
		}

	}
}
