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
using IFramework.Modules.MVVM;

namespace IFramework.UI
{
    public class Groups:IGroups
    {
        private MVVMModule _moudule;
        private Dictionary<Type, Tuple<Type, Type, Type>> _map;

        private MVVMGroup FindGroup(string panelName)
        {
            return _moudule.FindGroup(panelName);
        }
        private MVVMGroup FindGroup(UIPanel panel)
        {
            return FindGroup(panel.PanelName);
        }



        public Groups(Dictionary<Type, Tuple<Type, Type, Type>> map)
        {
            _moudule = MVVMModule.CreatInstance<MVVMModule>("UIGroup");
            this._map = map;
        }
        public UIPanel FindPanel(string panelName)
        {
            var group = FindGroup(panelName);
            if (group == null) return null;
                return (group.view as UIView).panel;
        }
        public void InvokeUIModuleEventListenner(UIEventArgs arg)
        {
            if (arg.pressPanel != null)
                (FindGroup(arg.pressPanel).view as IUIModuleEventListenner).OnPress(arg);
            if (arg.popPanel != null)
                (FindGroup(arg.popPanel).view as IUIModuleEventListenner).OnPop(arg);
            if (arg.curPanel != null)
                (FindGroup(arg.curPanel).view as IUIModuleEventListenner).OnTop(arg);
        }
        public void Subscribe(UIPanel panel)
        {
            var _group = FindGroup(panel);
            if (_group != null) throw new Exception(string.Format("Have Subscribe Panel Name: {0}", panel.PanelName));

            Tuple<Type, Type, Type> tuple;
            _map.TryGetValue(panel.GetType(), out tuple);
            if (tuple == null) throw new Exception(string.Format("Could Not Find map with Type: {0}", panel.GetType()));


            var model = Activator.CreateInstance(tuple.Item1) as IDataModel;
            var view = Activator.CreateInstance(tuple.Item2) as UIView;
            var vm = Activator.CreateInstance(tuple.Item3) as UIViewModel;
            view.panel = panel;

            UIGroup group = new UIGroup(panel.name, view, vm, model);
            _moudule.AddGroup(group);
            (view as IUIModuleEventListenner).OnLoad();
        }
        public void UnSubscribe(UIPanel panel)
        {
            var group = FindGroup(panel);
            if (group != null)
            {
                (group.view as IUIModuleEventListenner).OnClear();
                group.Dispose();
            }
        }
        public void Dispose()
        {
            _moudule.Dispose();
        }
    }
}
