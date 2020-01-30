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
            logView.Font = new System.Drawing.Font("宋体", LogConfig.FontSize);

            this.Log_FontSize.Text = LogConfig.FontSize.ToString();
            this.Log_LogEnable.Checked = LogConfig.LogEnable;
            this.Log_Enable.Checked = LogConfig.Enable;
            this.Log_WarnEnable.Checked = LogConfig.WarnningEnable;
            this.Log_ErrEnable.Checked = LogConfig.ErrEnable;

            this.Log_LogLev.Text = LogConfig.LogLevel.ToString();
            this.Log_WarnLev.Text = LogConfig.WarnningLevel.ToString();
            this.Log_ErrLev.Text = LogConfig.ErrLevel.ToString();

        }

        private void LogClear(object sender, EventArgs e)
        {
            logView.Items.Clear();
        }

        private void Log_Fresh_Click(object sender, EventArgs e)
        {
            LogConfig.LogEnable= this.Log_LogEnable.Checked;
              LogConfig.Enable= this.Log_Enable.Checked;
              LogConfig.WarnningEnable = this.Log_WarnEnable.Checked;
              LogConfig.ErrEnable= this.Log_ErrEnable.Checked;

            if (!int.TryParse(this.Log_LogLev.Text, out Log.LogLevel))
                Log.E("Log Fresh Err LogLevel");
            if (!int.TryParse(this.Log_WarnLev.Text, out Log.WarnningLevel))
                Log.E("Log Fresh Err WarnningLevel");
            if (!int.TryParse(this.Log_ErrLev.Text, out Log.ErrLevel))
                Log.E("Log Fresh Err ErrLevel");
            if (!int.TryParse(this.Log_FontSize.Text, out LogConfig.FontSize))
                Log.E("Log Fresh Err FontSize");
            else
                logView.Font = new System.Drawing.Font("宋体", LogConfig.FontSize);
        }
    }

}
