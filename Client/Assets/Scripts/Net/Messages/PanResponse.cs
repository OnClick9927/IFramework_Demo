﻿/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-01-31
 *Description:    Demo Of  IFramework

 *History:        2020-01-31--
*********************************************************************************/
using System.Collections.Generic;

namespace IFramework_Demo
{
    [NetMessage(7)]
    class PanResponse : INetMessage
    {
        public List<GameColum> colums;
    }
}
