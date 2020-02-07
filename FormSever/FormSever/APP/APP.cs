using IFramework;
using IFramework.Modules.Loom;
using IFramework.Modules.Message;
using IFramework.Modules.Coroutine;
using System.Windows.Forms;
using System.IO;
using IFramework.Serialization.Csv;
using System.Collections.Generic;
namespace FormSever
{
    public partial class APP
    {
        private static void InitFrameworkMoudles()
        {
            Framework.env0.modules.Loom = Framework.env0.modules.CreateModule<LoomModule>();
            Framework.env0.modules.Message = Framework.env0.modules.CreateModule<MessageModule>();
            Framework.env0.modules.Coroutine= Framework.env0.modules.CreateModule<CoroutineModule>();
            Framework.env0.modules.App= Framework.env0.modules.CreateModule<AppMoudle>();
        }
        public static void WriteLogConfig()
        {
            CsvWriter csvWriter = new CsvWriter(new StreamWriter(logConfigPath), new CsvRow(), new CsvExplainer());
            csvWriter.Write(new List<Configs.LogConfig>() {
                    Configs.log
                });
            csvWriter.Dispose(); 
        }
        public static void WriteNetConfig()
        {
            CsvWriter csvWriter = new CsvWriter(new StreamWriter(netConfigPath), new CsvRow(), new CsvExplainer());
            csvWriter.Write(new List<Configs.NetConfig>() {
                    Configs.net
                });
            csvWriter.Dispose();
        }
        private static void InitLog()
        {
            if (!File.Exists(logConfigPath))
            {
                WriteLogConfig();
            }
            CsvReader cr = new CsvReader(new StreamReader(logConfigPath), new CsvRow(), new CsvExplainer());
            Configs.log=cr.Get<Configs.LogConfig>()[0];
            cr.Dispose();
        }
        private static void InitNet()
        {
            if (!File.Exists(netConfigPath))
                WriteNetConfig();
            CsvReader cr = new CsvReader(new StreamReader(netConfigPath), new CsvRow(), new CsvExplainer());
            Configs.net = cr.Get<Configs.NetConfig>()[0];
            cr.Dispose();
        }
    }
    [OnFrameworkInitClass]
    public static partial class APP
    {
        static private string configPath = Application.ExecutablePath.GetDirPath().CombinePath("Configs");
        static public string logConfigPath = configPath.CombinePath("LogConfig.csv");
        static public string netConfigPath = configPath.CombinePath("NetConfig.csv");
        static APP()
        {
            if (!Directory.Exists(configPath))
                Directory.CreateDirectory(configPath);

            InitLog();
            InitNet();

            InitFrameworkMoudles();
        }

       
    }



}
