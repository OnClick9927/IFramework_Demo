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
            this.UDPBufferSize.Text = Configs.net.UDPBuffersize.ToString();
            this.UDPPort.Text = Configs.net.UDPPort.ToString();
            this.UDPCheckSpace.Text = Configs.net.UDPCheckSpace.ToString();
            this.UDPTimeOut.Text = Configs.net.UDPConnTimeOut.ToString();
            this.UDPMaxCon.Text = Configs.net.UDPMaxConn.ToString();
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

            if (!int.TryParse(this.UDPBufferSize.Text, out Configs.net.UDPBuffersize))
                Log.E("Net Init Err UDPBuffersize");
            if (!int.TryParse(this.UDPPort.Text, out Configs.net.UDPPort))
                Log.E("Net Init Err UDPPort");
            if (!int.TryParse(this.UDPCheckSpace.Text, out Configs.net.UDPCheckSpace))
                Log.E("Net Init Err UDPCheckSpace");
            if (!int.TryParse(this.UDPTimeOut.Text, out Configs.net.UDPConnTimeOut))
                Log.E("Net Init Err UDPConnTimeOut");
            if (!int.TryParse(this.UDPMaxCon.Text, out Configs.net.UDPMaxConn))
                Log.E("Net Init Err UDPMaxConn");

            APP.WriteNetConfig();
        }

       

    }

}
