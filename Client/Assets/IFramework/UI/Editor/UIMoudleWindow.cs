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
using System;
using System.Collections.Generic;
using System.IO;
using IFramework.Modules.MVVM;
namespace IFramework
{

    [EditorWindowCache("IFramework.UIModule")]
	partial class UIMoudleWindow:EditorWindow,ILayoutGUIDrawer
	{
        [SerializeField] ViewType viewType;
        [SerializeField] private RunTimeView runTimeView;
        [SerializeField] private MVP_GenCodeView  mVP_GenCodeView;
        [SerializeField] private MVVM_GenCodeView   mVVM_GenCodeView;

        static string genpath;
        private enum ViewType
        {
            MVVM_GenCode,  MVP_GenCode, Runtime,
        }
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

        private class SearchablePopup : PopupWindowContent
        {
            public class InnerSearchField : FocusAbleGUIDrawer
            {
                private class Styles
                {
                    public static GUIStyle SearchTextFieldStyle = new GUIStyle("ToolbarSeachTextField");
                    public static GUIStyle SearchCancelButtonStyle = new GUIStyle("SearchCancelButton");
                    public static GUIStyle SearchCancelButtonEmptyStyle = new GUIStyle("SearchCancelButtonEmpty");
                }

                public string OnGUI(Rect position, string value)
                {
                    GUIStyle cancelBtnStyle = string.IsNullOrEmpty(value) ? Styles.SearchCancelButtonEmptyStyle : Styles.SearchCancelButtonStyle;
                    position.width -= cancelBtnStyle.fixedWidth;

                    Styles.SearchTextFieldStyle.fixedHeight = position.height;
                    cancelBtnStyle.fixedHeight = position.height;

                    Styles.SearchTextFieldStyle.alignment = TextAnchor.MiddleLeft;
                    while (Styles.SearchTextFieldStyle.lineHeight < position.height - 15)
                    {
                        Styles.SearchTextFieldStyle.fontSize++;
                    }
                    GUI.SetNextControlName(FocusID);

                    value = GUI.TextField(new Rect(position.x,
                                                                     position.y + 1,
                                                                     position.width,
                                                                     position.height - 1),
                                                                     value,
                                                                     Styles.SearchTextFieldStyle);
                    if (GUI.Button(new Rect(position.x + position.width,
                                            position.y + 1,
                                            cancelBtnStyle.fixedWidth,
                                            cancelBtnStyle.fixedHeight
                                            ),
                                            GUIContent.none,
                                            cancelBtnStyle))
                    {
                        value = string.Empty;
                        GUI.changed = true;
                        GUIUtility.keyboardControl = 0;
                    }


                    Event e = Event.current;
                    if (position.Contains(e.mousePosition))
                    {
                        if (!Focused)
                            if ((e.type == EventType.MouseDown /*&& e.clickCount == 2*/) /*|| e.keyCode == KeyCode.F2*/)
                            {
                                Focused = true;
                                GUIFocusControl.Focus(this);
                                if (e.type != EventType.Repaint && e.type != EventType.Layout)
                                    Event.current.Use();
                            }
                    }
                    //if ((/*e.keyCode == KeyCode.Return ||*/ e.keyCode == KeyCode.Escape || e.character == '\n'))
                    //{
                    //    GUIFocusControl.Focus(null, Focused);
                    //    Focused = false;
                    //    if (e.type != EventType.Repaint && e.type != EventType.Layout)
                    //        Event.current.Use();
                    //}
                    return value;
                }

            }
            private const float rowHeight = 16.0f;
            private const float rowIndent = 8.0f;
            public static void Show(Rect activatorRect, string[] options, int current, Action<int, string> onSelectionMade)
            {
                SearchablePopup win = new SearchablePopup(options, current, onSelectionMade);
                PopupWindow.Show(activatorRect, win);
            }
            private static void Repaint() { EditorWindow.focusedWindow.Repaint(); }
            private static void DrawBox(Rect rect, Color tint)
            {
                Color c = GUI.color;
                GUI.color = tint;
                GUI.Box(rect, "", Selection);
                GUI.color = c;
            }
            private class FilteredList
            {
                public struct Entry
                {
                    public int Index;
                    public string Text;
                }
                private readonly string[] allItems;
                public FilteredList(string[] items)
                {
                    allItems = items;
                    Entries = new List<Entry>();
                    UpdateFilter("");
                }
                public string Filter { get; private set; }
                public List<Entry> Entries { get; private set; }
                public int Count { get { return allItems.Length; } }
                public bool UpdateFilter(string filter)
                {
                    if (Filter == filter)
                        return false;
                    Filter = filter;
                    Entries.Clear();
                    for (int i = 0; i < allItems.Length; i++)
                    {
                        if (string.IsNullOrEmpty(Filter) || allItems[i].ToLower().Contains(Filter.ToLower()))
                        {
                            Entry entry = new Entry
                            {
                                Index = i,
                                Text = allItems[i]
                            };
                            if (string.Equals(allItems[i], Filter, StringComparison.CurrentCultureIgnoreCase))
                                Entries.Insert(0, entry);
                            else
                                Entries.Add(entry);
                        }
                    }
                    return true;
                }
            }

