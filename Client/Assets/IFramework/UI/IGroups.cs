/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-07-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using IFramework.Modules;

namespace IFramework.UI
{
    public interface IGroups:IDisposable
    {
        UIPanel FindPanel(string name);
        void InvokeUIModuleEventListenner(UIEventArgs arg);
        void Subscribe(UIPanel panel);
        void UnSubscribe(UIPanel panel);
    }

}
