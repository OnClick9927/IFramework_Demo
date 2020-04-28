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
using UnityEngine.UI;

namespace IFramework.UI
{

    public abstract class UIView : View, IUIModuleEventListenner
    {
        public enum ViewState
        {
           None, Load, Top, Press, Pop, Clear
        }
        public UIPanel panel;
        private ViewState _lastState= ViewState.None;

        public ViewState lastState { get { return _lastState; } }

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
            _lastState = ViewState.Load;
            OnLoad();
        }
        void IUIModuleEventListenner.OnTop(UIEventArgs arg)
        {
            _lastState = ViewState.Top;
            OnTop(arg);
        }
        void IUIModuleEventListenner.OnPress(UIEventArgs arg)
        {
            _lastState = ViewState.Press;
            OnPress(arg);
        }
        void IUIModuleEventListenner.OnPop(UIEventArgs arg)
        {
            _lastState = ViewState.Pop;
            OnPop(arg);
        }
        void IUIModuleEventListenner.OnClear()
        {
            _lastState = ViewState.Clear;
            OnClear();
        }

        protected abstract void OnLoad();
        protected abstract void OnTop(UIEventArgs arg);
        protected abstract void OnPress(UIEventArgs arg);
        protected abstract void OnPop(UIEventArgs arg);
        protected abstract void OnClear();
    }
    public abstract class UIView<VM, Panel> : UIView where VM : UIViewModel where Panel : UIPanel
    {

        public VM Tcontext { get { return this.context as VM; } }
        public Panel Tpanel { get { return this.panel as Panel; } }
    }

}
