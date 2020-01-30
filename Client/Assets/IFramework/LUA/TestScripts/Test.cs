/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-08
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
namespace IFramework.Lua
{
	public class Test:UnityEngine.MonoBehaviour
	{
        public UnityEngine.TextAsset txt;
        [UnityEngine.ContextMenu("Con")]
        private void Awake()
        {
            XLuaEnvironment.DoString(txt.bytes);
                

        }
    }
}
