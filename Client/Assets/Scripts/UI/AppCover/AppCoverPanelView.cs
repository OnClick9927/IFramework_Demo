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
	public class AppCoverPanelView : TUIView_MVVM<AppCoverPanelViewModel, AppCoverPanel>
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
            this.Tpanel.Btn_Register.onClick.AddListener(() =>
            {
                APP.UI.Get<RegisterPanel>(UIConfig.Name<RegisterPanel>(), UIConfig.Path<RegisterPanel>(), UIConfig.Layer<RegisterPanel>());
            });
            this.Tpanel.Btn_Load.onClick.AddListener(() =>
            {
                APP.UI.Get<LoadPanel>(UIConfig.Name<LoadPanel>(), UIConfig.Path<LoadPanel>(), UIConfig.Layer<LoadPanel>());
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
            Show();
        }

    }
}
