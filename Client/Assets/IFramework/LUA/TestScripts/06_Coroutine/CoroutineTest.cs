using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace IFramework.Lua
{
    public class Coroutine_Runner : MonoBehaviour
    {
    }


    public static class CoroutineConfig
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp
        {
            get
            {
                return new List<Type>()
            {
                typeof(WaitForSeconds),
                //typeof(WWW)
            };
            }
        }
    }
    public class CoroutineTest : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            XLuaEnvironment.DoString("require 'coruntine_test'");
        }
    }
}
