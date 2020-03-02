/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-09-01
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IFramework
{
    public class LanguageModule : FrameworkModule
    {
        public class LanObserver : IDisposable
        {
            private bool isDisposed;
            private bool isPaused;
            public string Key { get; private set; }
            public SystemLanguage CurLan { get { return moudle.lan; } }
            public SystemLanguage Fallback { get; private set; }
            private List<LanPair> Pairs { get { return moudle.keyDic[Key]; } }
            private LanguageModule moudle;
            internal LanObserver(LanguageModule moudle, string key, SystemLanguage fallback, bool autoStart)
            {
                this.moudle = moudle;
                moudle.lanObservers.Add(this);
                this.Key = key;
                this.Fallback = fallback;
                moudle.ObserveEvent += ObserveEvent;
                isDisposed = false;
                Pause();
                if (autoStart) Start();
            }
            private Action<SystemLanguage, string> observeEvent;
            public LanObserver ObserveEvent(Action<SystemLanguage, string> eve)
            {
                this.observeEvent = eve;
                return this;
            }
            private string GetValueInPairs()
            {
                LanPair pair = Pairs.Find(p => { return p.Lan == CurLan; });
                if (pair == null)
                    pair = Pairs.Find(p => { return p.Lan == Fallback; });
                return pair == null ? string.Empty : pair.Value;
            }
            private void ObserveEvent()
            {
                if (isDisposed || isPaused) return;
                if (observeEvent != null)
                    observeEvent(CurLan, GetValueInPairs());
            }
            public void Start() { UnPause(); }

            public void Pause()
            {
                isPaused = true;
            }
            public void UnPause()
            {
                isPaused = false;
            }
            public void Dispose()
            {
                Pause();
                isDisposed = true;
                observeEvent = null;
                moudle.ObserveEvent -= ObserveEvent;
                moudle.lanObservers.Remove(this);
            }
        }

        private List<LanPair> lanPairs;
        private Dictionary<string, List<LanPair>> keyDic;
        private List<Func<List<LanPair>>> lanPairLoaders;
        private List<LanObserver> lanObservers;
        private event Action ObserveEvent;

        private SystemLanguage lan = SystemLanguage.Unknown;

        public SystemLanguage Lan
        {
            get { return lan; }
            set
            {
                if (lan == value) return;
                lan = value;
                if (ObserveEvent != null)
                    ObserveEvent();
            }
        }

        protected override bool needUpdate { get { return false; } }

        public void AddLoader(Func<List<LanPair>> loader)
        {
            lanPairLoaders.Add(loader);
        }
        public void Load(bool reWrite = true)
        {
            List<LanPair> tmpPairs = new List<LanPair>();
            lanPairLoaders.ForEach((loader) => {
                List<LanPair> result = loader.Invoke();
                if (result != null && result.Count > 0)
                    tmpPairs.AddRange(result);
            });
            tmpPairs.ForEach((tmpPair) => {
                LanPair pair = lanPairs.Find((p) => { return p.Lan == tmpPair.Lan && p.key == tmpPair.key; });
                if (pair != null && reWrite && pair.Value != tmpPair.Value)
                    pair.Value = tmpPair.Value;
                else
                    lanPairs.Add(tmpPair);
            });
            tmpPairs.Clear();
            Fresh();
        }
        public void Load(Func<List<LanPair>> loader, bool reWrite = true)
        {
            List<LanPair> tmpPairs = loader.Invoke();
            tmpPairs.ForEach((tmpPair) => {
                LanPair pair = lanPairs.Find((p) => { return p.Lan == tmpPair.Lan && p.key == tmpPair.key; });
                if (pair != null && reWrite && pair.Value != tmpPair.Value)
                    pair.Value = tmpPair.Value;
                else
                    lanPairs.Add(tmpPair);
            });
            tmpPairs.Clear();
            Fresh();
        }
        private void Fresh()
        {
            keyDic = lanPairs.GroupBy(lanPair => { return lanPair.key; }, (key, list) => { return new { key, list }; })
                    .ToDictionary((v) => { return v.key; }, (v) => { return v.list.ToList(); });
            if (ObserveEvent != null) ObserveEvent();
        }


        public LanObserver CreatObserver(string key, SystemLanguage fallback, bool autoStart = true)
        {
            if (!keyDic.ContainsKey(key)) throw new Exception(string.Format("Key Not Found {0}", key));
            List<LanPair> pairs = keyDic[key];
            var fallbackPair = pairs.Find((p) => { return p.Lan == fallback; });
            if (fallbackPair == null) Log.W(string.Format("Fallback Language Not Exist Key: {0} : Lan: {1}", key, fallback));
            LanObserver observer = new LanObserver(this, key, fallback, autoStart);
            return observer;
        }

        protected override void Awake()
        {
            lanPairs = new List<LanPair>();
            lanPairLoaders = new List<Func<List<LanPair>>>();
            lanObservers = new List<LanObserver>();
        }
        protected override void OnDispose()
        {
            lanPairs.Clear();
            lanPairLoaders.Clear();
            lanObservers.Clear();
        }
      
      

        protected override void OnUpdate()
        {

        }
    }
}
