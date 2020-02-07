/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-13
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEditor;
using IFramework.GUITool;
using UnityEngine;
using System.Linq;

namespace IFramework
{
    [EditorWindowCache("IFramework.UIModule")]
	partial class UIMoudleWindow:EditorWindow,ILayoutGUIDrawer
	{
        private UIModule moudle;
        private SearchFieldDrawer searcher;
        [SerializeField] private string searchText = string.Empty;
        [SerializeField] private bool ShowBGBG = true;
        [SerializeField] private bool ShowBackGround = true;
        [SerializeField] private bool ShowAnimationUnderPage = true;
        [SerializeField] private bool ShowCommon = true;
        [SerializeField] private bool ShowAnimationOnPage = true;
        [SerializeField] private bool ShowPop = true;
        [SerializeField] private bool ShowGuide = true;
        [SerializeField] private bool ShowToast = true;
        [SerializeField] private bool ShowTop = true;
        [SerializeField] private bool ShowTopTop = true;

        [SerializeField] private bool IsDicOn = true;
        [SerializeField] private bool IsStackOn = true;
        [SerializeField] private bool IsCacheOn = true;

        [SerializeField] private string SearchTxt = string.Empty;
        [SerializeField] private Vector2 stackSroll, cacheScroll,scroll0;
        private const float typeWith = 200;
        private const float paneltypeWith = 100;
        private class Styles
        {
            public static GUIStyle BoldLabel = EditorStyles.boldLabel;
            public static GUIStyle toolbarButton = EditorStyles.toolbarButton;
            public static GUIStyle toolbar = EditorStyles.toolbar;
            public static GUIStyle searchField = GUI.skin.FindStyle("ToolbarSeachTextField");
            public static GUIStyle cancelBtn = GUI.skin.FindStyle("ToolbarSeachCancelButton");
            public static GUIStyle Fold = new GUIStyle(GUI.skin.FindStyle("ToolbarDropDown"));
            public static GUIStyle FoldOut = EditorStyles.foldout;

