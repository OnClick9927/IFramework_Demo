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
    public struct RegisterPanelViewArg:IEventArgs
    {
        public string name;
        public string psd;
        public string acc;
        public bool ok;
    }

    public class RegisterPanelView : TUIView_MVVM<RegisterPanelViewModel, RegisterPanel>
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

            Tpanel.ok.onClick.AddListener(() => {

                message.Publish(this, 1, new RegisterPanelViewArg() {
                    name = Tpanel.Name.text,
                    psd=Tpanel.psd.text,
                    acc=Tpanel.account.text,
                    ok=true

                 });
            });

            Tpanel.Name.onEndEdit.AddListener((name) =>
            {

                message.Publish(this, 1, new RegisterPanelViewArg()
                {
                    name = Tpanel.Name.text,
                    psd = Tpanel.psd.text,
                    acc = Tpanel.account.text,
                    ok = false

                });
            });
            Tpanel.psd.onEndEdit.AddListener((name) =>
            {

                message.Publish(this, 1, new RegisterPanelViewArg()
                {
                    name = Tpanel.Name.text,
                    psd = Tpanel.psd.text,
                    acc = Tpanel.account.text,
                    ok = false

                });
            });
            Tpanel.account.onEndEdit.AddListener((name) =>
            {

                message.Publish(this, 1, new RegisterPanelViewArg()
                {
                    name = Tpanel.Name.text,
                    psd = Tpanel.psd.text,
                    acc = Tpanel.account.text,
                    ok = false

                });
            });
            this.Sequence(APP.envType)
                .TimeSpan(TimeSpan.FromSeconds(200))
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
