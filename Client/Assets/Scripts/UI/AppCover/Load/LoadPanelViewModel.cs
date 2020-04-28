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
using IFramework.UI;

namespace IFramework_Demo
{
	public class LoadPanelViewModel : UIViewModel<LoadPanelData>
	{
 		private String _account;
		public String account
		{
			get { return GetProperty(ref _account, this.GetPropertyName(() => _account)); }
			private set			{
				Tmodel.account = value;
				SetProperty(ref _account, value, this.GetPropertyName(() => _account));
			}
		}

		private String _password;
		public String password
		{
			get { return GetProperty(ref _password, this.GetPropertyName(() => _password)); }
			private set			{
				Tmodel.password = value;
				SetProperty(ref _password, value, this.GetPropertyName(() => _password));
			}
		}

      
        protected override void SyncModelValue()
		{
 			this.account = Tmodel.account;
			this.password = Tmodel.password;

		}
        protected override void SubscribeMessage()
        {
            base.SubscribeMessage();
            message.Subscribe<LoadPanelView>(ListenView);
            APP.message.Subscribe<LoginMessageHandler>(ListenNet);


        }

        private void ListenNet(Type publishType, int code, IEventArgs args, object[] param)
        {
            LoginResponse res = param[0] as LoginResponse;
            if (res!=null && res.sucess)
            {
                APP.acc = res.account;
                APP.uname = res.name;
                APP.psd = password;
                APP.UI.Get<GamePanel>(UIConfig.Name<GamePanel>(), UIConfig.Path<GamePanel>(), UIConfig.Layer<GamePanel>());
            }
        }

        private void ListenView(Type publishType, int code, IEventArgs args, object[] param)
        {
            if (args is LoadPanelViewArg)
            {
                LoadPanelViewArg arg = (LoadPanelViewArg)args;
                if (arg.ok)
                {
                    APP.net.SendTcpMessage(new LoginRequest()
                    {
                        account = arg.acc,
                        psd = arg.psd,
                    });
                }
                else
                {
                    account = arg.acc;
                    password = arg.psd;
                }
            }
        }

        protected override void UnSubscribeMessage()
        {
            base.UnSubscribeMessage();
            message.UnSubscribe<LoadPanelView>(ListenView);
            APP.message.UnSubscribe<LoginMessageHandler>(ListenNet);

        }
    }
}
