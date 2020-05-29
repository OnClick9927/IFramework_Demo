/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-08-14
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace IFramework.Lua
{
    [CSharpCallLua]
    public delegate void DoSth2(LuaFunction func,params object[] obj);

    [Serializable]
    public class Injection
    {
        public string Key="";
        public GameObject value;
    }
    public class LuaBehavior:MonoBehaviour
	{
        public TextAsset luaScript;
        public List<Injection> injections = new List<Injection>();
       
        private Action luaAwake;
        private Action luaOnEnable;
        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDisable;
        private Action luaOnDestroy;

        private LuaTable scriptEnv;
        private void LuaDispose()
        {
            luaAwake = null;
            luaOnEnable = null;
            luaStart = null;
            luaUpdate = null;
            luaOnDisable = null;
            if (luaOnDestroy != null) luaOnDestroy();
            luaOnDestroy = null;
            injections = null;
            Dispose();
            if (!XLuaEnv.disposed)
            {
                XLuaEnv.onDispose -= LuaDispose;
            }
        }
        protected virtual void Dispose() { }
        private byte[] load(ref string filepath)
        {
            return System.IO.File.ReadAllBytes("E:\\PPro\\unity\\IF\\Assets\\IFramework\\Unity\\LUA\\function.lua.txt");
        }
        private void Awake()
        {
           // XLuaEnvironment.AddLoader(load);
            XLuaEnv.onDispose += LuaDispose;
            scriptEnv = XLuaEnv.GetTable(luaScript, "LuaTestScript");
            //scriptEnv = XLuaEnv.NewTable();

            //// 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            //LuaTable meta = XLuaEnv.NewTable();
            //meta.Set("__index", XLuaEnv.GlobalTable);
            //scriptEnv.SetMetaTable(meta);
            //meta.Dispose();

            scriptEnv.Set("this", this);
            foreach (var injection in injections) scriptEnv.Set(injection.Key, injection.value);

            //XLuaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);

            luaAwake = scriptEnv.Get<Action>("Awake");
            luaOnEnable = scriptEnv.Get<Action>("OnEnable");
            luaStart = scriptEnv.Get<Action>("Start");
            luaUpdate = scriptEnv.Get<Action>("Update");
            luaOnDisable = scriptEnv.Get<Action>("OnDisable");
            luaOnDestroy = scriptEnv.Get<Action>("OnDestroy");

            var test1 = scriptEnv.Get<DoSth2>("test"); 
            var test2 = scriptEnv.Get<LuaFunction>("test2");
            
            test1.Invoke(test2, 1, 2);
            //object[] objs = test2.Call( 1, 2);


            //if (luaAwake != null)
            //{
            //    luaAwake();
            //    luaAwake = null;
            //}
        }


        private void OnEnable()
        {
            if (luaOnEnable != null) luaOnEnable();
        }
        private void Start()
        {
            if (luaStart != null)
            {
                luaStart();
                luaStart = null;
            } 

        }
        private void Update()
        {
            if (luaUpdate != null) luaUpdate();

        }
        private void OnDisable()
        {
            if (luaOnDisable != null) luaOnDisable();
        }
        private void OnDestroy()
        {
            LuaDispose();
        }
    }
}
