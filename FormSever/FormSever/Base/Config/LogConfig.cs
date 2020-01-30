using IFramework;
namespace FormSever
{
    static class LogConfig
    {
        public static int FontSize=15;

        public static bool Enable { get { return Log.Enable; }set { Log.Enable = value; } }
        public static int LogLevel { get { return Log.LogLevel; } set { Log.LogLevel = value; } }
        public static int WarnningLevel { get { return Log.WarnningLevel; } set { Log.WarnningLevel = value; } }
        public static int ErrLevel { get { return Log.ErrLevel; } set { Log.ErrLevel = value; } }

        public static bool LogEnable { get { return Log.LogEnable; } set { Log.LogEnable = value; } }
        public static bool WarnningEnable { get { return Log.WarnningEnable; } set { Log.WarnningEnable = value; } }
        public static bool ErrEnable { get { return Log.ErrEnable; } set { Log.ErrEnable = value; } }
    }
}
