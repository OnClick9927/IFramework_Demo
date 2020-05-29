/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-07-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;

namespace IFramework.UI
{
    public interface IPanelLoader
    {
        UIPanel Load(Type type, string path, string name, UIPanelLayer layer);
    }
}
