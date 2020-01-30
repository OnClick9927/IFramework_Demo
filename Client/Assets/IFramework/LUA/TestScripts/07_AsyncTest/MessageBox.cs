using UnityEngine;
using UnityEngine.UI;
using XLua;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

namespace IFramework.Lua
{
    public class MessageBox : MonoBehaviour
    {

        public static void ShowAlertBox(string message, string title, Action onFinished = null)
        {
            var alertPanel = GameObject.Find("Canvas").transform.Find("AlertBox");
            if (alertPanel == null)
            {
                alertPanel = (Instantiate(Resources.Load("AlertBox")) as GameObject).transform;
                alertPanel.gameObject.name = "AlertBox";
                alertPanel.SetParent(GameObject.Find("Canvas").transform);
                alertPanel.localPosition = new Vector3(-6f, -6f, 0f);
            }

            alertPanel.Find("title").GetComponent<Text>().text = title;
            alertPanel.Find("message").GetComponent<Text>().text = message;

            var button = alertPanel.Find("alertBtn").GetComponent<Button>();
            UnityAction onclick = () =>
            {
                if (onFinished != null)
                {
                    onFinished();
                }
                button.onClick.RemoveAllListeners();
                alertPanel.gameObject.SetActive(false);
            };
            //防止消息框未关闭时多次被调用
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onclick);
            alertPanel.gameObject.SetActive(true);
        }

        public static void ShowConfirmBox(string message, string title, Action<bool> onFinished = null)
        {
            var confirmPanel = GameObject.Find("Canvas").transform.Find("ConfirmBox");
            if (confirmPanel == null)
            {
                confirmPanel = (Instantiate(Resources.Load("ConfirmBox")) as GameObject).transform;
                confirmPanel.gameObject.name = "ConfirmBox";
                confirmPanel.SetParent(GameObject.Find("Canvas").transform);
                confirmPanel.localPosition = new Vector3(-8f, -18f, 0f);
            }

            confirmPanel.Find("confirmTitle").GetComponent<Text>().text = title;
            confirmPanel.Find("conmessage").GetComponent<Text>().text = message;

            var confirmBtn = confirmPanel.Find("confirmBtn").GetComponent<Button>();
            var cancelBtn = confirmPanel.Find("cancelBtn").GetComponent<Button>();
            Action cleanup = () =>
            {
                confirmBtn.onClick.RemoveAllListeners();
                cancelBtn.onClick.RemoveAllListeners();
                confirmPanel.gameObject.SetActive(false);
            };

            UnityAction onconfirm = () =>
            {
                if (onFinished != null)
                {
                    onFinished(true);
                }
                cleanup();
            };

            UnityAction oncancel = () =>
            {
                if (onFinished != null)
                {
                    onFinished(false);
                }
                cleanup();
            };

            //防止消息框未关闭时多次被调用
            confirmBtn.onClick.RemoveAllListeners();
            confirmBtn.onClick.AddListener(onconfirm);
            cancelBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.AddListener(oncancel);
            confirmPanel.gameObject.SetActive(true);
        }
    }

    public static class MessageBoxConfig
    {
        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
    {
        typeof(Action),
        typeof(Action<bool>),
        typeof(UnityAction),
    };
    }
}
