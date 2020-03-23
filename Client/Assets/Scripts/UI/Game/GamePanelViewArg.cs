/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-24
 *Description:    Description
 *History:        2020-03-24--
*********************************************************************************/
using IFramework;
using UnityEngine;

namespace IFramework_Demo
{
    public struct GamePanelViewArg:IEventArgs
    {
        public Color color;
        public float slider_x;
        public float slider_y;

        public int input_x;
        public int input_y;

        public string input_chat;
        public int slider_panSize;
    }
}
