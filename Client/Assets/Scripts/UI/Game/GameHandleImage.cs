/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-27
 *Description:    Description
 *History:        2020-03-27--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IFramework;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IFramework_Demo
{
    public class GameHandleImage : MonoBehaviour, IDragHandler
    {
        public GamePanel gp;

      

        public void OnDrag(PointerEventData eventData)
        {
            //transform.position = eventData.position;
            gp.OnDrag(eventData.position);
        }

      
    }
}
