/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-07-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Modules;
using IFramework.Modules.MVP;
using IFramework.Modules.MVVM;

namespace IFramework
{
    public partial class UIModule
    {
        public Canvas Canvas { get; private set; }
        private RectTransform UIRoot;
        public RectTransform BGBG { get; private set; }
        public RectTransform Background { get; private set; }
        public RectTransform AnimationUnderPage { get; private set; }
        public RectTransform Common { get; private set; }
        public RectTransform AnimationOnPage { get; private set; }
        public RectTransform PopUp { get; private set; }
        public RectTransform Guide { get; private set; }
        public RectTransform Toast { get; private set; }
        public RectTransform Top { get; private set; }
        public RectTransform TopTop { get; private set; }
        public RectTransform UICamera { get; private set; }
        private RectTransform InitTransform(string name)
        {
            GameObject go = new GameObject(name);
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.SetParent(Canvas.transform);
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.localPosition = Vector3.zero;
            rect.sizeDelta = Vector3.zero;
            return rect;
        }
        private void InitTransform()
        {
            UIRoot = new GameObject(name).AddComponent<RectTransform>();
            Canvas = UIRoot.gameObject.AddComponent<Canvas>();
            UIRoot.gameObject.AddComponent<CanvasScaler>();
            UIRoot.gameObject.AddComponent<GraphicRaycaster>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            BGBG = InitTransform("BGBG");
            Background = InitTransform("Background");
            AnimationUnderPage = InitTransform("AnimationUnderPage");
            Common = InitTransform("Common");
            AnimationOnPage = InitTransform("AnimationOnPage");
            PopUp = InitTransform("PopUp");
            Guide = InitTransform("Guide");
            Toast = InitTransform("Toast");
            Top = InitTransform("Top");
            TopTop = InitTransform("TopTop");
            UICamera = InitTransform("UICamera");
        }
        public void SetCamera(Camera ca, bool isLast = true, int index = -1)
        {
            UICamera.SetChildWithIndex(ca.transform, !isLast ? index : UICamera.childCount);
        }
        public void SetParent(UIPanel ui, bool isLast = true, int index = -1)
        {
            switch (ui.PanelLayer)
            {
                case UIPanelLayer.BGBG:
                    BGBG.SetChildWithIndex(ui.transform, !isLast ? index : BGBG.childCount);
                    break;
                case UIPanelLayer.Background:
                    Background.SetChildWithIndex(ui.transform, !isLast ? index : Background.childCount);
                    break;
                case UIPanelLayer.AnimationUnderPage:
                    AnimationUnderPage.SetChildWithIndex(ui.transform, !isLast ? index : AnimationUnderPage.childCount);
                    break;
                case UIPanelLayer.Common:
                    Common.SetChildWithIndex(ui.transform, !isLast ? index : Common.childCount);
                    break;
                case UIPanelLayer.AnimationOnPage:
                    AnimationOnPage.SetChildWithIndex(ui.transform, !isLast ? index : AnimationOnPage.childCount);
                    break;
                case UIPanelLayer.PopUp:
                    PopUp.SetChildWithIndex(ui.transform, !isLast ? index : PopUp.childCount);
                    break;
                case UIPanelLayer.Guide:
                    Guide.SetChildWithIndex(ui.transform, !isLast ? index : Guide.childCount);
                    break;
                case UIPanelLayer.Toast:
                    Toast.SetChildWithIndex(ui.transform, !isLast ? index : Toast.childCount);
                    break;
                case UIPanelLayer.Top:
                    Top.SetChildWithIndex(ui.transform, !isLast ? index : Top.childCount);
                    break;
                case UIPanelLayer.TopTop:
                    TopTop.SetChildWithIndex(ui.transform, !isLast ? index : TopTop.childCount);
                    break;
                default:
                    break;
            }
            ui.transform.LocalIdentity();
        }
    }
    public partial class UIModule
    {
        private List<LoadDel> loaders;
        public int LoaderCount { get { return loaders.Count; } }

