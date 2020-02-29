/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.1f1
 *Date:           2019-03-22
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEditor;
using IFramework.Modules.Coroutine;

namespace IFramework
{
    class EditorEnv
    {
        private const string RelativeCorePath = "0.1Core";
        private const string RelativeCoreEditorPath = "0.1Core/Editor";
        private const string RelativeUtilPath = "UTil";
        private const string RelativeEditorPath = "UTil/Editor";
        private static string _Fpath;
        private const EnvironmentType envType = EnvironmentType.Ev0;

        public static string FrameworkName { get { return Framework.FrameworkName; } }
        public static string Author { get { return Framework.Author; } }
        public static string Version { get { return Framework.Version; } }
        public static string Description { get { return Framework.Description; } }
        public static string FrameworkPath { get {
                if (string.IsNullOrEmpty(_Fpath))
                {
                    string[] assetPaths = AssetDatabase.GetAllAssetPaths();
                    for (int i = 0; i < assetPaths.Length; i++)
                    {
                        if (assetPaths[i].Contains(RelativeCorePath))
                        {
                            string tempPath = assetPaths[i];
                            int index = tempPath.IndexOf(RelativeCorePath);
                            _Fpath= tempPath.Substring(0, index);
                            break;
                        }
                    }
                }
                return _Fpath;
            }
        }
        public static string EditorPath { get { return FrameworkPath.CombinePath(RelativeEditorPath); } }
        public static string UtilPath { get { return FrameworkPath.CombinePath(RelativeUtilPath); } }
        public static string CorePath { get { return FrameworkPath.CombinePath(RelativeCorePath); } }
        public static string CoreEditorPath { get { return  FrameworkPath.CombinePath(RelativeCoreEditorPath); } }


        public static FrameworkEnvironment Env { get { return Framework.env0; } }
        [InitializeOnLoadMethod]
        public static void EditorEnvInit()
        {
            UnityEngine.Debug.Log("FrameworkPath   right?   "+FrameworkPath);
            Framework.InitEnv("IFramework_Editor", envType).InitWithAttribute();

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
