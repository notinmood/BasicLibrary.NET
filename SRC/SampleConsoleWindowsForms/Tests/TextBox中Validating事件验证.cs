using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HiLand.Framework.WindowsFormsConsole.Tests
{
    public partial class TextBox中Validating事件验证 : Form
    {
        public TextBox中Validating事件验证()
        {
            InitializeComponent();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if ((TextBox)sender == textBox1)
            {
                this.label1.Text = textBox1.Text;
            }
        }
    }
}
