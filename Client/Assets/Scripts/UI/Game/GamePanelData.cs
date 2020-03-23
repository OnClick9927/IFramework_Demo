/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-24
 *Description:    Description
 *History:        2020-03-24--
*********************************************************************************/
using UnityEngine;
using IFramework.Modules.MVVM;

namespace IFramework_Demo
{
    public class GamePanelData:IDataModel
    {
        public Color pickColor;
        public Vector2 curSelectPos;
        public string chat;
        public int pan_size;
    }
}
