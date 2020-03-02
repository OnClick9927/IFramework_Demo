/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1.440
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-28
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules.MVP;
using UnityEngine;

namespace IFramework
{
    public class UIEntity : MVPEntity
    {
        public UIPanel panel;
        protected override void OnDestory()
        {
            base.OnDestory();
            if (panel != null && panel.gameObject != null)
                GameObject.Destroy(panel.gameObject);
        }
    }
}
