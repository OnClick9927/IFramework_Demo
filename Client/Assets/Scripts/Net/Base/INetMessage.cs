/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using System;
using IFramework;

namespace IFramework_Demo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class NetMessageAttribute : Attribute
    {
        public NetMessageAttribute(uint id)
        {
            Id = id;
        }

        public uint Id { get; }
    }
    public interface INetMessage : IEventArgs
    {
    }
}
