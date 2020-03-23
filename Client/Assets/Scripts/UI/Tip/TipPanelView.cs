/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-27
 *Description:    Description
 *History:        2020-03-27--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IFramework;

namespace IFramework_Demo
{
	public class TipPanelView : TUIView_MVVM<TipPanelViewModel, TipPanel>
	{
		protected override void BindProperty()
		{
			base.BindProperty();
            handler.BindProperty(() =>
            {
                string text = Tcontext.text;
                Tpanel.NetMessage(text);
            })
            .BindProperty(()=> {
                Tpanel.ErrMessage(Tcontext.err_text);
            })
            .BindProperty(()=> {
                Tpanel.Danmu(Tcontext.danmu);
            });
            
			//ToDo
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
