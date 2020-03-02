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
using IFramework.Modules.Message;

namespace IFramework_Demo
{
	public class StatusPanelViewModel : TUIViewModel_MVVM<StatusPanelData>
	{
 		private float _fps;
		public float fps
		{
			get { return GetProperty(ref _fps, this.GetPropertyName(() => _fps)); }
			private set			{
				Tmodel.fps = value;
				SetProperty(ref _fps, value, this.GetPropertyName(() => _fps));
			}
		}

		private Boolean _connected;
		public Boolean connected
		{
			get { return GetProperty(ref _connected, this.GetPropertyName(() => _connected)); }
			private set			{
				Tmodel.connected = value;
				SetProperty(ref _connected, value, this.GetPropertyName(() => _connected));
			}
		}


		protected override void SyncModelValue()
		{
 			this.fps = Tmodel.fps;
			this.connected = Tmodel.connected;

		}
        protected override void SubscribeMessage()
        {
            base.SubscribeMessage();
            APP.message.Subscribe<AppModule>(FPS_Conn);
        }

        private void FPS_Conn(Type publishType, int code, IEventArgs args, object[] param)
        {
            if (args is ConnArg)
            {
                ConnArg c = (ConnArg)args;
                connected = c.conn;
            }
            if (args is FpsArg)
            {
                FpsArg c = (FpsArg)args;
                fps = c.fps;
            }
        }

        protected override void UnSubscribeMessage()
        {
            base.UnSubscribeMessage();
            APP.message.Unsubscribe<AppModule>(FPS_Conn);

        }
    }
}
