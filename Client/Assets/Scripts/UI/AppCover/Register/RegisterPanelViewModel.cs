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


		protected override void SyncModelValue()
		{
 			this.account = Tmodel.account;
			this.password = Tmodel.password;

		}

	}
}
