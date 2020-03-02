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
	public class UIConfig
	{
        private static Dictionary<Type, Tuple<string, string, UIPanelLayer>> dic =
               new Dictionary<Type, Tuple<string, string, UIPanelLayer>>()
               {
                   {typeof(UpdatePanel),Tuple.Create("UpdatePanel","UI/UpdatePanel",UIPanelLayer.Common )},
                   {typeof(AppCoverPanel),Tuple.Create("AppCoverPanel","UI/AppCoverPanel",UIPanelLayer.Common )},
                   {typeof(RegisterPanel),Tuple.Create("RegisterPanel","UI/RegisterPanel",UIPanelLayer.Common )},
                   {typeof(LoadPanel),Tuple.Create("LoadPanel","UI/LoadPanel",UIPanelLayer.Common )},
                   {typeof(StatusPanel),Tuple.Create("StatusPanel","UI/StatusPanel",UIPanelLayer.Top )},

               };
        public static string Name<T>()
        {
            return dic[typeof(T)].Item1;
        }
        public static string Path<T>()
        {
            return dic[typeof(T)].Item2;
        }
        public static UIPanelLayer Layer<T>()
        {
            return dic[typeof(T)].Item3;
        }
    }
}
