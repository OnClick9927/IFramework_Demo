/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.318
 *UnityVersion:   2018.4.17f1
 *Date:           2020-06-01
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/

using System;
using IFramework.UI;
using UnityEngine;

namespace IFramework.Lua
{
    public class LuaGroups : IGroups
    {
        public event Action onDispose;
        public event Func<string,UIPanel> onFindPanel;
        public event Func<string, bool> onHaveLoad;
        public event Action<UIEventArgs> onInvokeListeners;
        public event Action<UIPanel> onSubscribe;
        public event Action<UIPanel> onUnSubscribe;

        public void Dispose()
        {
            if (onDispose!=null)
            {
                onDispose();
            }
            onDispose = null;
            onFindPanel = null;
            onHaveLoad = null;
            onSubscribe = null;
            onUnSubscribe = null;
            onInvokeListeners = null;
        }

        public UIPanel FindPanel(string name)
        {
            if (onFindPanel!=null)
            {
                return onFindPanel(name);
            }
            return null;
        }

      

        public void InvokeUIModuleEventListenner(UIEventArgs arg)
        {
            if (onInvokeListeners != null)
            {
                onInvokeListeners(arg);
            }
        }

        public void Subscribe(UIPanel panel)
        {
          
            if (onSubscribe != null)
            {
                onSubscribe(panel);
            }
        }

        public void UnSubscribe(UIPanel panel)
        {
            if (onUnSubscribe != null)
            {
                onUnSubscribe(panel);
            }
        }
    }

}
