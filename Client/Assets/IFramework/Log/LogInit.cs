/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-05-20
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEngine;

namespace IFramework
{
    [OnFrameworkInitClass]
    static class LogInit
    {
        public const string StoName = "LogSetting";
        static LogInit()
        {
            Log.loger = new UnityLoger();
            LogSetting setting = Resources.Load<LogSetting>(StoName);
            if (setting == null) return;
            Log.LogEnable = setting.LogEnable;
            Log.WarnningEnable = setting.WarnningEnable;
            Log.ErrEnable = setting.ErrEnable;
            Log.LogLevel = setting.LogLevel;
            Log.WarnningLevel = setting.WarnningLevel;
            Log.Enable = setting.Enable;
            Log.ErrLevel = setting.ErrLevel;
        }
    }

}