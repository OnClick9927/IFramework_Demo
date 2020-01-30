using FormSever.Net;
using IFramework;
using System;
using System.Windows.Forms;

namespace FormSever
{
    partial class SeverForm
    {
        NetSever netSever;
        private void ReadNetConfig()
        {
            this.TCPBufferSize.Text = NetConfig.TCPBuffersize.ToString();
            this.TCPPort.Text = NetConfig.TCPPort.ToString();
            this.TcpCheckSpace.Text = NetConfig.TCPCheckSpace.ToString();
            this.TCPTimeOut.Text = NetConfig.TCPConnTimeOut.ToString();
            this.TCPMaxCon.Text = NetConfig.TCPMaxConn.ToString();
            this.UDPBufferSize.Text = NetConfig.UDPBuffersize.ToString();
            this.UDPPort.Text = NetConfig.UDPPort.ToString();
            this.UDPCheckSpace.Text = NetConfig.UDPCheckSpace.ToString();
            this.UDPTimeOut.Text = NetConfig.UDPConnTimeOut.ToString();
            this.UDPMaxCon.Text = NetConfig.UDPMaxConn.ToString();
        }
        private void Net_Init_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(this.TCPBufferSize.Text, out NetConfig.TCPBuffersize))
                Log.E("Net Init Err  TCPBuffersize");
            if (!int.TryParse(this.TCPPort.Text, out NetConfig.TCPPort))
                Log.E("Net Init Err TCPPort");
            if (!int.TryParse(this.TcpCheckSpace.Text, out NetConfig.TCPCheckSpace))
                Log.E("Net Init Err TCPCheckSpace");
            if (!int.TryParse(this.TCPTimeOut.Text, out NetConfig.TCPConnTimeOut))
                Log.E("Net Init Err TCPConnTimeOut");
            if (!int.TryParse(this.TCPMaxCon.Text, out NetConfig.TCPMaxConn))
                Log.E("Net Init Err TCPMaxConn");

            if (!int.TryParse(this.UDPBufferSize.Text, out NetConfig.UDPBuffersize))
                Log.E("Net Init Err UDPBuffersize");
            if (!int.TryParse(this.UDPPort.Text, out NetConfig.UDPPort))
                Log.E("Net Init Err UDPPort");
            if (!int.TryParse(this.UDPCheckSpace.Text, out NetConfig.UDPCheckSpace))
                Log.E("Net Init Err UDPCheckSpace");
            if (!int.TryParse(this.UDPTimeOut.Text, out NetConfig.UDPConnTimeOut))
                Log.E("Net Init Err UDPConnTimeOut");
            if (!int.TryParse(this.UDPMaxCon.Text, out NetConfig.UDPMaxConn))
                Log.E("Net Init Err UDPMaxConn");
        }

        private void NetRun_Click(object sender, EventArgs e)
        {
            APP.GoToGame();
        }

    }

}