        public Stack<UIPanel> UIStack;
        public Stack<UIPanel> UICache;
        public UIPanel StackTop { get { return UIStack.Peek(); } }
        public int StackCount { get { return UIStack.Count; } }
        public int CacheCount { get { return UICache.Count; } }
        public bool IsInStack(UIPanel panel) { return UIStack.Contains(panel); }
        public bool IsInCache(UIPanel panel) { return UICache.Contains(panel); }
        public bool IsInUse(UIPanel panel) { return IsInCache(panel) || IsInStack(panel); }
        public UIPanel Current { get { return Peek(); } }
        public UIPanel Peek() { return UIStack.Peek(); }
        public UIPanel CachePeek() { return UICache.Peek(); }
    }
    public partial class UIModule : FrameworkModule
    {
        public enum ModuleType
        {
            MVP,
            MVVM
        }
        private class UIGroups :IDisposable
        {
            private ModuleType _moduleType;
            private MVPModule _mvpModule;
            private Dictionary<Type, Tuple<Type, Type, Type, Type, Type>> _mvpMap;

            private MVVMModule _mvvmModule;
            private Dictionary<Type, Tuple<Type, Type, Type>> _mvvmMap;

            public UIGroups(ModuleType moduleType, Dictionary<Type, Tuple<Type, Type, Type, Type, Type>> map) 
            {
                _mvpModule = CreatInstance<MVPModule>("UIGroup");
                this._moduleType = moduleType;
                this._mvpMap = map;
            }

            public UIGroups(ModuleType moduleType, Dictionary<Type, Tuple<Type, Type, Type>> map)
            {
                _mvvmModule = CreatInstance<MVVMModule>("UIGroup");
                this._moduleType = moduleType;
                this._mvvmMap = map;
            }

            public MVPGroup MVPSubscribe(UIPanel panel)
            {
                var _group = FindMVPGroup(panel);
                if (_group != null) throw new Exception(string.Format("Have Subscribe Panel Name: {0}", panel.PanelName));

                Tuple<Type, Type, Type, Type, Type> tuple;
                _mvpMap.TryGetValue(panel.GetType(), out tuple);
                if (tuple == null) throw new Exception(string.Format("Could Not Find map with Type: {0}", panel.GetType()));
                var enity = Activator.CreateInstance(tuple.Item1) as UIEnity;
                    enity.panel = panel;

                var sensor = Activator.CreateInstance(tuple.Item2) as UISensor_MVP;
                var policy = Activator.CreateInstance(tuple.Item3) as UIPolicy_MVP;
                var executor = Activator.CreateInstance(tuple.Item4) as UIExecutor_MVP;
                var view = Activator.CreateInstance(tuple.Item5) as UIView_MVP;

                MVPGroup group = new MVPGroup(enity, sensor, policy, executor, view, panel.PanelName);
                _mvpModule.AddGroup(group);
                return group;
            }
            public MVPGroup FindMVPGroup(UIPanel panel)
            {
                return FindMVPGroup(panel.PanelName);
            }
            public MVPGroup FindMVPGroup(string panelName)
            {
                return _mvpModule.FindGroup(panelName);
            }

            public MVVMGroup MVVMSubscribe(UIPanel panel)
            {
                var _group = FindMVVMGroup(panel);
                if (_group != null) throw new Exception(string.Format("Have Subscribe Panel Name: {0}", panel.PanelName));

                Tuple<Type, Type, Type> tuple;
                _mvvmMap.TryGetValue(panel.GetType(), out tuple);
                if (tuple == null) throw new Exception(string.Format("Could Not Find map with Type: {0}", panel.GetType()));


                var model = Activator.CreateInstance(tuple.Item1) as IDataModel;
                var view = Activator.CreateInstance(tuple.Item2) as UIView_MVVM;
                var vm = Activator.CreateInstance(tuple.Item3) as UIViewModel_MVVM;
                view.panel = panel;

                UIGroup_MVVM group = new UIGroup_MVVM(panel.name, view, vm, model);
                _mvvmModule.AddGroup(group);
                return group;
            }

            public MVVMGroup FindMVVMGroup(UIPanel panel)
            {
                return FindMVVMGroup(panel.PanelName);
            }
            public MVVMGroup FindMVVMGroup(string panelName)
            {
                return _mvvmModule.FindGroup(panelName);
            }
            public void UnSubscribe(UIPanel panel)
            {
                switch (_moduleType)
                {
                    case ModuleType.MVP:
                        var group = FindMVPGroup(panel);
                        group.Dispose();
                        break;
                    case ModuleType.MVVM:
                        var group1 = FindMVVMGroup(panel);
                        group1.Dispose();
                        break;
                }

            }



