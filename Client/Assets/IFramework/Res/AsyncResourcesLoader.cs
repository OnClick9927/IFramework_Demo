/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.255
 *UnityVersion:   2018.4.17f1
 *Date:           2020-05-21
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;

namespace IFramework.Modules.Resources
{
    public class AsyncResourcesLoader<T> : AsyncResourceLoader<T> where T : UnityEngine.Object
    {
        private UnityEngine.ResourceRequest _request;
        protected override float _progress
        {
            get
            {
                if (_request!=null)
                    return _request.progress;
                if (_isdone)
                    return 1;
                return 0;
            }
        }
        protected override void OnLoad()
        {
            try
            {
               var _request = UnityEngine.Resources.LoadAsync<T>(path);
                _request.completed += (ops) => {
                    _isdone = true;
                    Tresource.value = _request.asset as T;
                };
            }
            catch (Exception e)
            {
                ThrowErr(e.Message);
            }
           
        }
        protected override void OnUnLoad()
        {
            if (Tresource.value != null)
            {
                UnityEngine.Resources.UnloadAsset(Tresource.Tvalue);
                Tresource.value = default(T);
            }
            _request = null;
        }
    }

}
