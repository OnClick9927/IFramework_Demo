using IFramework;
using System;

namespace FormSever
{
    partial class SeverForm
    {
        private void ReadLogConfig()
        {
            Log.loger = new Logger_Winfom(logView);
            logView.Resize += (o, e) => { Refresh(); };
            logView.Font = new System.Drawing.Font("宋体", Configs.log.FontSize);

            this.Log_FontSize.Text = Configs.log.FontSize.ToString();
            this.Log_LogEnable.Checked = Configs.log.LogEnable;
            this.Log_Enable.Checked = Configs.log.Enable;
            this.Log_WarnEnable.Checked = Configs.log.WarnningEnable;
            this.Log_ErrEnable.Checked = Configs.log.ErrEnable;

            this.Log_LogLev.Text = Configs.log.LogLevel.ToString();
            this.Log_WarnLev.Text = Configs.log.WarnningLevel.ToString();
            this.Log_ErrLev.Text = Configs.log.ErrLevel.ToString();

        }

        private void LogClear(object sender, EventArgs e)
        {
            logView.Items.Clear();
        }

        private void Log_Fresh_Click(object sender, EventArgs e)
        {
            Configs.log.LogEnable= this.Log_LogEnable.Checked;
            Configs.log.Enable= this.Log_Enable.Checked;
            Configs.log.WarnningEnable = this.Log_WarnEnable.Checked;
            Configs.log.ErrEnable= this.Log_ErrEnable.Checked;

            if (!int.TryParse(this.Log_LogLev.Text, out Log.lev_L))
                Log.E("Log Fresh Err LogLevel");
            if (!int.TryParse(this.Log_WarnLev.Text, out Log.lev_W))
                Log.E("Log Fresh Err WarnningLevel");
            if (!int.TryParse(this.Log_ErrLev.Text, out Log.lev_E))
                Log.E("Log Fresh Err ErrLevel");
            if (!int.TryParse(this.Log_FontSize.Text, out Configs.log.FontSize))
                Log.E("Log Fresh Err FontSize");
            else
                logView.Font = new System.Drawing.Font("宋体", Configs.log.FontSize);

            APP.WriteLogConfig();
        }
    }

}
