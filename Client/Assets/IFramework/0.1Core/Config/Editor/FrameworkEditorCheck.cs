/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-12-09
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System.IO;
using UnityEditor;
using UnityEngine;

namespace IFramework
{
    class FrameworkEditorCheck
    {
        [InitializeOnLoadMethod]
        public static void Check()
        {
            Framework.env0 = Framework.CreateEnv("IFramework_Editor");
            Framework.env0.Init();
            EditorApplication.quitting += Framework.env0.Dispose;

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
