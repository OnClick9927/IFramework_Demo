/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1.440
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-28
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
namespace IFramework.UI
{
    public class UIEventArgs : FrameworkArgs
    {
        public enum Code
        {
            GoBack,GoForward,Push
        }
        public Code code;
        public UIPanel popPanel;
        public UIPanel curPanel;
        public UIPanel pressPanel;
        protected override void OnDataReset()
        {
            popPanel = null;
            curPanel = null;
            pressPanel = null;
        }

    }
}
