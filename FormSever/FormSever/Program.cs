using System;
using System.Windows.Forms;
using IFramework;
namespace FormSever
{
    static class Program
    {
        static int TickCountPersecond=60;
        static Timer timer;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            timer = new Timer();

            timer.Interval = 1000/ TickCountPersecond;
            timer.Tick += Trick;
            timer.Start();
            //Framework.Init();

            Application.Run(new SeverForm());

        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Framework.Update();
            Framework.Dispose();
            timer.Stop();
            timer = null;
        }

        private static void Trick(object sender, EventArgs e)
        {
            Framework.Init();
            Framework.Update();
        }
    }
}
