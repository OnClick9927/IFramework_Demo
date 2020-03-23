using FormSever.Net;
using IFramework;
using System;
using System.Windows.Forms;
using IFramework.Modules.NodeAction;
namespace FormSever
{
    public partial class SeverForm : Form
    {
        public SeverForm()
        {
            InitializeComponent();
            ReadLogConfig();
            ReadNetConfig();
            APP.form = this;
        }

       

        private void data_save_Click(object sender, EventArgs e)
        {
            APP.datas.Save();
            APP.gameMap.Save();
        }
        SequenceNode sq;
        private void message_send_Click(object sender, EventArgs e)
        {
             string str= message_message.Text;
            if (sq!=null)
            {
                sq.Recyle();
            }
          sq=  this.Sequence(APP.env)
                .Repeat((r) => {
                    r.Sequence((s) =>
                    {
                        s.Event(() =>
                        {
                            APP.net.SendTcpMessageToAll(new SeverFormBroadCast() { message = str });

                        })
                        .TimeSpan(TimeSpan.FromSeconds(Message_gap_slider.Value));
                    });
                }, MesaggSlider.Value)
                .Run();
        }


        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            MessageCount.Text = string.Format("Send Count {0}", MesaggSlider.Value);
        }

        private void Message_gap_slider_Scroll(object sender, ScrollEventArgs e)
        {
            MessageGap.Text = string.Format("Send Gap {0} s", Message_gap_slider.Value);
        }
    }
    [NetMessage(11)]
    public class  SeverFormBroadCast:INetMessage
    {
        public string message;
    }
}
