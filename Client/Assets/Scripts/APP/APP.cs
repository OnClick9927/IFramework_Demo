/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using IFramework;
using UnityEngine;
namespace IFramework_Demo
{
    public partial class APP :MonoBehaviour
    {
        private void Awake()
        {
            Framework.Init();
        }
        private void Update()
        {
            Framework.Update();
        }
        private void OnDisable()
        {
            Framework.Update();
            Framework.Dispose();
        }
    }
}