            public void Update()
            {
                switch (_moduleType)
                {
                    case ModuleType.MVP:
                        _mvpModule.Update();
                        break;
                    case ModuleType.MVVM:
                        _mvvmModule.Update();
                        break;
                }
            }
            public void Dispose()
            {
                switch (_moduleType)
                {
                    case ModuleType.MVP:
                        _mvpModule.Dispose();
                        break;
                    case ModuleType.MVVM:
                        _mvvmModule.Dispose();
                        break;
                }
            }
        }

        private UIGroups _uiGroups;
        private ModuleType _moduleType;

        protected override void Awake()
        {
            InitTransform();

            UIStack = new Stack<UIPanel>();
            UICache = new Stack<UIPanel>();
            loaders = new List<LoadDel>();

        }
        protected override void OnDispose()
        {
            UIStack.Clear();
            UICache.Clear();
            loaders.Clear();
            if (_uiGroups!=null)
                _uiGroups.Dispose();
            if (Canvas!=null)
                GameObject.Destroy(Canvas.gameObject);
        }

        protected override void OnUpdate()
        {
            if (_uiGroups!=null)
                _uiGroups.Update();
        }


        public delegate UIPanel LoadDel(Type type, string path, string name, UIPanelLayer layer);

        public void AddLoader(LoadDel loader)
        {
            loaders.Add(loader);
        }
        public void SetUseMVP(Dictionary<Type, Tuple<Type, Type, Type, Type, Type>> map)
        {
            this._moduleType = ModuleType.MVP;
            _uiGroups = new UIGroups(ModuleType.MVP, map);
        }
        public void SetUseMVVM(Dictionary<Type, Tuple<Type, Type, Type>> map)
        {
            this._moduleType = ModuleType.MVVM;
            _uiGroups = new UIGroups(ModuleType.MVVM, map);
        }

        public UIPanel Load(Type type, string path, UIPanelLayer layer, string name)
        {
            if (_uiGroups == null)
                throw new Exception("Please Set ModuleType First");
            UIPanel ui = default(UIPanel);
            if (loaders == null || LoaderCount == 0)
            {
                Log.E("NO UILoader");
                return ui;
            }
            for (int i = 0; i < loaders.Count; i++)
            {
                var result = loaders[i].Invoke(type, path, name, layer);
                if (result == null) continue;

                ui = result;
                ui = GameObject.Instantiate(ui);
                ui.PanelLayer = layer;
                SetParent(ui);
                ui.PanelName = name;
                ui.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                switch (_moduleType)
                {
                    case ModuleType.MVP:
                        var enity = _uiGroups.MVPSubscribe(ui);
                        (enity.sensor as IUIModuleEventListenner).OnLoad();
                        break;
                    case ModuleType.MVVM:
                        var enity2 = _uiGroups.MVVMSubscribe(ui);
                        (enity2.view as IUIModuleEventListenner).OnLoad();
                        break;
                }

                return ui;
            }
            Log.E(string.Format("NO ui Type: {0}  Path: {1}  Layer: {2}  Name: {3}", type, path, layer, name));
            return ui;
        }
        public T Load<T>(string path, string name, UIPanelLayer layer) where T : UIPanel
        {
            return (T)Load(typeof(T), path, layer, name);
        }
        public bool HaveLoadPanel( string panelName)
        {
            switch (_moduleType)
            {
                case ModuleType.MVP:
                    return null == _uiGroups.FindMVPGroup(panelName);
                case ModuleType.MVVM:
                    return null == _uiGroups.FindMVVMGroup(panelName);
                default:
                    return false;
            }
        }


