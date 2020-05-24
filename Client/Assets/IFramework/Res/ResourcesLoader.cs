﻿/*********************************************************************************
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
    public class ResourcesLoader<T> : ResourceLoader<T>where T: UnityEngine.Object
    {
        protected override void OnLoad()
        {
            try
            {
                Tresource.value = UnityEngine.Resources.Load<T>(path);
            }
            catch (Exception e)
            {
                ThrowErr(e.Message);
            }
            finally
            {
                _isdone = true;
                _progress = 1;
            }
        }
        protected override void OnUnLoad()
        {
            if (Tresource.value != null)
            {
                UnityEngine.Resources.UnloadAsset(Tresource.value);
                Tresource.value = default(T);
            }
        }
    }

}
