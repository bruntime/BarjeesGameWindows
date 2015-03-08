using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barjees.Windows
{
    /// <summary>
    /// View the contents of log file
    /// </summary>
    public partial class LogWindow : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LogWindow()
        {
            InitializeComponent();
            openLogFileToolStripMenuItem.Click += openLogFileToolStripMenuItem_Click;
        }

        void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillItems(true);
        }
        /// <summary>
        /// Handle OnLoad event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillItems(false);
        }
        /// <summary>
        /// Create log list items
        /// </summary>
        private void FillItems(bool isOpenFile)
        {
            logList.Items.Clear();
            foreach(var line in GetContents(isOpenFile))
            {                
                string[] message = line.Split(new char[] {','});
                ListViewItem item = new ListViewItem(message[0]);
                foreach (string val in message.Skip(1))
                    item.SubItems.Add(val);

                logList.Items.Add(item);
            }
        }
        /// <summary>
        /// Return the log contents
        /// </summary>
        /// <param name="newFile"></param>
        /// <returns>True to open log file, False to open current log</returns>
        private string[] GetContents(bool newFile)
        {
            if(!newFile)
            {
                return Logging.Logger.GetContents();
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Select log file";
                dlg.InitialDirectory = Logging.Logger.GetLogFolder();
                dlg.FileName = "*.csv";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var contents = System.IO.File.ReadAllLines(dlg.FileName);
                        GameApp.Instance.SetStatus(dlg.FileName);
                        return contents.Skip(1).ToArray();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return GetContents(false);
                    }
                }
                else
                    return GetContents(false);
            }

        }
    }
}
