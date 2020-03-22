using IFramework;
using IFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormSever.Net
{
    [OnEnvironmentInit]
    static class NetMessageHandleTool
    {
       public  readonly static List<INetMessageHandler> Handlers;
       
        static NetMessageHandleTool()
        {
            Handlers = typeof(INetMessageHandler).GetSubTypesInAssemblys()
                .Select((type) => {
                    if (!type.IsAbstract)
                        return Activator.CreateInstance(type) as INetMessageHandler;
                    return null;
                }).ToList();

            Handlers.RemoveAll((h) => { return h == null; });

        }
    }
}
