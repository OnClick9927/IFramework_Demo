/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-08-27
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEngine;

namespace IFramework.GUITool
{


    public abstract class FocusAbleGUIDrawer : GUIDrawer
    {
        public string FocusID { get; private set; }
        protected bool focused;
        public bool Focused
        {
            get
            {
                return focused;
            }
            set
            {
                if (value)
                    OnFcous();
                else
                {
                    OnFocusOther();
                    if (!focused)
                        OnLostFous();
                }
                focused = value;
            }
        }
        public FocusAbleGUIDrawer()
        {
            FocusID = GUIFocusControl.Subscribe(this);
        }
        public Rect position { get; private set; }
        protected virtual void OnFcous() { }
        protected virtual void OnFocusOther() { }
        protected virtual void OnLostFous() { }

        public override void Dispose()
        {
            GUIFocusControl.UnSubscribe(this);
        }

        public virtual void Focus()
        {
            GUIFocusControl.Focus(this);
        }

        public virtual void Difuse()
        {
            GUIFocusControl.Diffuse(this);
        }
    }

}