            private readonly Action<int, string> onSelectionMade;
            private readonly int currentIndex;
            private readonly FilteredList list;
            private Vector2 scroll;
            private int hoverIndex;
            private int scrollToIndex;
            private float scrollOffset;
            private static GUIStyle Selection = "SelectionRect";

            private SearchablePopup(string[] names, int currentIndex, Action<int, string> onSelectionMade)
            {
                list = new FilteredList(names);
                this.currentIndex = currentIndex;
                this.onSelectionMade = onSelectionMade;
                hoverIndex = currentIndex;
                scrollToIndex = currentIndex;
                scrollOffset = GetWindowSize().y - rowHeight * 2;
            }

            public override void OnOpen()
            {
                base.OnOpen();
                EditorApplication.update += Repaint;
            }
            public override void OnClose()
            {
                base.OnClose();
                EditorApplication.update -= Repaint;
            }

            public override Vector2 GetWindowSize()
            {
                return new Vector2(base.GetWindowSize().x*2,
                    Mathf.Min(600, list.Count * rowHeight + EditorStyles.toolbar.fixedHeight));
            }

            public override void OnGUI(Rect rect)
            {
                Rect searchRect = new Rect(0, 0, rect.width, EditorStyles.toolbar.fixedHeight);
                Rect scrollRect = Rect.MinMaxRect(0, searchRect.yMax, rect.xMax, rect.yMax);

                HandleKeyboard();
                DrawSearch(searchRect);
                DrawSelectionArea(scrollRect);
            }
            private InnerSearchField searchField = new InnerSearchField();
            private void DrawSearch(Rect rect)
            {
                GUI.Label(rect, "", EditorStyles.toolbar);
                if (list.UpdateFilter(searchField.OnGUI(rect.Zoom(AnchorType.MiddleCenter, -2), list.Filter)))
                {
                    hoverIndex = 0;
                    scroll = Vector2.zero;
                }
            }

            private void DrawSelectionArea(Rect scrollRect)
            {
                Rect contentRect = new Rect(0, 0,
                    scrollRect.width - GUI.skin.verticalScrollbar.fixedWidth,
                    list.Entries.Count * rowHeight);

                scroll = GUI.BeginScrollView(scrollRect, scroll, contentRect);

                Rect rowRect = new Rect(0, 0, scrollRect.width, rowHeight);

                for (int i = 0; i < list.Entries.Count; i++)
                {
                    if (scrollToIndex == i &&
                        (Event.current.type == EventType.Repaint
                         || Event.current.type == EventType.Layout))
                    {
                        Rect r = new Rect(rowRect);
                        r.y += scrollOffset;
                        GUI.ScrollTo(r);
                        scrollToIndex = -1;
                        scroll.x = 0;
                    }

                    if (rowRect.Contains(Event.current.mousePosition))
                    {
                        if (Event.current.type == EventType.MouseMove ||
                            Event.current.type == EventType.ScrollWheel)
                            hoverIndex = i;
                        if (Event.current.type == EventType.MouseDown)
                        {
                            onSelectionMade(list.Entries[i].Index, list.Entries[i].Text);
                            EditorWindow.focusedWindow.Close();
                        }
                    }

                    DrawRow(rowRect, i);

                    rowRect.y = rowRect.yMax;
                }

                GUI.EndScrollView();
            }

            private void DrawRow(Rect rowRect, int i)
            {
                if (list.Entries[i].Index == currentIndex)
                    DrawBox(rowRect, Color.cyan);
                else if (i == hoverIndex)
                    DrawBox(rowRect, Color.white);
                Rect labelRect = new Rect(rowRect);
                labelRect.xMin += rowIndent;
                GUI.Label(labelRect, list.Entries[i].Text);
            }
            private void HandleKeyboard()
            {
                Event e = Event.current;
                if (e.type == EventType.KeyDown)
                {
                    if (e.keyCode == KeyCode.DownArrow)
                    {
                        hoverIndex = Mathf.Min(list.Entries.Count - 1, hoverIndex + 1);
                        Event.current.Use();
                        scrollToIndex = hoverIndex;
                        scrollOffset = rowHeight;
                    }
                    if (e.keyCode == KeyCode.UpArrow)
                    {
                        hoverIndex = Mathf.Max(0, hoverIndex - 1);
                        Event.current.Use();
                        scrollToIndex = hoverIndex;
                        scrollOffset = -rowHeight;
                    }
                    if (e.keyCode == KeyCode.Return || e.character == '\n')
                    {
                        if (hoverIndex >= 0 && hoverIndex < list.Entries.Count)
                        {
                            onSelectionMade(list.Entries[hoverIndex].Index, list.Entries[hoverIndex].Text);
                            EditorWindow.focusedWindow.Close();
                        }
                    }
                    if (e.keyCode == KeyCode.Escape)
                    {
                        EditorWindow.focusedWindow.Close();
                    }
                }
            }
        }
        [Serializable]
        private class RunTimeView:ILayoutGUIDrawer
        {
            private const float typeWith = 200;
            private const float paneltypeWith = 100;
            private SearchFieldDrawer searcher;
            private UIModule moudle;
            [SerializeField] private Vector2 stackSroll, cacheScroll;
            [SerializeField] private string searchText_module = string.Empty;
            [SerializeField] private string searchTxt_panel = string.Empty;

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

