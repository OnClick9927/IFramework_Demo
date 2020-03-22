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

    public class RegisterPanelViewModel : TUIViewModel_MVVM<RegisterPanelData>
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
        private String _name;
        public String Name
        {
            get { return GetProperty(ref _name, this.GetPropertyName(() => _name)); }
            private set
            {
                Tmodel.name = value;
                SetProperty(ref _name, value, this.GetPropertyName(() => _name));
            }
        }

        protected override void SyncModelValue()
		{
 			this.account = Tmodel.account;
			this.password = Tmodel.password;
            this.Name = Tmodel.name;
		}
        protected override void SubscribeMessage()
        {
            base.SubscribeMessage();
            message.Subscribe<RegisterPanelView>(ListenView);
            APP.message.Subscribe<RegisterMessageHandler>(ListenNet);
        }
        protected override void UnSubscribeMessage()
        {
            base.UnSubscribeMessage();
            message.Unsubscribe<RegisterPanelView>(ListenView);
            APP.message.Unsubscribe<RegisterMessageHandler>(ListenNet);


        }

        private void ListenNet(Type publishType, int code, IEventArgs args, object[] param)
        {
            RegisterResponse res = param[0] as RegisterResponse;
            if (res.sucess)
            {
                APP.UI.GoBack();
            }

        }

        private void ListenView(Type publishType, int code, IEventArgs args, object[] param)
        {
            if (args is RegisterPanelViewArg)
            {
                RegisterPanelViewArg arg = (RegisterPanelViewArg)args;
                if (arg.ok)
                {
                    APP.net.SendTcpMessage(new RegisterRequest()
                    {
                        account=arg.acc,
                        psd=arg.psd,
                        name=arg.name
                    });
                }
                else
                {
                    account = arg.acc;
                    password = arg.psd;
                    Name = arg.name;
                }
            }
        }
    }
}
