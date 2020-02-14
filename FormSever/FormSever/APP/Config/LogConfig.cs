using IFramework;
namespace FormSever
{
    static partial class Configs
    {
        public class LogConfig
        {
            public int FontSize = 15;

            public bool Enable { get { return Log.enable; } set { Log.enable = value; } }
            public int LogLevel { get { return Log.lev_L; } set { Log.lev_L = value; } }
            public int WarnningLevel { get { return Log.lev_W; } set { Log.lev_W = value; } }
            public int ErrLevel { get { return Log.lev_E; } set { Log.lev_E = value; } }

            public bool LogEnable { get { return Log.enable_L; } set { Log.enable_L = value; } }
            public bool WarnningEnable { get { return Log.enable_W; } set { Log.enable_W = value; } }
            public bool ErrEnable { get { return Log.enable_E; } set { Log.enable_E = value; } }
        }
    }
}