            [SerializeField] private bool IsStackOn = true;
            [SerializeField] private bool IsCacheOn = true;
            public void OnEnable()
            {
                searcher = new SearchFieldDrawer()
                {
                    value = searchText_module
                };
                searcher.onEndEdit += (str) => {
                    searchText_module = str;
                    if (!EditorApplication.isPlaying) return;
                    moudle = Framework.env1.modules.FindModule<UIModule>(str);
                    if (moudle == null)
                    {
                       EditorWindow.focusedWindow. ShowNotification(new GUIContent("Not Find,Moudle Must Bind Framework First"));
                    }
                };
            }
            public void OnGUI()
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
                        .TextField(ref searchTxt_panel, Styles.searchField, GUILayout.MaxWidth(300))
                        .Button(() => {
                            searchTxt_panel = "";
                            GUI.FocusControl(null);
                        }, "", Styles.cancelBtn)
                    .EndHorizontal()
                    .BeginHorizontal(Styles.toolbar)
                        .Label("Name")
                        .Label("Type", GUILayout.MaxWidth(typeWith))
                        .Label("PanelType", GUILayout.Width(paneltypeWith))
                    .EndHorizontal();


                if (moudle == null || moudle.disposed || !EditorApplication.isPlaying) return;
                this.BeginHorizontal(Styles.toolbar)
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
                canshow &= panel.PanelName.ToLower().Contains(searchTxt_panel.ToLower());
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
                            moudle.GoBack();
                        }
                    }
                    using (new EditorGUI.DisabledScope(moudle.CacheCount <= 0))
                    {
                        if (GUILayout.Button("GoForWard", GUILayout.Height(30)))
                        {
                            moudle.GoForWard();
                        }
                    }
                    GUILayout.EndHorizontal();
                    using (new EditorGUI.DisabledScope(moudle.CacheCount <= 0))
                    {
                        if (GUILayout.Button("ClearCache", GUILayout.Height(30)))
                        {
                            moudle.ClearCache();
                        }
                    }
                }
            }
        }
        [Serializable]
        private class MVP_GenCodeView:ILayoutGUIDrawer
        {
            private int hashID;
            private bool DropdownButton(int id, Rect position, GUIContent content)
            {
                Event e = Event.current;
                switch (e.type)
                {
                    case EventType.MouseDown:
                        if (position.Contains(e.mousePosition) && e.button == 0)
                        {
                            Event.current.Use();
                            return true;
                        }
                        break;
                    case EventType.KeyDown:
                        if (GUIUtility.keyboardControl == id && e.character == '\n')
                        {
                            Event.current.Use();
                            return true;
                        }
                        break;
                    case EventType.Repaint:
                        Styles.BoldLabel.Draw(position, content, id, false);
                        break;
                }
                return false;
            }
            private List<string> panelTypes;
            private string panelType;
            private string PanelGenDir;
            private string UIMapDir;
            [SerializeField] string UIMapName = "UIMap_MVP";

            string UIMap_CSName { get { return UIMapName.Append(".cs"); } }
            string uimapGenPath { get { return genpath.CombinePath("MapGen_MVP.txt"); } } 
            string sensorGenPath { get { return genpath.CombinePath("SensorGen_MVP.txt"); } }
            string policyGenPath { get { return genpath.CombinePath("PolicyGen_MVP.txt"); } }
            string excutorGenPath { get { return genpath.CombinePath("ExcutorGen_MVP.txt"); } }
            string viewGenPath { get { return genpath.CombinePath("ViewGen_MVP.txt"); } }
            string EnityGenPath { get { return genpath.CombinePath("EnityGen_MVP.txt"); } }
            


            public void OnGUI()
            {
                this.Space(5)
                .DrawHorizontal(() => {
                    this.Label("Check UIMap Script Name", Styles.toolbar);
                    this.TextField(ref UIMapName);
                });
                this.DrawHorizontal(() =>
                {
                    this.Label("Drag UIMap Gen Directory", Styles.toolbar);
                    this.Label(UIMapDir);
                    Rect rect = GUILayoutUtility.GetLastRect();
                    if (string.IsNullOrEmpty(UIMapDir))
                        rect.DrawOutLine(10, Color.red);
                    else
                        rect.DrawOutLine(2, Color.black);
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        var drag = DragAndDropUtil.Drag(Event.current, rect);
                        if (drag.Finsh && drag.EnterArera && drag.paths.Length == 1)
                        {
                            string path = drag.paths[0];
                            if (path.Contains("Assets"))
                            {
                                if (path.IsDirectory())
                                    UIMapDir = path;
                                else
                                    UIMapDir = path.GetDirPath();
                            }

                        }
                    }

                })
            .DrawHorizontal(() =>
            {
                this.FlexibleSpace()
                    .Button(() =>
                    {
                        if (string.IsNullOrEmpty(UIMapDir))
                        {
                            EditorWindow.focusedWindow.ShowNotification(new GUIContent("Set UI Map Gen Dir "));
                            return;
                        }
                        //string uimapGenPath = genpath.CombinePath("MapGen_MVP.txt");
                        CreateUIMapGen(uimapGenPath);
                        WriteTxt(UIMapDir.CombinePath(UIMap_CSName), uimapGenPath);
                        AssetDatabase.Refresh();

                    }, "Copy UIMap From Source");

            })
            .Space(30);


                if (hashID == 0) hashID = "MVP_GenCodeView".GetHashCode();
                this.DrawHorizontal(() =>
                {
                    this.Label("Click Choose Panel Type", Styles.toolbar);
                    GUILayout.Label("");
                    Rect pos = GUILayoutUtility.GetLastRect();

                    int ctrlId = GUIUtility.GetControlID(hashID, FocusType.Keyboard, pos);
                    {
                        if (DropdownButton(ctrlId, pos, new GUIContent(string.Format("PanelType: {0}", panelType))))
                        {
                            if (panelTypes == null)
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Fresh Panel Types"));
                                return;
                            }
                            int index = -1;
                            for (int i = 0; i < panelTypes.Count; i++)
                            {
                                if (panelTypes[i] == panelType)
                                {
                                    index = i; break;
                                }
                            }
                            SearchablePopup.Show(pos, panelTypes.ToArray(), index, (i, str) =>
                            {
                                panelType = str;
                                EditorWindow.focusedWindow.Repaint();
                            });
                        }
                    }
                })
                    .Space(10)
                    .DrawHorizontal(() =>
                    {
                        this.Label("Drag Panel Gen Directory", Styles.toolbar);
                        this.Label(PanelGenDir);
                        Rect rect = GUILayoutUtility.GetLastRect();
                        if (string.IsNullOrEmpty(PanelGenDir))
                            rect.DrawOutLine(10, Color.red);
                        else
                            rect.DrawOutLine(2, Color.black);
                        if (rect.Contains(Event.current.mousePosition))
                        {
                            var drag = DragAndDropUtil.Drag(Event.current, rect);
                            if (drag.Finsh && drag.EnterArera && drag.paths.Length == 1)
                            {
                                string path = drag.paths[0];
                                if (path.Contains("Assets"))
                                {
                                    if (path.IsDirectory())
                                        PanelGenDir = path;
                                    else
                                        PanelGenDir = path.GetDirPath();
                                }

                            }
                        }

                    })
                    .Space(10)
                    .DrawHorizontal(() =>
                    {
                        this.Button(() =>
                        {
                            panelTypes = typeof(UIPanel).GetSubTypesInAssemblys().Select((type) =>
                            {
                                return type.FullName;
                            }).ToList();
                        }, "Fresh Panel Types")
                           .Space(20)
                           .Button(() =>
                           {
                               if (string.IsNullOrEmpty(UIMapDir))
                               {
                                   EditorWindow.focusedWindow.ShowNotification(new GUIContent("Set UI Map Gen Dir "));
                                   return;
                               }
                               if (!File.Exists(UIMapDir.CombinePath(UIMap_CSName)))
                               {
                                   EditorWindow.focusedWindow.ShowNotification(new GUIContent("Copy UI Map"));
                                   return;
                               }
                               if (string.IsNullOrEmpty(panelType))
                               {
                                   EditorWindow.focusedWindow.ShowNotification(new GUIContent("Choose UI Panel Type "));
                                   return;
                               }
                               if (string.IsNullOrEmpty(PanelGenDir))
                               {
                                   EditorWindow.focusedWindow.ShowNotification(new GUIContent("Set UI Panel Gen Dir "));

                                   return;
                               }
                               string type = panelType.Split('.').ToList().Last();
                               //string sensorGenPath = genpath.CombinePath("SensorGen_MVP.txt");
                               //string policyGenPath = genpath.CombinePath("PolicyGen_MVP.txt");

                               //string excutorGenPath = genpath.CombinePath("ExcutorGen_MVP.txt");
                               //string viewGenPath = genpath.CombinePath("ViewGen_MVP.txt");
                               //string EnityGenPath = genpath.CombinePath("EnityGen_MVP.txt");

                               CreateSensorGen(sensorGenPath);
                               CreatePolicyGen(policyGenPath);
                               CreatePolicyExcutorGen(excutorGenPath);
                               CreateViewGen(viewGenPath);
                               CreateEnityGen(EnityGenPath);

                               WriteTxt(PanelGenDir.CombinePath(type.Append("Sensor.cs")), sensorGenPath);
                               WriteTxt(PanelGenDir.CombinePath(type.Append("Policy.cs")), policyGenPath);
                               WriteTxt(PanelGenDir.CombinePath(type.Append("Excutor.cs")), excutorGenPath);
                               WriteTxt(PanelGenDir.CombinePath(type.Append("View_MVP.cs")), viewGenPath);
                               WriteTxt(PanelGenDir.CombinePath(type.Append("Enity.cs")), EnityGenPath);
                               WriteMap(UIMapDir.CombinePath(UIMap_CSName), UIMapDir.CombinePath(UIMap_CSName));
                               AssetDatabase.Refresh();
                           }, "Gen");
                    });

            }
            private static void CreateUIMapGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# ");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tpublic static Dictionary<Type, Tuple<Type, Type, Type, Type, Type>> map =");
                        sw.WriteLine("\t\tnew Dictionary<Type, Tuple<Type, Type, Type, Type, Type>>()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("//ToDo");
                        sw.WriteLine("\t\t};");
                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }

                }
                AssetDatabase.Refresh();
            }
            private static void CreateEnityGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : UIEnity");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t}");


                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }

                }
                AssetDatabase.Refresh();
            }
            private static void CreateSensorGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : UISensor_MVP");
                        sw.WriteLine("\t{");

                        sw.WriteLine("\t\tprotected override void OnClear()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");

                        sw.WriteLine("\t\tprotected override void OnLoad()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("\t\tprotected override void OnPop(UIEventArgs arg)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("\t\tprotected override void OnPress(UIEventArgs arg)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("\t\tprotected override void OnTop(UIEventArgs arg)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");

                        sw.WriteLine("\t}");


                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }

                }
                AssetDatabase.Refresh();
            }
            private static void CreatePolicyGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : UIPolicy_MVP");
                        sw.WriteLine("\t{");

                        sw.WriteLine("\t\tprotected override void OnSensor(int code, IEventArgs args, object[] param)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");

                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }
                }
                AssetDatabase.Refresh();
            }
            private static void CreatePolicyExcutorGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : UIExecutor_MVP");
                        sw.WriteLine("\t{");

                        sw.WriteLine("\t\tprotected override void OnPolicy(int code, IEventArgs args, object[] param)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");

                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }
                }
                AssetDatabase.Refresh();
            }
            private static void CreateViewGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : UIView_MVP");
                        sw.WriteLine("\t{");

                        sw.WriteLine("\t\tprotected override void OnPolicyPolicyExecutor(int code, IEventArgs args, object[] param)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t");
                        sw.WriteLine("\t\t}");

                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }
                }
                AssetDatabase.Refresh();
            }
            private static void WriteTxt(string writePath, string sourcePath)
            {
                var txt = File.ReadAllText(sourcePath);
                txt = txt.Replace("#User#", EditorProjectConfig.UserName)
                         .Replace("#UserSCRIPTNAME#", Path.GetFileNameWithoutExtension(writePath))
                           .Replace("#UserNameSpace#", EditorProjectConfig.NameSpace)
                           .Replace("#UserVERSION#", EditorProjectConfig.Version)
                           .Replace("#UserDescription#", EditorProjectConfig.Description)
                           .Replace("#UserUNITYVERSION#", Application.unityVersion)
                           .Replace("#UserDATE#", DateTime.Now.ToString("yyyy-MM-dd"));
                File.WriteAllText(writePath, txt, System.Text.Encoding.UTF8);
            }
            private void WriteMap(string writePath, string sourcePath)
            {
                var txt = File.ReadAllText(sourcePath);
                string flag = "//ToDo";

                string tmp = string.Format("typeof({0}),Tuple.Create(typeof({0}Enity),typeof({0}Sensor),typeof({0}Policy),typeof({0}Excutor),typeof({0}View))", panelType);
                tmp = tmp.Append("},\n").AppendHead("\t\t\t{").Append(flag);
                txt = txt.Replace(flag, tmp);
                File.WriteAllText(writePath, txt, System.Text.Encoding.UTF8);
            }
        }
        [Serializable]
        private class MVVM_GenCodeView : ILayoutGUIDrawer
        {
            [SerializeField] private string UIMapDir;
            [SerializeField] private string PanelGenDir;
            [SerializeField] string UIMapName = "UIMap_MVVM";

            private List<string> panelTypes;
            private string panelType;
            private List<string> modelTypes;
            private string modelType;

            string uimapGenPath { get { return genpath.CombinePath("MapGen_MVVM.txt"); } }
            string viewGenPath { get { return genpath.CombinePath("View_MVVM.txt"); } }
            string VMGenPath { get { return genpath.CombinePath("VM_MVVM.txt"); } }
            string UIMap_CSName { get { return UIMapName.Append( ".cs"); } }


            private int hashID;
            private bool DropdownButton(int id, Rect position, GUIContent content)
            {
                Event e = Event.current;
                switch (e.type)
                {
                    case EventType.MouseDown:
                        if (position.Contains(e.mousePosition) && e.button == 0)
                        {
                            Event.current.Use();
                            return true;
                        }
                        break;
                    case EventType.KeyDown:
                        if (GUIUtility.keyboardControl == id && e.character == '\n')
                        {
                            Event.current.Use();
                            return true;
                        }
                        break;
                    case EventType.Repaint:
                        Styles.BoldLabel.Draw(position, content, id, false);
                        break;
                }
                return false;
            }
            public void OnGUI()
            {
                
                this.Space(5)
                    .DrawHorizontal(() => {
                    this.Label("Check UIMap Script Name", Styles.toolbar);
                    this.TextField(ref UIMapName);
                });
                this.DrawHorizontal(() =>
                {
                    this.Label("Drag UIMap Gen Directory", Styles.toolbar);
                    this.Label(UIMapDir);
                    Rect rect = GUILayoutUtility.GetLastRect();
                    if (string.IsNullOrEmpty(UIMapDir))
                        rect.DrawOutLine(10, Color.red);
                    else
                        rect.DrawOutLine(2, Color.black);
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        var drag = DragAndDropUtil.Drag(Event.current, rect);
                        if (drag.Finsh && drag.EnterArera && drag.paths.Length == 1)
                        {
                            string path = drag.paths[0];
                            if (path.Contains("Assets"))
                            {
                                if (path.IsDirectory())
                                    UIMapDir = path;
                                else
                                    UIMapDir = path.GetDirPath();
                            }

                        }
                    }

                })
                  .DrawHorizontal(() =>
                  {
                      this.FlexibleSpace()
                          .Button(() =>
                          {
                              if (string.IsNullOrEmpty(UIMapDir))
                              {
                                  EditorWindow.focusedWindow.ShowNotification(new GUIContent("Set UI Map Gen Dir "));
                                  return;
                              }
                           //   string uimapGenPath = genpath.CombinePath("MapGen_MVVM.txt");
                              CreateUIMapGen(uimapGenPath);
                              WriteTxt(UIMapDir.CombinePath(UIMap_CSName), uimapGenPath,null);
                              AssetDatabase.Refresh();

                          }, "Copy UIMap From Source");

                  }).Space(30);
                if (hashID == 0) hashID = "MVVM_GenCodeView".GetHashCode();
                this.DrawHorizontal(() =>
                {
                    this.Label("Click Choose Panel Type", Styles.toolbar);
                    GUILayout.Label("");
                    Rect pos = GUILayoutUtility.GetLastRect();

                    int ctrlId = GUIUtility.GetControlID(hashID, FocusType.Keyboard, pos);
                    {
                        if (DropdownButton(ctrlId, pos, new GUIContent(string.Format("PanelType: {0}", panelType))))
                        {
                            if (panelTypes == null)
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Fresh Panel Types"));
                                return;
                            }
                            int index = -1;
                            for (int i = 0; i < panelTypes.Count; i++)
                            {
                                if (panelTypes[i] == panelType)
                                {
                                    index = i; break;
                                }
                            }
                            SearchablePopup.Show(pos, panelTypes.ToArray(), index, (i, str) =>
                            {
                                panelType = str;
                                EditorWindow.focusedWindow.Repaint();
                            });
                        }
                    }
                })
                    .Space(10)
                    .DrawHorizontal(() =>
                    {
                        this.Label("Click Choose Model Type", Styles.toolbar);
                        GUILayout.Label("");
                        Rect pos = GUILayoutUtility.GetLastRect();

                        int ctrlId = GUIUtility.GetControlID(hashID, FocusType.Keyboard, pos);
                        {
                            if (DropdownButton(ctrlId, pos, new GUIContent(string.Format("ModelType: {0}", modelType))))
                            {
                                if (modelTypes == null)
                                {
                                    EditorWindow.focusedWindow.ShowNotification(new GUIContent("Fresh Model Types"));
                                    return;
                                }
                                int index = -1;
                                for (int i = 0; i < modelTypes.Count; i++)
                                {
                                    if (modelTypes[i] == modelType)
                                    {
                                        index = i; break;
                                    }
                                }
                                SearchablePopup.Show(pos, modelTypes.ToArray(), index, (i, str) =>
                                {
                                    modelType = str;
                                    EditorWindow.focusedWindow.Repaint();
                                });
                            }
                        }
                    })
                    .Space(10)
                    .DrawHorizontal(() =>
                    {
                        this.Label("Drag Panel Gen Directory", Styles.toolbar);
                        this.Label(PanelGenDir);
                        Rect rect = GUILayoutUtility.GetLastRect();
                        if (string.IsNullOrEmpty(PanelGenDir))
                            rect.DrawOutLine(10, Color.red);
                        else
                            rect.DrawOutLine(2, Color.black);
                        if (rect.Contains(Event.current.mousePosition))
                        {
                            var drag = DragAndDropUtil.Drag(Event.current, rect);
                            if (drag.Finsh && drag.EnterArera && drag.paths.Length == 1)
                            {
                                string path = drag.paths[0];
                                if (path.Contains("Assets"))
                                {
                                    if (path.IsDirectory())
                                        PanelGenDir = path;
                                    else
                                        PanelGenDir = path.GetDirPath();
                                }

                            }
                        }

                    })
                    .Space(10)
                    .DrawHorizontal(() =>
                    {
                        this.Button(() =>
                        {
                            panelTypes = typeof(UIPanel).GetSubTypesInAssemblys().Select((type) =>
                            {
                                return type.FullName;
                            }).ToList();
                            modelTypes = typeof(IDataModel).GetSubTypesInAssemblys().Select((type) =>
                            {
                                return type.FullName;
                            }).ToList();
                        }, "Fresh Panel && Model Types")
                        .Space(20)
                        .Button(() =>
                        {
                            if (string.IsNullOrEmpty(UIMapDir))
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Set UI Map Gen Dir "));
                                return;
                            }
                            if (!File.Exists(UIMapDir.CombinePath(UIMap_CSName)))
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Copy UI Map"));
                                return;
                            }
                            if (string.IsNullOrEmpty(panelType))
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Choose UI Panel Type "));
                                return;
                            }
                            if (string.IsNullOrEmpty(modelType))
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Choose UI Model Type "));
                                return;
                            }
                            if (string.IsNullOrEmpty(PanelGenDir))
                            {
                                EditorWindow.focusedWindow.ShowNotification(new GUIContent("Set UI Panel Gen Dir "));
                                return;
                            }
                            string _modelType= modelType.Split('.').ToList().Last();
                            string paneltype = panelType.Split('.').ToList().Last();
                            string vmType = paneltype.Append("ViewModel");
                            string viewType = paneltype.Append("View");

                            //string viewPath = genpath.CombinePath("View_MVVM.txt");
                            //string VMPath = genpath.CombinePath("VM_MVVM.txt");
                                              

                            CreateViewGen(viewGenPath);
                            CreateVMGen(VMGenPath);

                            //v
                            WriteTxt(PanelGenDir.CombinePath(viewType.Append(".cs")), viewGenPath,
                                (str)=>{


                                    return str.Replace("#VMType#", vmType).Replace("#PanelType#", paneltype);
                                }
                                );
                            //vm
                            WriteTxt(PanelGenDir.CombinePath(vmType.Append(".cs")), VMGenPath,
                                    (str) => {
                                        string fieldStr = " ";
                                        string syncStr = " ";
                                        Type t = AppDomain.CurrentDomain.GetAssemblies()
                                                    .SelectMany((a)=> { return a.GetTypes(); })
                                                    .ToList().Find((type)=> { return type.FullName == modelType; });
                                        var fs= t.GetFields();
                                        for (int i = 0; i < fs.Length; i++)
                                        {
                                            var info = fs[i];
                                            fieldStr = WriteField(fieldStr, info.FieldType, info.Name);
                                            syncStr = WriteSyncStr(syncStr, info.Name);
                                        }
                                        
                                        return str.Replace("#ModelType#", _modelType)
                                                  .Replace("#FieldString#", fieldStr)
                                                  .Replace("#SyncModelValue#", syncStr);
                                    }
                                );

                               WriteMap(UIMapDir.CombinePath(UIMap_CSName), UIMapDir.CombinePath(UIMap_CSName));
                            AssetDatabase.Refresh();
                        }, "Gen");
                    });

            }
            private void WriteMap(string writePath, string sourcePath)
            {
                var txt = File.ReadAllText(sourcePath);
                string flag = "//ToDo";

                string tmp = string.Format("typeof({0}),Tuple.Create(typeof({1}),typeof({0}View),typeof({0}ViewModel))", panelType,modelType);
                tmp = tmp.Append("},\n").AppendHead("\t\t\t{").Append(flag);
                txt = txt.Replace(flag, tmp);
                File.WriteAllText(writePath, txt, System.Text.Encoding.UTF8);
            }

            private string WriteField(string result ,Type ft,string fn)
            {
                return result.Append(string.Format("\t\tprivate {0} _{1};\n", ft.Name, fn))
                                .Append(string.Format("\t\tpublic {0} {1}\n", ft.Name, fn))
                                .Append(string.Format("\t\t{0}\n","{"))
                                .Append(string.Format("\t\t\tget {0} return GetProperty(ref _{1}, this.GetPropertyName(() => _{1})); {2}\n", "{", fn, "}"))
                                .Append(string.Format("\t\t\tprivate set"))
                                .Append(string.Format("\t\t\t{0}\n","{"))
                                .Append(string.Format("\t\t\t\tTmodel.{0} = value;\n", fn))
                                .Append(string.Format("\t\t\t\tSetProperty(ref _{0}, value, this.GetPropertyName(() => _{0}));\n", fn))
                                .Append(string.Format("\t\t\t{0}\n","}"))
                                .Append(string.Format("\t\t{0}\n\n","}"));
            }
            private string WriteSyncStr(string result, string fn)
            {
                return result.Append(string.Format("\t\t\tthis.{0} = Tmodel.{0};\n", fn));

            }

            private void CreateVMGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : TUIViewModel_MVVM<#ModelType#>");
                        sw.WriteLine("\t{");

                        sw.WriteLine("#FieldString#");
                        sw.WriteLine("\t\tprotected override void SyncModelValue()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("#SyncModelValue#");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");

                      

                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }

                }
                AssetDatabase.Refresh();
            }

            private void CreateViewGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# : TUIView_MVVM<#VMType#, #PanelType#>");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tprotected override void BindProperty()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\tbase.BindProperty();");
                        sw.WriteLine("\t\t\t//ToDo");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");

                        sw.WriteLine("\t\tprotected override void OnClear()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");

                        sw.WriteLine("\t\tprotected override void OnLoad()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");

                        sw.WriteLine("\t\tprotected override void OnPop(UIEventArgs arg)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");

                        sw.WriteLine("\t\tprotected override void OnPress(UIEventArgs arg)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");

                        sw.WriteLine("\t\tprotected override void OnTop(UIEventArgs arg)");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t}");
                        sw.WriteLine("");
                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }

                }
                AssetDatabase.Refresh();
            }

            private static void WriteTxt(string writePath, string sourcePath,Func<string ,string> func)
            {
                var txt = File.ReadAllText(sourcePath);
                txt = txt.Replace("#User#", EditorProjectConfig.UserName)
                         .Replace("#UserSCRIPTNAME#", Path.GetFileNameWithoutExtension(writePath))
                           .Replace("#UserNameSpace#", EditorProjectConfig.NameSpace)
                           .Replace("#UserVERSION#", EditorProjectConfig.Version)
                           .Replace("#UserDescription#", EditorProjectConfig.Description)
                           .Replace("#UserUNITYVERSION#", Application.unityVersion)
                           .Replace("#UserDATE#", DateTime.Now.ToString("yyyy-MM-dd"));
                if (func!=null)
                    txt = func.Invoke(txt);
                File.WriteAllText(writePath, txt, System.Text.Encoding.UTF8);
            }

            private void CreateUIMapGen(string genSourcePath)
            {
                if (File.Exists(genSourcePath)) return;
                using (FileStream fs = new FileStream(genSourcePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        fs.Lock(0, fs.Length);
                        sw.WriteLine("/*********************************************************************************");
                        sw.WriteLine(" *Author:         #User#");
                        sw.WriteLine(" *Version:        #UserVERSION#");
                        sw.WriteLine(" *UnityVersion:   #UserUNITYVERSION#");
                        sw.WriteLine(" *Date:           #UserDATE#");
                        sw.WriteLine(" *Description:    #UserDescription#");
                        sw.WriteLine(" *History:        #UserDATE#--");
                        sw.WriteLine("*********************************************************************************/");
                        sw.WriteLine("using System;");
                        sw.WriteLine("using System.Collections;");
                        sw.WriteLine("using System.Collections.Generic;");
                        sw.WriteLine("using IFramework;");

                        sw.WriteLine("");
                        sw.WriteLine("namespace #UserNameSpace#");
                        sw.WriteLine("{");
                        sw.WriteLine("\tpublic class #UserSCRIPTNAME# ");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tpublic static Dictionary<Type, Tuple<Type, Type, Type>> map =");
                        sw.WriteLine("\t\tnew Dictionary<Type, Tuple<Type, Type, Type>>()");
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("//ToDo");
                        sw.WriteLine("\t\t};");
                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        fs.Unlock(0, fs.Length);
                        sw.Flush();
                        fs.Flush();
                    }

                }
                AssetDatabase.Refresh();
            }
        }
    }
    partial class UIMoudleWindow 
    {

        private void OnEnable()
        {
            genpath = EditorEnv.FrameworkPath.CombinePath("UI/Editor/Gen");
            if (runTimeView == null)
                runTimeView = new RunTimeView();
            runTimeView.OnEnable();

            if (mVVM_GenCodeView == null)
                mVVM_GenCodeView = new MVVM_GenCodeView();
            if (mVP_GenCodeView == null)
                mVP_GenCodeView = new MVP_GenCodeView();
        }
        private void OnGUI()
        {
            int viewIndex = (int)viewType;
            this.Toolbar(ref viewIndex, Enum.GetNames(typeof(ViewType)))
                .Pan(() =>
                {
                    viewType = (ViewType)viewIndex;
                    switch (viewType)
                    {
                        case ViewType.MVVM_GenCode: mVVM_GenCodeView.OnGUI(); break;
                        case ViewType.MVP_GenCode: mVP_GenCodeView.OnGUI(); break;
                        case ViewType.Runtime: runTimeView.OnGUI(); break;
                    }
                });
          
            this.Repaint();
        }
    }
}
