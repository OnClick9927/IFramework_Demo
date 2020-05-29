

using UnityEngine;

namespace IFramework.Lua
{
    public class AsyncTest : MonoBehaviour
    {

        void Start()
        {
            XLuaEnv.DoString("require 'async_test'");
        }

    }
}

