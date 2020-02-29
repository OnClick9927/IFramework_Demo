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
	public class UIMap_MVVM 
	{
		public static Dictionary<Type, Tuple<Type, Type, Type>> map =
		new Dictionary<Type, Tuple<Type, Type, Type>>()
		{
			{typeof(IFramework_Demo.UpdatePanel),Tuple.Create(typeof(IFramework_Demo.UpdatePanelData),typeof(IFramework_Demo.UpdatePanelView),typeof(IFramework_Demo.UpdatePanelViewModel))},
			{typeof(IFramework_Demo.AppCoverPanel),Tuple.Create(typeof(IFramework_Demo.AppCoverPanelData),typeof(IFramework_Demo.AppCoverPanelView),typeof(IFramework_Demo.AppCoverPanelViewModel))},
			{typeof(IFramework_Demo.LoadPanel),Tuple.Create(typeof(IFramework_Demo.LoadPanelData),typeof(IFramework_Demo.LoadPanelView),typeof(IFramework_Demo.LoadPanelViewModel))},
			{typeof(IFramework_Demo.RegisterPanel),Tuple.Create(typeof(IFramework_Demo.RegisterPanelData),typeof(IFramework_Demo.RegisterPanelView),typeof(IFramework_Demo.RegisterPanelViewModel))},
//ToDo
		};
	}
}
