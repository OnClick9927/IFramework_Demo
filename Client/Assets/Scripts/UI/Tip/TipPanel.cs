﻿/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-27
 *Description:    Description
 *History:        2020-03-27--
*********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using IFramework;
using UnityEngine.UI;
using UnityEngine;
using IFramework.Tweens;
//<<<<<<< HEAD
using IFramework.UI;

//=======
//>>>>>>> 385cdc4088d4982e0fd0d85a438499b267dd4fc6
namespace IFramework_Demo
{

    public class TipPanel : UIPanel
	{
		public Text Text;

        private Queue<string> pmdQueue;     //跑马灯队列.

        private void Start()
        {
   
            pmdQueue = new Queue<string>();
            rect_w = Text.GetComponent<RectTransform>().rect.width;

            orgPos = Text.rectTransform.localPosition;
             v3 = new Vector3(orgPos.x + rect_w, orgPos.y, orgPos.z);

            err_orgPos = errText.rectTransform.localPosition;

            NetMessage(Text.text);
        }
        private float rect_w;
        private Vector3 v3;
        private Vector3 orgPos;
        private float speed = 400;    //滚动速度
        private int loop = 1;          //循环次数

        public void NetMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            pmdQueue.Enqueue(msg);
            if (ie==null)
            {
                ie = Marquee();
                StartCoroutine(ie);
            }
           
        }
        private IEnumerator ie;
        private IEnumerator Marquee()
        {

            Text.gameObject.SetActive(true);

            while (pmdQueue.Count > 0)
            {

                Text.text = pmdQueue.Dequeue();

                yield return new WaitForFixedUpdate();

                float distance = Text.preferredWidth*2f + rect_w;
                int tLoop = loop; 

                while (tLoop-- > 0)
                {
                    Text.rectTransform.localPosition = v3;
                    var a= Text.rectTransform.DoLocalMove(new Vector3(v3.x - distance * 1.2f, orgPos.y, orgPos.z), 5);
                    while (!a.recyled)
                    {
                        yield return null;
                    }
                }
            }
            yield return null;
            Text. gameObject.SetActive(false);
            ie = null;
            yield break;
        }



        private Vector3 err_orgPos;
        public Text errText;
        public void ErrMessage(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            errText.transform.parent.gameObject.SetActive(true);
            errText.text = text;
            errText.transform.parent.transform.localPosition = err_orgPos;

            var tv = errText.transform.parent.transform.DoLocalMove(new Vector3(err_orgPos.x, err_orgPos.y + 200, err_orgPos.z), 1)
                .OnCompelete(() => {
                    errText.transform.parent.gameObject.SetActive(false);

                });
        }



        public Text danmuPrefab;
        private class EmojiTextPool : ObjectPool<Text>
        {
          

            public Text Prefab { get; set; }

            protected override Text CreatNew(IEventArgs arg)
            {
                return GameObject.Instantiate<Text>(Prefab);
            }
            protected override void OnGet(Text t, IEventArgs arg)
            {
                t.gameObject.SetActive(true);

                base.OnGet(t, arg);
            }
            protected override bool OnSet(Text t, IEventArgs arg)
            {
                t.gameObject.SetActive(false);
                return base.OnSet(t, arg);
            }
        }

        private EmojiTextPool pool;

        private float lastY=1055;
        public void Danmu(string msg)
        {
            if (pool==null)
            {
                pool = new EmojiTextPool();
                pool.Prefab = danmuPrefab;
            }

            var item=  pool.Get();
            item.text = msg;
            float x = Screen.width + item.preferredWidth/2;

            if (lastY-600< Screen.height - 30-lastY)
            {
                lastY = Random.Range(lastY, Screen.height - 30);
            }
            else
            {
                lastY = Random.Range(600f,lastY);
            }
            Vector2 start = new Vector2(x, lastY);

            item.transform.position = start;
            item.transform.parent = this.transform;

            var tv = item.transform.DoLocalMoveX(0 - x, 8)
       .OnCompelete(() => {
           pool.Set(item);

       });
        }
       
    }
}
