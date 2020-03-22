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

    [NetMessage(0)]
    class EmptyMessage : INetMessage
    {

    }
    [NetMessage(1)]
    class TestMessage : INetMessage
    {
        public int index = 666;
    }
    [NetMessage(2)]
    class LoginRequest : INetMessage
    {
        public string account;
        public string psd;
    }
    [NetMessage(3)]
    class LoginResponse : INetMessage
    {
        public string account;
        public string psd;
        public string name;
        public bool sucess;
        public string err;
    }
    [NetMessage(4)]
    class RegisterRequest : INetMessage
    {
        public string account;
        public string psd;
        public string name;
    }
    [NetMessage(5)]
    class RegisterResponse : INetMessage
    {
        public string account;
        public string psd;
        public string name;
        public bool sucess;
        public string err;
    }
}
