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
using  IFramework.AB;
namespace IFramework.Lua
{
    public interface IXLuaLoader
    {
        byte[] load(ref string filepath);
    }

    public class AssetBundleLoader : IXLuaLoader
    {
        public  static string genPath
        {
            get { return Application.dataPath.CombinePath("IFramework/HotFix/Scripts").ToRegularPath(); }
        }
        public byte[] load(ref string filepath)
        {
            string path = filepath.Replace(".", "/");
            path = path.Append(".lua");
             path = genPath.CombinePath(path).ToAssetsPath();
             var asset = ABAssets.Load<TextAsset>(path);
             var bytes = (asset.asset as TextAsset)?.bytes;
             asset.Release();
             return  bytes ;
        }
    }
    public class XLuaEnv :SingletonPropertyClass<XLuaEnv>
	{
        private XLuaEnv() { }
        private LuaEnv _luaenv;
        private List<LuaTable> _tables;
        private static float _lastGCTime;

        public static LuaTable gtable { get { return Instance._luaenv.Global; } }
        public static float gcInterval=1f;
        public static bool disposed { get; private set; }
        public static Action onDispose;

        public static LuaTable NewTable()
        {
            LuaTable table = Instance._luaenv.NewTable();
            Instance._tables.Add(table);
            return table;
        }
        public static void AddLoader(IXLuaLoader loader)
        {
           Instance. _luaenv.AddLoader(loader.load);
        }

        public static LuaFunction LoadString(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance._luaenv.LoadString(chunk, chunkName, env);
        }
        public static T LoadString<T>(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance._luaenv.LoadString<T>(chunk, chunkName, env);
        }
        public static T LoadString<T>(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance._luaenv.LoadString<T>(chunk, chunkName, env);
        }



        public static object[] DoString(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance._luaenv.DoString(chunk, chunkName, env);
        }
        public static object[] DoString(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            return Instance._luaenv.DoString(chunk, chunkName, env);
        }



        public static LuaTable GetTable(TextAsset luaScript, string chunkName = "chunk")
        {
            //// 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable meta = XLuaEnv.NewTable();
            meta.Set("__index", XLuaEnv.gtable);
            LuaTable scriptEnv = XLuaEnv.NewTable();
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            DoString(luaScript.text, chunkName, scriptEnv);
            return scriptEnv;
        }
        

        public static void FullGc()
        {
            Instance._luaenv.FullGc();
        }

        protected override void OnSingletonInit()
        {
            disposed = false;
            _luaenv = new LuaEnv();
            _tables = new List<LuaTable>();
            Framework.BindEnvUpdate(Update, EnvironmentType.Ev1);
            Framework.BindEnvDispose(Dispose, EnvironmentType.Ev1);
        }
        public override void Dispose()
        {
           
            if (onDispose != null) onDispose();

            _tables.ForEach((table) =>
            {
                table.Dispose();
            });
            // DoString(@"
            //         local util = require 'xlua.util'
            //        -- util.print_func_ref_by_csharp()
            // ");
            _luaenv.Dispose();
            _luaenv = null;
            disposed = true;
        }
        private void Update()
        {
            if (_luaenv == null) return;
            if (Time.time - _lastGCTime > gcInterval)
            {
                _luaenv.Tick();
                _lastGCTime = Time.time;
            }
        }

    }
}
