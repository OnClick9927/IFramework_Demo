using IFramework;
using System;

namespace FormSever
{
    partial class SeverForm
    {
        private void ReadNetConfig()
        {
            this.TCPBufferSize.Text = Configs.net.TCPBuffersize.ToString();
            this.TCPPort.Text = Configs.net.TCPPort.ToString();
            this.TcpCheckSpace.Text = Configs.net.TCPCheckSpace.ToString();
            this.TCPTimeOut.Text = Configs.net.TCPConnTimeOut.ToString();
            this.TCPMaxCon.Text = Configs.net.TCPMaxConn.ToString();
           
        }
        private void Net_Fresh_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(this.TCPBufferSize.Text, out Configs.net.TCPBuffersize))
                Log.E("Net Init Err  TCPBuffersize");
            if (!int.TryParse(this.TCPPort.Text, out Configs.net.TCPPort))
                Log.E("Net Init Err TCPPort");
            if (!int.TryParse(this.TcpCheckSpace.Text, out Configs.net.TCPCheckSpace))
                Log.E("Net Init Err TCPCheckSpace");
            if (!int.TryParse(this.TCPTimeOut.Text, out Configs.net.TCPConnTimeOut))
                Log.E("Net Init Err TCPConnTimeOut");
            if (!int.TryParse(this.TCPMaxCon.Text, out Configs.net.TCPMaxConn))
                Log.E("Net Init Err TCPMaxConn");


            APP.WriteNetConfig();
        }

       

    }

}
