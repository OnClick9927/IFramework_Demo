/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-08-19
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEngine;
namespace IFramework.UI
{

  


    public abstract class UIPanel : MonoBehaviour
    {
        public UIPanelLayer PanelLayer { get; set; }
        public virtual string PanelName  { get { return this.name; } set { this.name = value; } }
    }
}
