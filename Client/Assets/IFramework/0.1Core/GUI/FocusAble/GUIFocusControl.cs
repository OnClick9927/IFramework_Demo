/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-08-27
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace IFramework.GUITool
{
    public class GUIFocusControl : SingletonPropertyClass<GUIFocusControl>
    {
        private GUIFocusControl() { }
        private List<FocusAbleGUIDrawer> drawers;
        private List<string> UUIDs;
        public static string CurFocusID
        {
            get
            {
                if (CurFocusDrawer == null)
                    return string.Empty;
                return CurFocusDrawer.FocusID;
            }
        }
        public static FocusAbleGUIDrawer CurFocusDrawer { get; private set; }

        public static string Subscribe(FocusAbleGUIDrawer drawer)
        {
            if (!Instance.drawers.Contains(drawer)) Instance.drawers.Add(drawer);
            string uuid = drawer.GetHashCode().ToString();
            if (Instance.UUIDs.Contains(uuid))
                uuid += "1";
            Instance.UUIDs.Add(uuid);
            return uuid;
        }
        public static void UnSubscribe(FocusAbleGUIDrawer drawer)
        {
            if (Instance.drawers.Contains(drawer))
            {
                Instance.drawers.Remove(drawer);
            }
            if (Instance.UUIDs.Contains(drawer.FocusID))
            {
                Instance.UUIDs.Remove(drawer.FocusID);
            }
        }

        public static bool Contans(FocusAbleGUIDrawer drawer)
        {
            return Instance.drawers.Contains(drawer);
        }

        public static void Focus(FocusAbleGUIDrawer drawer)
        {
            if (CurFocusDrawer == drawer)
                GUI.FocusControl(null);
            CurFocusDrawer = drawer;

            for (int i = 0; i < Instance.drawers.Count; i++)
            {
                if (Instance.drawers[i] == drawer) Instance.drawers[i].Focused = true;
                else { Instance.drawers[i].Focused = false; }
            }
            GUI.FocusControl(null);
            GUI.FocusControl(drawer.FocusID);
        }
        public static void Diffuse(FocusAbleGUIDrawer drawer)
        {
            if (CurFocusDrawer == drawer)
            {
                for (int i = 0; i < Instance.drawers.Count; i++)
                {
                    Instance.drawers[i].Focused = false;
                }
                GUI.FocusControl(null);
                CurFocusDrawer = null;
            }
        }


        protected override void OnSingletonInit()
        {
            drawers = new List<FocusAbleGUIDrawer>();
            UUIDs = new List<string>();
        }

        public override void Dispose()
        {
            drawers.Clear();
            UUIDs.Clear();
        }
    }

}
