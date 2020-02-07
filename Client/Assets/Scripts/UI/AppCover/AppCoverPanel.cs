/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2020-02-13
 *Description:    APP的登录界面
 *History:        2020-02-13--
*********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using IFramework;
using UnityEngine.UI;
using UnityEngine;

namespace IFramework_Demo
{
	public class AppCoverPanel : UIPanel
	{
		//注册按钮
		public Button Btn_Register;
		//登录按钮
		public Button Btn_Load;
        private RectTransform rectTransform { get { return GetComponent<RectTransform>(); } }
        protected override void OnClear(UIEventArgs arg)
        {
            
        }

        protected override void OnLoad(UIEventArgs arg)
        {
            rectTransform.sizeDelta = Vector2.zero;
        }

        protected override void OnPop(UIEventArgs arg)
        {
            
        }

        protected override void OnPress(UIEventArgs arg)
        {
            
        }

        protected override void OnTop(UIEventArgs arg)
        {
           
        }
    }
}
