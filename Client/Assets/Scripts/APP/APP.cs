/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using IFramework;
using IFramework.UI;
using UnityEngine;
namespace IFramework_Demo
{
    public partial class APP
    {
        public const EnvironmentType envType = EnvironmentType.Ev1;
        public static AppModule App { get { return Framework.env1.modules.App as AppModule; } }
        public static UIModule UI { get { return App.UI; } }
        public static FrameworkEnvironment env { get { return Framework.env1; } }

        public static NetClient net { get { return APP.App.net; } }

        public static IFramework.Modules.Message.MessageModule message{ get { return env.modules.Message; } }

        public static string acc;
        public static string uname;
        internal static string psd;
        internal const int NetMaxConnCount=10;
    }
     
    public partial class APP :MonoBehaviour
    {
        private void Awake()
        {
            Framework.InitEnv("RT", envType).InitWithAttribute();
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
