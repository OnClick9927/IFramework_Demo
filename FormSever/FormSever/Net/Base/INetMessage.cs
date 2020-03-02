using System;
using IFramework;

namespace FormSever.Net
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public class NetMessageAttribute : Attribute {
        public NetMessageAttribute(uint id)
        {
            Id = id;
        }

        public uint Id { get; }
    }
    public interface INetMessage:IEventArgs
    {
    }

    [NetMessage(0)]
    class EmptyMessage:INetMessage
    {

    }
}
