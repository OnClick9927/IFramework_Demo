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
using IFramework.Moudles;
namespace IFramework
{
    public class UIMoudle : FrameworkMoudle
    {
        protected override bool needUpdate { get { return false; } }

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
        protected override void Awake()
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

            panelDic = new Dictionary<Type, List<UIPanel>>();
            UIStack = new Stack<UIPanel>();
            UICache = new Stack<UIPanel>();
            loaders = new List<LoadDel>();
        }
        protected override void OnDispose()
        {
            panelDic.Clear();
            UIStack.Clear();
            UICache.Clear();
            loaders.Clear();
            if (Canvas!=null)
                GameObject.Destroy(Canvas.gameObject);
        }
        protected override void OnUpdate() { }


        public delegate UIPanel LoadDel(Type type, string path, string name, UIPanelLayer layer, UIEventArgs arg);
        private List<LoadDel> loaders;
        public int LoaderCount { get { return loaders.Count; } }
        public void AddLoader(LoadDel loader)
        {
            loaders.Add(loader);
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

        public UIPanel Load(Type type, string path, UIPanelLayer layer, string name, UIEventArgs arg)
        {
            UIPanel ui = default(UIPanel);
            if (loaders == null || LoaderCount == 0)
            {
                Log.E("NO UILoader");
                return ui;
            }
            for (int i = 0; i < loaders.Count; i++)
            {
                var result = loaders[i].Invoke(type, path, name, layer, arg);
                if (result == null) continue;

                ui = result;
                ui = GameObject.Instantiate(ui);
                ui.PanelLayer = layer;
                SetParent(ui);
                (ui as IUIPanel).OnLoad(arg);
                ui.PanelName = name;
                if (!panelDic.ContainsKey(type))
                    panelDic.Add(type, new List<UIPanel>());
                panelDic[type].Add(ui);
                return ui;
            }
            Log.E(string.Format("NO ui Type: {0}  Path: {1}  Layer: {2}  Name: {3}", type, path, layer, name));
            return ui;
        }
        public T Load<T>(string path, string name, UIPanelLayer layer, UIEventArgs arg) where T : UIPanel
        {
            return (T)Load(typeof(T), path, layer, name, arg);
        }
        public bool HaveLoadPanel(Type type, UIPanelLayer layer, string name)
        {
            return null == panelDic[type].Find((p) => { return p.PanelLayer == layer && p.PanelName == name; });
        }

        public Dictionary<Type, List<UIPanel>> panelDic { get; private set; }
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

        public void Push(UIPanel ui, UIEventArgs arg)
        {
            if (StackCount != 0)
                (Current as IUIPanel).OnPress(arg);
            UIStack.Push(ui);
            (Current as IUIPanel).OnTop(arg);
            if (UICache.Count > 0) ClearCache(arg);

        }
        public void GoForWard(UIEventArgs arg)
        {
            if (CacheCount == 0) return;
            var ui = UICache.Pop();
            if (StackCount != 0)
                (Current as IUIPanel).OnPress(arg);
            UIStack.Push(ui);
            (Current as IUIPanel).OnTop(arg);
        }
        public void GoBack(UIEventArgs arg)
        {
            if (StackCount == 0) return;
            if (StackCount > 0)
            {
                var ui = UIStack.Pop();
                UICache.Push(ui);
                (ui as IUIPanel).OnPop(arg);
            }
            if (StackCount > 0)
                (Current as IUIPanel).OnTop(arg);
        }
        public void ClearCache(UIEventArgs arg)
        {
            while (UICache.Count != 0)
            {
                UIPanel p = UICache.Pop();
                if (!IsInStack(p))
                {
                    panelDic[p.GetType()].Remove(p);
                    (p as IUIPanel).OnClear(arg);
                }
            }
        }

        public UIPanel Get(Type type, string path, UIPanelLayer layer, string name, UIEventArgs arg, bool loadNew = false)
        {
            if (!type.IsSubclassOf(typeof(UIPanel)))
            {
                Log.E(string.Format("{0} is Not UIpanel", type));
                return default(UIPanel);
            }
            //if (UICache.Count > 0) ClearCache(arg);

            if (loadNew || !panelDic.ContainsKey(type))
            {
                UIPanel ui = Load(type, path, layer, name, arg);
                Push(ui, arg);
                return ui;
            }
            else
            {
                UIPanel ui = panelDic[type].Find((p) => { return p.PanelLayer == layer && p.PanelName == name; });
                if (ui == null) return Get(type, path, layer, name, arg, true);
                else
                {
                    Push(ui, arg); return ui;
                }
            }
        }
        public T Get<T>(string path, UIPanelLayer layer, string name, UIEventArgs arg, bool loadNew = false) where T : UIPanel
        {
            return (T)Get(typeof(T), path, layer, name, arg, loadNew);
        }


    }
}
