/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1.440
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-28
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules.MVP;

namespace IFramework
{
    public abstract class UISensor_MVP : SensorSystem, IUIModuleEventListenner
    {
        void IUIModuleEventListenner.OnLoad()
        {
            OnLoad();
        }
        void IUIModuleEventListenner.OnTop(UIEventArgs arg)
        {
            OnTop(arg);
        }
        void IUIModuleEventListenner.OnPress(UIEventArgs arg)
        {
            OnPress(arg);
        }
        void IUIModuleEventListenner.OnPop(UIEventArgs arg)
        {
            OnPop(arg);
        }
        void IUIModuleEventListenner.OnClear()
        {
            OnClear();
        }


        protected abstract void OnLoad();
        protected abstract void OnTop(UIEventArgs arg);
        protected abstract void OnPress(UIEventArgs arg);
        protected abstract void OnPop(UIEventArgs arg);
        protected abstract void OnClear();

    }

}
