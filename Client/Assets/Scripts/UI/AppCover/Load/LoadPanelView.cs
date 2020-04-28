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
using IFramework.NodeAction;

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
            Tpanel.ok.onClick.AddListener(() => {

                message.Publish(this, 1, new LoadPanelViewArg()
                {
                    psd = Tpanel.psd.text,
                    acc = Tpanel.account.text,
                    ok = true

                });
            });

            Tpanel.psd.onEndEdit.AddListener((name) =>
            {

                message.Publish(this, 1, new LoadPanelViewArg()
                {
                    psd = Tpanel.psd.text,
                    acc = Tpanel.account.text,
                    ok = false

                });
            });
            Tpanel.account.onEndEdit.AddListener((name) =>
            {

                message.Publish(this, 1, new LoadPanelViewArg()
                {
                    psd = Tpanel.psd.text,
                    acc = Tpanel.account.text,
                    ok = false

                });
            });
        }

        protected override void OnPop(UIEventArgs arg)
        {
            Hide();
            if (node != null)
            {
                node.Recyle();
                node = null;
            }
        }

        protected override void OnPress(UIEventArgs arg)
        {
            Hide();
            if (node != null)
            {
                node.Recyle();
                node = null;
            }
        }
        SequenceNode node;

        protected override void OnTop(UIEventArgs arg)
        {
            Show();
            node = this.Sequence(APP.envType)
            .TimeSpan(TimeSpan.FromSeconds(20))
            .OnCompelete(() => { Log.L("rs"); APP.UI.GoBack(); })
            .Run();
        }
    }
}
