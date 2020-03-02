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
    [OnEnvironmentInit]
    static class LogInit
    {
        public const string StoName = "LogSetting";
        static LogInit()
        {
            Log.loger = new UnityLoger();
            LogSetting setting = Resources.Load<LogSetting>(StoName);
            if (setting == null) return;
            Log.enable_L = setting.LogEnable;
            Log.enable_W = setting.WarnningEnable;
            Log.enable_E = setting.ErrEnable;
            Log.lev_L = setting.LogLevel;
            Log.lev_W = setting.WarnningLevel;
            Log.enable = setting.Enable;
            Log.lev_E = setting.ErrLevel;
        }
    }

}