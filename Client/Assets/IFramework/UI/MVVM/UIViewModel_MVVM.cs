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
    public abstract class UIViewModel_MVVM : ViewModel { }
    public abstract class TUIViewModel_MVVM<M> : UIViewModel_MVVM where M : IDataModel
    {
        protected M Tmodel { get { return (M)model; } }
    }

}
