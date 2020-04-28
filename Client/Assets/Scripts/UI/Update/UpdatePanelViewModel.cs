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
using IFramework.NodeAction;
using IFramework;
using IFramework.UI;

namespace IFramework_Demo
{
	public class UpdatePanelViewModel : UIViewModel<UpdatePanelData>
	{
 		private Single _progress;
		public Single progress
		{
			get { return GetProperty(ref _progress, this.GetPropertyName(() => _progress)); }
			private set			{
				Tmodel.progress = value;
				SetProperty(ref _progress, value, this.GetPropertyName(() => _progress));
			}
		}

        protected override void SyncModelValue()
		{
 			this.progress = Tmodel.progress;

		}
        protected override void SubscribeMessage()
        {
            base.SubscribeMessage();
            this.message.Subscribe<UpdatePanelView>(ListenView);
        }
        protected override void UnSubscribeMessage()
        {
            base.UnSubscribeMessage();
            this.message.UnSubscribe<UpdatePanelView>(ListenView);
        }




        private void ListenView(Type publishType, int code, IEventArgs args, object[] param)
        {
            if (args is OnPanelLoadArg)
            {
                (args as OnPanelLoadArg).Recyle();
                UpdateResources(1000);
            }
        }
        private void UpdateResources(int count)
        {
            int curCount = 0;
            this.Sequence(APP.envType)
                   .Repeat((r) =>
                   {
                       r.Event(() =>
                       {
                           if (curCount++ < count)
                           {
                               progress = curCount / (float)count;
                           }
                       });
                   }, count)
                   .OnRecyle(() => {
                       GoToAppCoverPanel();
                   })
                   .Run();
        }
        private void GoToAppCoverPanel()
        {
            APP.UI.Get<AppCoverPanel>(UIConfig.Name<AppCoverPanel>(), UIConfig.Path<AppCoverPanel>(), UIConfig.Layer<AppCoverPanel>());
        }

	}
}
