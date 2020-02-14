/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1.440
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-28
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
namespace IFramework
{
    internal interface IUIModuleEventListenner
    {
        void OnLoad();
        void OnTop(UIEventArgs arg);
        void OnPress(UIEventArgs arg);
        void OnPop(UIEventArgs arg);
        void OnClear();
    }
}
