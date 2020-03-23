/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/

namespace IFramework_Demo
{
    [NetMessage(11)]
     class SeverFormBroadCast : INetMessage
    {
        public string message;
    }
}