            static Styles()
            {
                Fold.fixedHeight = BoldLabel.fixedHeight;
                BoldLabel.fontSize = 10;
            }
        }
        private class Contents
        {
            public static GUIContent BGBG = new GUIContent("BGBG", "BGBG");
            public static GUIContent BackGround = new GUIContent("BG", "BackGround");
            public static GUIContent AUP = new GUIContent("AUP", "AnimationUnderPage");
            public static GUIContent Common = new GUIContent("Com", "Common");
            public static GUIContent AOP = new GUIContent("AOP", "AnimationOnPage");
            public static GUIContent Popup = new GUIContent("Pop", "Popup");
            public static GUIContent Guide = new GUIContent("Guide", "Guide");
            public static GUIContent Toast = new GUIContent("Toast", "Toast");
            public static GUIContent Top = new GUIContent("Top", "Top");
            public static GUIContent TopTop = new GUIContent("TopTop", "TopTop");
        }
    }
    partial class UIMoudleWindow 
    {
        private void OnEnable()
        {
            searcher = new SearchFieldDrawer() {
                value= searchText
            };
            searcher.onEndEdit += (str) => {
                searchText = str;
                if (!EditorApplication.isPlaying) return;
                moudle = Framework.env1.modules.FindModule<UIModule>(str);
                if (moudle==null)
                {
                    ShowNotification(new GUIContent("Not Find,Moudle Must Bind Framework First"));
                }
            };
        }
        private void OnGUI()
        {
            this.BeginHorizontal()
                .Label("Search UIMoudle ")
                .FlexibleSpace()
                .Label("", GUILayout.MaxWidth(300))
                .Pan(() => {
                    searcher.OnGUI(GUILayoutUtility.GetLastRect());
                })
                .EndHorizontal();
            this.BeginHorizontal(Styles.toolbar)
                    .Toggle(ref ShowBGBG, Contents.BGBG, Styles.toolbarButton)
                    .Toggle(ref ShowBackGround, Contents.BackGround, Styles.toolbarButton)
                    .Toggle(ref ShowAnimationUnderPage, Contents.AUP, Styles.toolbarButton)
                    .Toggle(ref ShowCommon, Contents.Common, Styles.toolbarButton)
                    .Toggle(ref ShowAnimationOnPage, Contents.AOP, Styles.toolbarButton)
                    .Toggle(ref ShowPop, Contents.Popup, Styles.toolbarButton)
                    .Toggle(ref ShowGuide, Contents.Guide, Styles.toolbarButton)
                    .Toggle(ref ShowToast, Contents.Toast, Styles.toolbarButton)
                    .Toggle(ref ShowTop, Contents.Top, Styles.toolbarButton)
                    .Toggle(ref ShowTopTop, Contents.TopTop, Styles.toolbarButton)
                .EndHorizontal()
                .BeginHorizontal(Styles.toolbar)
                    .Label("Search Panel By Name")
                    .FlexibleSpace()
                    .TextField(ref SearchTxt, Styles.searchField, GUILayout.MaxWidth(300))
                    .Button(() => {
                        SearchTxt = "";
                        GUI.FocusControl(null);
                    }, "", Styles.cancelBtn)
                .EndHorizontal()
                .BeginHorizontal(Styles.toolbar)
                    .Label("Name")
                    .Label("Type", GUILayout.MaxWidth(typeWith))
                    .Label("PanelType", GUILayout.Width(paneltypeWith))
                .EndHorizontal();


            if (moudle == null || moudle.disposed || !EditorApplication.isPlaying) return;
            this.BeginHorizontal(Styles.Fold, GUILayout.Height(20))
                        .Space(10)
                        .Foldout(ref IsDicOn, "Panel Dictionary (PreFabs)", true, Styles.FoldOut)
                        .FlexibleSpace()
                .EndHorizontal()
                .Pan(() => {
                    if (!IsDicOn) return;
                    this.Space(2)
                        .BeginScrollView(ref scroll0)
                            .BeginVertical()
                                 .Pan(() =>
                                 {
                                     foreach (var item in moudle.panelDic.Keys)
                                     {
                                         var list = moudle.panelDic[item];
                                         this.BeginHorizontal(Styles.toolbar)
                                                  .Label(item.FullName)
                                             .EndHorizontal()
                                             .BeginHorizontal()
                                                 .Space(10)
                                                 .BeginVertical()
                                                        .Pan(() =>
                                                        {
                                                            list.ForEach((panel) =>
                                                            {
                                                                bool canshow = FitCanShow(panel);

                                                                if (!canshow) return;
                                                                Rect rect;
                                                                this.BeginHorizontal()
                                                                            .Space(10)
                                                                            .EBeginHorizontal(out rect, EditorStyles.toolbar)
                                                                                .Space(10)
                                                                                .Label(panel.PanelName)
                                                                                .Label(panel.GetType().ToString(), GUILayout.MaxWidth(typeWith))
                                                                                .Label(panel.PanelLayer.ToString(), GUILayout.Width(paneltypeWith))
                                                                            .EEndHorizontal()
                                                                            .Pan(() =>
                                                                            {
                                                                                if (Event.current.clickCount == 2 && rect.Contains(Event.current.mousePosition))
                                                                                    Selection.activeGameObject = panel.gameObject;
                                                                            })
                                                                        .EndHorizontal();

                                                            });
                                                        })
                                                  .EndVertical()
                                             .EndHorizontal();
                                         ;
                                     }
                                 })
                            .EndVertical()
                        .LayoutEndScrollView();
                })


                .Space(10)
                .BeginHorizontal(Styles.toolbar)
                    .Label("UI Load Order", Styles.BoldLabel)
                .EndHorizontal()
                .Space(2)
                .BeginVertical()
                    .BeginHorizontal(Styles.Fold, GUILayout.Height(20))
                        .Space(10)
                        .Foldout(ref IsStackOn, string.Format("Stack  Count:  {0}", moudle.StackCount), true, Styles.FoldOut)
                        .FlexibleSpace()
                    .EndHorizontal()
                    .Pan(() => {
                        if (moudle.StackCount <= 0) return;
                        var o = moudle.UIStack.ToList();
                        //GUI.enabled = false;
                        if (IsStackOn)
                        {
                            this.DrawScrollView(() => {
                                for (int i = moudle.UIStack.Count - 1; i >= 0; i--)
                                {
                                    GUI.enabled = i == 0;
                                    bool canshow = FitCanShow(o[i]);
                                    if (!canshow) continue;
                                    Rect rect;
                                    this.BeginHorizontal()
                                                .Space(10)
                                                .EBeginHorizontal(out rect, EditorStyles.toolbar)
                                                    .Space(10)
                                                    .Label(o[i].PanelName)
                                                    .Label(o[i].GetType().ToString(), GUILayout.MaxWidth(typeWith))
                                                    .Label(o[i].PanelLayer.ToString(), GUILayout.Width(paneltypeWith))
                                                .EEndHorizontal()
                                                .Pan(() =>
                                                {
                                                    if (Event.current.clickCount == 2 && rect.Contains(Event.current.mousePosition))
                                                        Selection.activeGameObject = o[i].gameObject;
                                                })
                                            .EndHorizontal();
                                }
                                GUI.enabled = true;
                            }, ref stackSroll);
                        }
                    })
                    .BeginHorizontal(Styles.Fold, GUILayout.Height(20))
                        .Space(10)
                        .Foldout(ref IsCacheOn, string.Format("Cache  Count:  {0}", moudle.CacheCount), true, Styles.FoldOut)
                        .FlexibleSpace()
                    .EndHorizontal()
                    .Pan(() => {
                        if (moudle.UICache.Count <= 0) return;
                        var o = moudle.UICache.ToList();
                        if (IsCacheOn)
                        {
                            this.DrawScrollView(() =>
                            {
                                GUI.enabled = false;
                                for (int i = 0; i < moudle.UICache.Count; i++)
                                {
                                    bool canshow = FitCanShow(o[i]);
                                    if (!canshow) continue;
                                    Rect rect;
                                    this.BeginHorizontal()
                                                .Space(10)
                                                .EBeginHorizontal(out rect, EditorStyles.toolbar)
                                                    .Space(10)
                                                    .Label(o[i].PanelName)
                                                    .Label(o[i].GetType().ToString(), GUILayout.MaxWidth(typeWith))
                                                    .Label(o[i].PanelLayer.ToString(), GUILayout.Width(paneltypeWith))
                                                .EEndHorizontal()
                                                .Pan(() =>
                                                {
                                                    if (Event.current.clickCount == 2 && rect.Contains(Event.current.mousePosition))
                                                        Selection.activeGameObject = o[i].gameObject;
                                                })
                                            .EndHorizontal();

                                }
                                GUI.enabled = true;

                            }, ref cacheScroll);
                        }
                       
                    })
                .EndVertical()
                .FlexibleSpace()
                .Space(20);
            TestButton();
            this.Repaint();
        }

