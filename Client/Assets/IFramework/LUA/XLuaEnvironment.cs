/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-08-13
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using XLua;
using UnityEngine;
using System.Collections.Generic;
using System;
using IFramework.Singleton;

namespace IFramework.Lua
{
    public interface IXLuaLoader
    {
        byte[] load(ref string filepath);
    }
    [MonoSingletonPath("IFramework/XLuaEnvironment")]
    public class XLuaEnvironment :MonoSingletonPropertyClass<XLuaEnvironment>
	{
        private LuaEnv LuaEnv;
        private List<LuaTable> tables;
        private static float LastGCTime;

        public static LuaTable GlobalTable { get { return Instance.LuaEnv.Global; } }
        public static float GCInterval=1f;
        public static bool Disposed { get; private set; }
        public static Action OnDispose;

        public static LuaTable NewTable()
        {
            LuaTable table = Instance.LuaEnv.NewTable();
            Instance.tables.Add(table);
            return table;
        }
        public static void AddLoader(IXLuaLoader loader)
        {
           Instance. LuaEnv.AddLoader(loader.load);
        }

        public static object[] DoString(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance.LuaEnv.DoString(chunk, chunkName, env);
        }
        public static object[] DoString(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance.LuaEnv.DoString(chunk, chunkName, env);
        }
        public static LuaFunction LoadString(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance.LuaEnv.LoadString(chunk, chunkName, env);
        }
        public static T LoadString<T>(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance.LuaEnv.LoadString<T>(chunk, chunkName, env);
        }
        public static T LoadString<T>(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance.LuaEnv.LoadString<T>(chunk, chunkName, env);
        }


        protected override void OnSingletonInit()
        {
            Disposed = false;
            LuaEnv = new LuaEnv();
            tables = new List<LuaTable>();
        }
        public override void Dispose()
        {
            if (OnDispose != null) OnDispose();
            tables.ForEach((table) =>
            {
                table.Dispose();
            });
            DoString(@"
                    local util = require 'xlua.util'
                   -- util.print_func_ref_by_csharp()
            ");
            LuaEnv.Dispose();
            LuaEnv = null;
            Disposed = true;
        }
        private void OnDestroy()
        {
            Dispose();
        }
        private void Update()
        {
            if (LuaEnv == null) return;
            if (Time.time - LastGCTime > GCInterval)
            {
                LuaEnv.Tick();
                LastGCTime = Time.time;
            }
        }

        public static void FullGc()
        {
            Instance.LuaEnv.FullGc();
        }
    }
}
