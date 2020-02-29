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

namespace IFramework_Demo
{
    public class OnPanelLoadArg : FrameworkArgs
    {
        protected override void OnDataReset()
        {
        }
    }
    public class UpdatePanelView : TUIView_MVVM<UpdatePanelViewModel, UpdatePanel>
	{
		protected override void BindProperty()
		{
			base.BindProperty();
            handler.BindProperty(() =>
            {
                this.Tpanel.progress.value = this.Tcontext.progress;
            });
			//ToDo
		}

		protected override void OnClear()
		{
		}

		protected override void OnLoad()
		{
            this.message.Publish(this, 0, RecyclableObject.Allocate<OnPanelLoadArg>( APP.envType));
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
