using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IFramework;
namespace FormSever
{
    public partial class SeverForm : Form
    {
        public SeverForm()
        {
            InitializeComponent();
            ReadLogConfig();
            ReadNetConfig();
        }

        private void data_save_Click(object sender, EventArgs e)
        {
            APP.datas.Save();
        }
    }
}
