/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.4.17f1
 *Date:           2020-02-15
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/

using UnityEngine;

namespace IFramework
{
    public class UnityLoger : ILoger
    {
        public void Log(LogType logType, object message, params object[] paras)
        {
            switch (logType)
            {
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Log:
                    Debug.Log(message);
                    break;
            }
        }

        public void LogFormat(LogType logType, string format, object message, params object[] paras)
        {
            switch (logType)
            {
                case LogType.Error:
                    Debug.LogErrorFormat(message as Object, format, paras);
                    break;
                case LogType.Warning:
                    Debug.LogWarningFormat(message as Object, format, paras);
                    break;
                case LogType.Log:
                    Debug.LogFormat(message as Object, format, paras);
                    break;
            }
        }
    }

}
