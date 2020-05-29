/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-24
 *Description:    Description
 *History:        2020-03-24--
*********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using IFramework;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using IFramework.UI;

namespace IFramework_Demo
{

    public class GamePanel : UIPanel,IPointerDownHandler, IDragHandler
    {
		public ColorPicker colorPicker;
		public RawImage map;
        public Text nameText;
		public Slider slider_pox_x;


        public InputField input_pox_x;
		public Slider slider_pox_y;
		public InputField input_pox_y;
		public Button ok;
        public Image kuangkuang;


        public Button chatBtn;
        public InputField input_chat;

        public Slider slider_size;
        public InputField input_size; 
        public Button ask_map;

        private void Start()
        {
            zero = kuangkuang.transform.localPosition;
            map_w = map.rectTransform.rect.width ;
            zero.y = zero.y - map_w/2;
            zero.x = zero.x - map_w/2;
        }
        float map_w;
        int uni { get { return (int)map_w / GamePanelViewModel.MapSize; } }

        private Vector2 zero=Vector2.zero;
        public void SetKuang(Vector2 pos)
        {
            Vector2 pp = new Vector2(zero.y+pos.y*map_w/ GamePanelViewModel.MapSize, zero.x + pos.x * map_w / GamePanelViewModel.MapSize);
            kuangkuang.transform.localPosition =pp;
        }
        internal void OnDrag(Vector2 position)
        {
            Vector2 v2 = position;   

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform,
                v2, null, out v2);

            Vector2 tmp = v2 - zero;
            if (tmp.x < 0 || tmp.y < 0 || tmp.x > map_w || tmp.y > map_w) return;



            float x = tmp.x;
            float y = tmp.y;

            
            int _x=  (int)x / uni;
            int _y = (int)y / uni;

            input_pox_x.text =Mathf.Clamp( _y,0,GamePanelViewModel.MapSize).ToString();
            input_pox_y.text = Mathf.Clamp(_x, 0, GamePanelViewModel.MapSize).ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData.position);
        }
        public void OnDrag(PointerEventData eventData)
        {
            //transform.position = eventData.position;
            OnDrag(eventData.position);
        }
    }
}
