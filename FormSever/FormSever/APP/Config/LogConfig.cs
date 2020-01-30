using IFramework;
namespace FormSever
{
    static partial class Configs
    {
        public class LogConfig
        {
            public int FontSize = 15;

            public bool Enable { get { return Log.Enable; } set { Log.Enable = value; } }
            public int LogLevel { get { return Log.LogLevel; } set { Log.LogLevel = value; } }
            public int WarnningLevel { get { return Log.WarnningLevel; } set { Log.WarnningLevel = value; } }
            public int ErrLevel { get { return Log.ErrLevel; } set { Log.ErrLevel = value; } }

            public bool LogEnable { get { return Log.LogEnable; } set { Log.LogEnable = value; } }
            public bool WarnningEnable { get { return Log.WarnningEnable; } set { Log.WarnningEnable = value; } }
            public bool ErrEnable { get { return Log.ErrEnable; } set { Log.ErrEnable = value; } }
        }
    }
}
