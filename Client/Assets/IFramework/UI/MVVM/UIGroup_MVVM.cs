/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1.440
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-28
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Modules.MVVM;

namespace IFramework
{
    public class UIGroup_MVVM : MVVMGroup
    {
        public UIGroup_MVVM(string name, View view, ViewModel viewModel, IDataModel model) : base(name, view, viewModel, model)
        {
        }
        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }

}
