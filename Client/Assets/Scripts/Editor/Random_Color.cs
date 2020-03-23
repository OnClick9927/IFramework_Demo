/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-03-23
 *Description:    Description
 *History:        2020-03-23--
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IFramework;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
namespace IFramework_Demo
{
	public class Random_Color
	{
        [MenuItem("Tool/HH")]
	    static void R()
        {
            var go = Selection.activeGameObject;
            if (go)
            {
                go.GetComponentsInChildren<Image>()
                    .ToList()
                    .ForEach((img) =>
                    {
                        img.color = new Color(UnityEngine.Random.Range(0, 1f),
                            UnityEngine.Random.Range(0, 1f),
                            UnityEngine.Random.Range(0, 1f),
                            UnityEngine.Random.Range(0, 1f));
                    });
            }


        }
        [MenuItem("Tool/HH1")]
        static void R2()
        {
            var go = Selection.activeGameObject;
            if (go)
            {
                for (int i = 0; i < 10000; i++)
                {
                    GameObject gg = new GameObject(i.ToString());
                    gg.transform.parent = go.transform;
                    gg.AddComponent<Image>();
                }
                //go.GetComponentsInChildren<Image>()
                //    .ToList()
                //    .ForEach((img) =>
                //    {
                //        img.color = new Color(UnityEngine.Random.Range(0, 1f),
                //            UnityEngine.Random.Range(0, 1f),
                //            UnityEngine.Random.Range(0, 1f),
                //            UnityEngine.Random.Range(0, 1f));
                //    });
            }


        }
    }
}
