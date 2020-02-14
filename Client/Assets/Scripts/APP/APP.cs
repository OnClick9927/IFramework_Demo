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
        private void Start()
        {
            Framework.InitEnv("RT", EnvironmentType.Ev1).InitWithAttribute();

        }
        private void Update()
        {
            Framework.env1.Update();
        }
        private void OnDisable()
        {
            Framework.env1.Update();
            Framework.env1.Dispose();
        }
    }
}
