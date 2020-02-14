/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-12-07
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IFramework.GUITool.RectDesign
{
    static class GUIElementEditors
    {
        public static List<Type> editorTypes = typeof(GUIElementEditor).GetSubTypesInAssemblys().ToList();
    }
    class GUIElementInspectorView : ILayoutGUIDrawer
    {
        private Dictionary<Type, GUIElementEditor> dic = new Dictionary<Type, GUIElementEditor>();
        private GUIElementEditor Pan;
        private Vector2 scroll2;

        public GUIElementInspectorView()
        {
            var designs = GUIElementEditors.editorTypes.FindAll((t) => { return t.IsDefined(typeof(CustomGUIElementAttribute), false); });
            var eles = GUIElements.elementTypes;
            foreach (var type in eles)
            {
                var typeTree = type.GetTypeTree();
                for (int i = 0; i < typeTree.Count; i++)
                {
                    Type des = designs.Find((t) => {
                        return (t.GetCustomAttributes(typeof(CustomGUIElementAttribute), false).First() as CustomGUIElementAttribute).EditType == typeTree[i];
                    });
                    if (des != null)
                    {
                        dic.Add(type, Activator.CreateInstance(des) as GUIElementEditor);
                        break;

                    }
                }
            }

            GUIElementSelection.onElementChange += (ele) =>
            {
                if (ele != null)
                {
                    Pan = dic[ele.GetType()];
                    Pan.element = ele;
                }
            };
        }
        public void OnGUI(Rect rect)
        {
            if (Pan == null) return;
            this.BeginArea(rect)
                    .BeginScrollView(ref scroll2)
                        .Pan(Pan.OnInspectorGUI)
                    .LayoutEndScrollView()
                .EndArea();
        }
    }
}
