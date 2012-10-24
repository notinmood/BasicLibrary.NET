using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HiLand.Framework.BusinessCore;

namespace HiLand.Framework.WindowsFormsConsole
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationService.LoadPlugins();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<Guid, IApplication> dic = ApplicationService.Plugins;
            this.textBox1.Text = string.Empty;
            foreach (KeyValuePair<Guid, IApplication> kvp in dic)
            {
                this.textBox1.Text += kvp.Value.ApplicationName + "\r\n";
            }
        }
    }
}
