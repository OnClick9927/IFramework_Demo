/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-27
 *Description:    Description
 *History:        2020-03-27--
*********************************************************************************/
using System;
using IFramework;
using IFramework.UI;

namespace IFramework_Demo
{
    public class TipPanelViewModel : UIViewModel<TipPanelData>
	{
 		private String _text;
		public String text
		{
			get { return GetProperty(ref _text, this.GetPropertyName(() => _text)); }
			private set			{
				Tmodel.text = value;
				SetProperty(ref _text, value, this.GetPropertyName(() => _text));
			}
		}

        private String _err_text;
        public String err_text
        {
            get { return GetProperty(ref _err_text, this.GetPropertyName(() => _err_text)); }
            private set
            {
                Tmodel.err_text = value;
                SetProperty(ref _err_text, value, this.GetPropertyName(() => _err_text));
            }
        }
        private String _danmu;
        public String danmu
        {
            get { return GetProperty(ref _danmu, this.GetPropertyName(() => _danmu)); }
            private set
            {
                Tmodel.danmu = value;
                SetProperty(ref _danmu, value, this.GetPropertyName(() => _danmu));
            }
        }



        protected override void SyncModelValue()
		{
 			this.text = Tmodel.text;
            this.err_text =Tmodel. err_text;

        }
        protected override void SubscribeMessage()
        {
            base.SubscribeMessage();
            APP.message.Subscribe<PanMessageHandler>(ListenMessage);
            APP.message.Subscribe<LoginMessageHandler>(ListenMessage);
            APP.message.Subscribe<RegisterMessageHandler>(ListenMessage);
            APP.message.Subscribe<AppModule>(ListenMessage);
            APP.message.Subscribe<SeverFormMessageHandler>(ListenMessage);
            APP.message.Subscribe<ChatBroadCastMessageHandler>(ListenMessage);


        }

        private void ListenMessage(Type publishType, int code, IEventArgs args, object[] param)
        {
            if (args is ConnArg)
            {
                ConnArg ar = (ConnArg)args;
                if (ar.conn)
                {
                    err_text = string.Format("<color=#BC00FFFF>【{0}】  Connect Sever Sucess</color>", DateTime.Now.ToString("HH:mm:ss"));
                }
                else
                {
                    if (string.IsNullOrEmpty(ar.err))
                    {
                        err_text = string.Format("<color=#BC00FFFF>【{0}】 Try Connect Sever {1}</color>", DateTime.Now.ToString("HH:mm:ss"), ar.count);
                    }
                    else
                    {
                        err_text = string.Format("<color=#BC00FFFF>【{0}】  {1}</color>", DateTime.Now.ToString("HH:mm:ss"), ar.err);

                    }
                }
            }
            if (param == null || param.Length == 0) return;
            if (param[0] is SeverFormBroadCast)
            {
                SeverFormBroadCast br = param[0] as SeverFormBroadCast;
                text = string.Format("【{1}】 Sever : <color=#BC00FFFF>{0}</color>", 
                    br.message,
                     DateTime.Now.ToString("HH:mm:ss"));
            }
             if (param[0] is PanBroadCast)
            {
                PanBroadCast br = param[0] as PanBroadCast;
                text = string.Format("【{4}】 {0}  绘制  [{1},{2}]  <color=#{3}>■Color {3}</color> 半径{5}", br.name,
                    br.colomn.posX, br.colomn.posY,br.colomn.color, DateTime.Now.ToString("HH:mm:ss"),br.size);
            }
            if (param[0] is loginBroadCast)
            {
                loginBroadCast br = param[0] as loginBroadCast;
                text = string.Format("【{1}】 {0} 上线了", br.name, DateTime.Now.ToString("HH:mm:ss"));
               
            }
            if (param[0] is LoginResponse)
            {
                LoginResponse br = param[0] as LoginResponse;
                err_text = string.Format("<color=#BC00FFFF>【{0}】 登录 {1}</color>", DateTime.Now.ToString("HH:mm:ss"), br.err);
            }
            if (param[0] is RegisterResponse)
            {
                RegisterResponse br = param[0] as RegisterResponse;
                err_text = string.Format("<color=#BC00FFFF>【{0}】 注册 {1}</color>", DateTime.Now.ToString("HH:mm:ss"), br.err);
            }
            if (param[0] is PanResponse)
            {
                danmu = string.Format("<color=#D9934F66>【{0}】图片请求中</color>", DateTime.Now.ToString("HH:mm:ss"));
            }
            if (param[0] is ChatBroadCast)
            {
                ChatBroadCast br = param[0] as ChatBroadCast;
                danmu = string.Format("【{2}】{0}: {1}" , br.name,br.message, DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        protected override void UnSubscribeMessage()
        {
            base.UnSubscribeMessage();
            APP.message.UnSubscribe<AppModule>(ListenMessage);

            APP.message.UnSubscribe<PanMessageHandler>(ListenMessage);
            APP.message.UnSubscribe<LoginMessageHandler>(ListenMessage);
            APP.message.UnSubscribe<RegisterMessageHandler>(ListenMessage);
            APP.message.UnSubscribe<SeverFormMessageHandler>(ListenMessage);
             
            APP.message.UnSubscribe<ChatBroadCastMessageHandler>(ListenMessage);

        }
    }
}
