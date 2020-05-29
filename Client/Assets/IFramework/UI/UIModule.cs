/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-07-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.UI
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
        private List<IPanelLoader> _loaders;
        public int loaderCount { get { return _loaders.Count; } }

        public Stack<UIPanel> stack;
        public Stack<UIPanel> memory;
        public int stackCount { get { return stack.Count; } }
        public int memoryCount { get { return memory.Count; } }
        public bool IsInStack(UIPanel panel) { return stack.Contains(panel); }
        public bool IsInMemory(UIPanel panel) { return memory.Contains(panel); }
        public bool IsInUse(UIPanel panel) { return IsInMemory(panel) || IsInStack(panel); }
        public UIPanel current
        {
            get
            {
                if (stack.Count == 0)
                    return null;
                return stack.Peek();
            }
        }
        public UIPanel MemoryPeek()
        {
            if (memory.Count == 0)
                return null;
            return memory.Peek();
        }
    }
    public partial class UIModule : FrameworkModule
    {

        private IGroups _groups;
        public override int priority { get { return 80; } }

        protected override void Awake()
        {
            InitTransform();

            stack = new Stack<UIPanel>();
            memory = new Stack<UIPanel>();
            _loaders = new List<IPanelLoader>();

        }
        protected override void OnDispose()
        {

            stack.Clear();
            memory.Clear();
            _loaders.Clear();

            if (_groups != null)
                _groups.Dispose();
            if (Canvas != null)
                GameObject.Destroy(Canvas.gameObject);
        }



        public void AddLoader(IPanelLoader loader)
        {
            _loaders.Add(loader);
        }

        public void SetGroups(IGroups groups)
        {
            this._groups = groups;
        }


        public UIPanel Load(Type type, string path, UIPanelLayer layer, string name)
        {
            if (_groups == null)
                throw new Exception("Please Set ModuleType First");
            UIPanel ui = default(UIPanel);
            if (_loaders == null || loaderCount == 0)
            {
                Log.E("NO UILoader");
                return ui;
            }
            for (int i = 0; i < _loaders.Count; i++)
            {
                var result = _loaders[i].Load(type, path, name, layer);
                if (result == null) continue;

                ui = result;
                ui = GameObject.Instantiate(ui);
                ui.PanelLayer = layer;
                SetParent(ui);
                ui.PanelName = name;
                ui.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                _groups.Subscribe(ui);

                return ui;
            }
            Log.E(string.Format("NO ui Type: {0}  Path: {1}  Layer: {2}  Name: {3}", type, path, layer, name));
            return ui;
        }
        public T Load<T>(string path, string name, UIPanelLayer layer) where T : UIPanel
        {
            return (T)Load(typeof(T), path, layer, name);
        }
        public bool HaveLoadPanel(string panelName)
        {
            return _groups.FindPanel(panelName) != null;
        }


        public void Push(UIPanel ui)
        {
            UIEventArgs arg = UIEventArgs.Allocate<UIEventArgs>(this.container.env.envType);
            arg.code = UIEventArgs.Code.Push;
            if (stackCount > 0)
                arg.pressPanel = current;
            arg.curPanel = ui;

            stack.Push(ui);
            InvokeUIModuleEventListenner(arg);
            arg.Recyle();
            if (memory.Count > 0) ClearCache();
        }
        public void GoForWard()
        {
            UIEventArgs arg = UIEventArgs.Allocate<UIEventArgs>(this.container.env.envType);
            arg.code = UIEventArgs.Code.GoForward;

            if (memoryCount <= 0) return;
            if (stackCount > 0)
                arg.pressPanel = current;

            var ui = memory.Pop();
            arg.curPanel = ui;
            stack.Push(ui);
            InvokeUIModuleEventListenner(arg);
            arg.Recyle();
        }
        public void GoBack()
        {
            UIEventArgs arg = UIEventArgs.Allocate<UIEventArgs>(this.container.env.envType);
            arg.code = UIEventArgs.Code.GoBack;
            if (stackCount <= 0) return;

            var ui = stack.Pop();
            arg.popPanel = ui;
            memory.Push(ui);

            if (stackCount > 0)
                arg.curPanel = current;
            InvokeUIModuleEventListenner(arg);
            arg.Recyle();

        }
        private void InvokeUIModuleEventListenner(UIEventArgs arg)
        {
            _groups.InvokeUIModuleEventListenner(arg);
        }


        public void ClearCache()
        {
            while (memory.Count != 0)
            {
                UIPanel p = memory.Pop();
                if (p != null && !IsInStack(p))
                {
                    _groups.UnSubscribe(p);
                }
            }
        }


        public UIPanel Get(Type type, string name, string path = "", UIPanelLayer layer = UIPanelLayer.Common)
        {
            //if (UICache.Count > 0) ClearCache(arg);

            if (current != null && current.PanelName == name && current.GetType() == type)
                return current;
            var panel = _groups.FindPanel(name);
            if (panel == null)
                panel = Load(type, path, layer, name);
            Push(panel);
            return panel;
        }
        public T Get<T>(string name, string path = "", UIPanelLayer layer = UIPanelLayer.Common) where T : UIPanel
        {
            return (T)Get(typeof(T), name, path, layer);
        }
    }
}