        private bool FitCanShow(UIPanel panel)
        {
            bool canshow = false;
            switch (panel.PanelLayer)
            {
                case UIPanelLayer.BGBG: canshow = ShowBGBG; break;
                case UIPanelLayer.Background: canshow = ShowBackGround; break;
                case UIPanelLayer.AnimationUnderPage: canshow = ShowAnimationUnderPage; break;
                case UIPanelLayer.Common: canshow = ShowCommon; break;
                case UIPanelLayer.AnimationOnPage: canshow = ShowAnimationOnPage; break;
                case UIPanelLayer.PopUp: canshow = ShowPop; break;
                case UIPanelLayer.Guide: canshow = ShowGuide; break;
                case UIPanelLayer.Toast: canshow = ShowToast; break;
                case UIPanelLayer.Top: canshow = ShowTop; break;
                case UIPanelLayer.TopTop: canshow = ShowTopTop; break;
            }
            canshow &= panel.PanelName.ToLower().Contains(SearchTxt.ToLower());
            return canshow;
        }
        private void TestButton()
        {
            if (moudle.LoaderCount <= 0)
                EditorGUILayout.HelpBox("Must Have Loader ", UnityEditor.MessageType.Error);
            EditorGUILayout.LabelField("LoaderCount: " + moudle.LoaderCount);
            using (new EditorGUI.DisabledScope(!EditorApplication.isPlaying))
            {
                GUILayout.BeginHorizontal();
                using (new EditorGUI.DisabledScope(moudle.StackCount <= 0))
                {
                    if (GUILayout.Button("GoBack", GUILayout.Height(30)))
                    {
                        moudle.GoBack(new UIEventArgs() { isInspectorBtn = true });
                    }
                }
                using (new EditorGUI.DisabledScope(moudle.CacheCount <= 0))
                {
                    if (GUILayout.Button("GoForWard", GUILayout.Height(30)))
                    {
                        moudle.GoForWard(new UIEventArgs() { isInspectorBtn = true });
                    }
                }
                GUILayout.EndHorizontal();
                using (new EditorGUI.DisabledScope(moudle.CacheCount <= 0))
                {
                    if (GUILayout.Button("ClearCache", GUILayout.Height(30)))
                    {
                        moudle.ClearCache(new UIEventArgs() { isInspectorBtn = true });
                    }
                }
            }
        }

    }
}
