

using UnityEngine;

namespace IFramework.Lua
{
    public class AsyncTest : MonoBehaviour
    {

        void Start()
        {
            XLuaEnvironment.DoString("require 'async_test'");
        }

    }
}

