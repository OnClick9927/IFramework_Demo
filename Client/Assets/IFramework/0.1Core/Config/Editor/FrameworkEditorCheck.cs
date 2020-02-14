/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-12-09
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules.Coroutine;
using UnityEditor;

namespace IFramework
{
    class FrameworkEditorCheck
    {
        [InitializeOnLoadMethod]
        public static void Check()
        {
            Framework.InitEnv("IFramework_Editor", EnvironmentType.Ev0).InitWithAttribute(); 

            EditorApplication.quitting += Framework.env0.Dispose;
            EditorApplication.update += Framework.env0.Update;
            Framework.env0.modules.Coroutine = Framework.env0.modules.CreateModule<CoroutineModule>();

#if UNITY_2018_1_OR_NEWER
            PlayerSettings.allowUnsafeCode = true;
#else
          string  path = Application.dataPath.CombinePath("mcs.rsp");
            string content = "-unsafe";
            if (File.Exists(path) && path.ReadText(System.Text.Encoding.Default) == content) return;
                path.WriteText(content, System.Text.Encoding.Default); 
            AssetDatabase.Refresh();
            EditorUtil.ReOpen2();
#endif
        }

       
    }
}
