/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1.440
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-28
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules.MVVM;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IFramework
{
    public abstract class UIView_MVVM : View, IUIModuleEventListenner
    {
        public UIPanel panel;
        protected void Show()
        {
            panel.gameObject.SetActive(true);
        }
        protected void Hide()
        {
            panel.gameObject.SetActive(false);
        }

        protected void BindText(Text text, Func<object> getter)
        {
            handler.BindProperty(() => { text.text = getter().ToString(); });
        }
        protected void BindSlider(Slider slider, Func<float> getter)
        {
            handler.BindProperty(() => { slider.value = getter(); });
        }
        protected void BindToogle(Toggle toggle, Func<bool> getter)
        {
            handler.BindProperty(() => { toggle.isOn = getter(); });
        }


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
    public abstract class TUIView_MVVM<VM, Panel> : UIView_MVVM where VM : UIViewModel_MVVM where Panel : UIPanel
    {

        public VM Tcontext { get { return this.context as VM; } }
        public Panel Tpanel { get { return this.panel as Panel; } }
    }

}
