/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-01
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEngine;
namespace IFramework.Lua
{
	public class SayHello:MonoBehaviour
	{
        private void Awake()
        {
            XLuaEnvironment.DoString("CS.UnityEngine.Debug.Log('haha By Unity')");
            XLuaEnvironment.DoString("CS.IFramework.Log.L('haha By IFramework')");

        }
    }
}
