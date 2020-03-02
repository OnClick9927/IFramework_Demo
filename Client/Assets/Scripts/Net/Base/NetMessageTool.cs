/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using IFramework;

namespace IFramework_Demo
{
    [OnEnvironmentInit]
    public static class NetMessageTool
    {
        static Dictionary<uint, Type> messageDIc;
        static NetMessageTool()
        {
            messageDIc = typeof(INetMessage).GetSubTypesInAssemblys()
                            .Where((type) => { return type.IsDefined(typeof(NetMessageAttribute), false); })
                            .ToDictionary((type) => { return (type.GetCustomAttributes(typeof(NetMessageAttribute), false).First() as NetMessageAttribute).Id; });

        }
        public static Type GetTypeByID(uint id)
        {
            return messageDIc[id];
        }
        public static uint GetIDByType(Type type)
        {
            foreach (var item in messageDIc)
            {
                if (item.Value == type)
                {
                    return item.Key;
                }
            }
            return 0;
        }
    }
}
