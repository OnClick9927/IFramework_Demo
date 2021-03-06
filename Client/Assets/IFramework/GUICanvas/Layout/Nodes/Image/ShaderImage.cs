﻿/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-12-07
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;
using System.Xml;
using UnityEngine;

namespace IFramework.GUITool.LayoutDesign
{
    [GUINode("Image/ShaderImage")]
    public class ShaderImage : GUINode
    {
        public int leftBorder, rightBorder, topBorder, bottomBorder, pass;
        public Rect sourceRect;
        public Material material;
        public Texture2D image;
        public ShaderImage() : base() { }
        public ShaderImage(ShaderImage other) : base(other)
        {
            leftBorder = other.leftBorder;
            rightBorder = other.rightBorder;
            topBorder = other.topBorder;
            bottomBorder = other.bottomBorder;
            pass = other.pass;
            image = other.image;
            material = other.material;
            sourceRect = other.sourceRect;
        }
        public override void Reset()
        {
            base.Reset();
            image = null;
            leftBorder = rightBorder = topBorder = bottomBorder = 100;
            pass = -1;
            material = null;
            sourceRect = new Rect(1, 1, -1, -1);
        }
        protected override void OnGUI_Self()
        {
            GUILayout.Label("", CalcGUILayOutOptions());
            position = GUILayoutUtility.GetLastRect();
            if (image != null)
            {
                if (material != null)
                {
                    if (pass > material.passCount)
                        pass = material.passCount - 1;
                }
                else pass = -1;
                Graphics.DrawTexture(position, image, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, material, pass);
            }
        }

        public override XmlElement Serialize(XmlDocument doc)
        {
            XmlElement root = base.Serialize(doc);
            SerializeField(root, "leftBorder", leftBorder);
            SerializeField(root, "rightBorder", rightBorder);
            SerializeField(root, "topBorder", topBorder);
            SerializeField(root, "bottomBorder", bottomBorder);
            SerializeField(root, "pass", pass);
            SerializeField(root, "sourceRect", sourceRect);
            if (image != null)
                SerializeField(root, "image", image.CreateReadableTexture().EncodeToPNG());
            return root;
        }
        public override void DeSerialize(XmlElement root)
        {
            base.DeSerialize(root);
            DeSerializeField(root, "leftBorder", ref leftBorder);
            DeSerializeField(root, "rightBorder", ref rightBorder);
            DeSerializeField(root, "topBorder", ref topBorder);
            DeSerializeField(root, "bottomBorder", ref bottomBorder);
            DeSerializeField(root, "pass", ref pass);
            DeSerializeField(root, "sourceRect", ref sourceRect);
            if (root.SelectSingleNode("image") != null)
            {
                byte[] bs = new byte[0];
                DeSerializeField(root, "image", ref bs);
                image = new Texture2D(200, 200);
                image.LoadImage(bs);
                image.hideFlags = HideFlags.DontSaveInEditor;
            }
        }
    }
}
