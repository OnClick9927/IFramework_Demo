using IFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormSever.Net
{
    [OnFrameworkInitClass]
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
