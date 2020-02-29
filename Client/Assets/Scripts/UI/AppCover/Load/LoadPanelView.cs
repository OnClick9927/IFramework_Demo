/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-01
 *Description:    Description
 *History:        2020-03-01--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IFramework;
using IFramework.Modules.NodeAction;

namespace IFramework_Demo
{
	public class LoadPanelView : TUIView_MVVM<LoadPanelViewModel, LoadPanel>
	{
		protected override void BindProperty()
		{
			base.BindProperty();
			//ToDo
		}

		protected override void OnClear()
		{
		}

        protected override void OnLoad()
        {
            this.Sequence(APP.envType)
                .TimeSpan(TimeSpan.FromSeconds(10))
                .OnRecyle(() => { APP.UI.GoBack(); })
                .Run();
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
            Show();
        }
    }
}
