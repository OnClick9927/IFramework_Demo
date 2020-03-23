using System;
using System.Drawing;
using System.Windows.Forms;
using IFramework;
namespace FormSever
{
    class Logger_Winfom : ILoger
    {
        public Logger_Winfom(ListView view)
        {
            m_view = view;
            m_view.GridLines = true;
            m_view.FullRowSelect = true;
            m_view.View = View.Details;
            m_view.Scrollable = true;
            m_view.MultiSelect = false;
            m_view.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            m_view.Columns.AddRange(new ColumnHeader[] {
                    new ColumnHeader()
                    {
                        ImageKey="Time",
                        Text="Time",
                        Width=100
                    },
                    new ColumnHeader()
                    {
                        ImageKey="Info",
                        Text="Info",
                        Width=-m_view.Width-100
                    }
                });
            
        }

        public ListView m_view { get; }

        public void Log(LogType logType, object messages, params object[] paras)
        {
            if (m_view.Items.Count>1000)
            {
                m_view.Items.Clear();
            }
            m_view.Items.Add(new ListViewItem());

            var item = m_view.Items[m_view.Items.Count - 1];
            switch (logType)
            {
                case LogType.Error:
                    item.BackColor = Color.Red;
                    break;
                case LogType.Warning:
                    item.BackColor = Color.Yellow;

                    break;
                case LogType.Log:
                    item.BackColor = Color.White;

                    break;
            }
            item.Text = DateTime.Now.ToString("HH:mm:ss");
            string txt = messages.ToString();

            if (paras != null)
                paras.ForEach((p) =>
                {
                    txt +=" "+ p.ToString();
                });
            item.SubItems.Add(new ListViewItem.ListViewSubItem()
            {
                Text = txt
            }
            );
            if (m_view!=null && m_view.Columns.Count>0)
            {
                m_view.Columns[1].Width = -1;
                m_view.EnsureVisible(m_view.Items.Count - 1);
            }

        }

        public void LogFormat(LogType logType, string format, object messages, params object[] paras)
        {
            m_view.Items.Add(new ListViewItem());

            var item = m_view.Items[m_view.Items.Count - 1];
            item.BackColor = logType == LogType.Log ? Color.White : logType == LogType.Error ? Color.Red : Color.White;
            item.Text =DateTime.Now.ToString("HH:mm:ss");
            item.SubItems.Add(new ListViewItem.ListViewSubItem()
                    {
                        Text = string.Format(format, messages, paras)
                    }
            );
        }
    }
}