        public void Push(UIPanel ui, UIEventArgs arg)
        {
            if (StackCount > 0)
                arg.pressPanel = Current;
            arg.curPanel = ui;

            UIStack.Push(ui);
            InvokeUIModuleEventListenner(arg);
            if (UICache.Count > 0) ClearCache();
        }
        public void GoForWard(UIEventArgs arg)
        {
            if (CacheCount <= 0) return;
            if (StackCount > 0)
                arg.pressPanel = Current;

            var ui = UICache.Pop();
            arg.curPanel = ui;
            UIStack.Push(ui);
            InvokeUIModuleEventListenner(arg);
        }
        public void GoBack(UIEventArgs arg)
        {
            if (StackCount <= 0) return;

            var ui = UIStack.Pop();
            arg.popPanel = ui;
            UICache.Push(ui);

            if (StackCount > 0)
                arg.curPanel = Current;
            InvokeUIModuleEventListenner(arg);
        }
        private void InvokeUIModuleEventListenner(UIEventArgs arg)
        {
            switch (_moduleType)
            {
                case ModuleType.MVP:
                    if (arg.pressPanel != null)
                        (_uiGroups.FindMVPGroup(arg.pressPanel).sensor as IUIModuleEventListenner).OnPress(arg);
                    if (arg.popPanel != null)
                        (_uiGroups.FindMVPGroup(arg.popPanel).sensor as IUIModuleEventListenner).OnPop(arg);
                    if (arg.curPanel!=null)
                        (_uiGroups.FindMVPGroup(arg.curPanel).sensor as IUIModuleEventListenner).OnTop(arg);

                    break;
                case ModuleType.MVVM:
                    if (arg.pressPanel != null)
                        (_uiGroups.FindMVVMGroup(arg.pressPanel).view as IUIModuleEventListenner).OnPress(arg);
                    if (arg.popPanel != null)
                        (_uiGroups.FindMVVMGroup(arg.popPanel).view as IUIModuleEventListenner).OnPop(arg);
                    if (arg.curPanel != null)
                        (_uiGroups.FindMVVMGroup(arg.curPanel).view as IUIModuleEventListenner).OnTop(arg);
                    break;
            }
        }


        public void ClearCache()
        {
            switch (_moduleType)
            {
                case ModuleType.MVP:
                    MVPClearCache();
                    break;
                case ModuleType.MVVM:
                    MVVMClearCache();
                    break;
            }
        }
        private void MVPClearCache()
        {
            while (UICache.Count != 0)
            {
                UIPanel p = UICache.Pop();
                if (p != null && !IsInStack(p))
                {
                    var group = _uiGroups.FindMVPGroup(p);
                    if (group != null)
                    {
                        (group.sensor as IUIModuleEventListenner).OnClear();
                        _uiGroups.UnSubscribe(p);
                    }
                }
            }
        }
        private void MVVMClearCache()
        {
            while (UICache.Count != 0)
            {
                UIPanel p = UICache.Pop();
                if (p != null && !IsInStack(p))
                {
                    var group = _uiGroups.FindMVVMGroup(p);
                    if (group != null)
                    {
                        (group.view as IUIModuleEventListenner).OnClear();
                        _uiGroups.UnSubscribe(p);
                    }

                }
            }
        }

        public UIPanel Get(Type type, string name, UIEventArgs arg, string path="", UIPanelLayer layer= UIPanelLayer.Common)
        {
            //if (UICache.Count > 0) ClearCache(arg);
            switch (_moduleType)
            {
                case ModuleType.MVP:
                    var enity = _uiGroups.FindMVPGroup(name);
                    if (enity == null)
                    {
                        UIPanel ui = Load(type, path, layer, name);
                        Push(ui, arg);
                        return ui;
                    }
                    else
                    {
                        Push((enity.enity as UIEnity).panel, arg);
                        return (enity.enity as UIEnity).panel;
                    }
                case ModuleType.MVVM:
                    var group = _uiGroups.FindMVVMGroup(name);
                    if (group == null)
                    {
                        UIPanel ui = Load(type, path, layer, name);
                        Push(ui, arg);
                        return ui;
                    }
                    else
                    {
                        Push((group.view as UIView_MVVM).panel, arg);
                        return (group.view as UIView_MVVM).panel;
                    }
                default:
                    return null;
            }
            
        }
        public T Get<T>(string name, UIEventArgs arg, string path = "", UIPanelLayer layer = UIPanelLayer.Common) where T : UIPanel
        {
            return (T)Get(typeof(T), name, arg, path, layer);
        }
    }
}
