using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HiLand.Utility.Cache;

namespace HiLand.Framework.WindowsFormsConsole.Cache
{
    public partial class CacheTest : Form
    {
        public CacheTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ICache cache= CacheFactory.Create();
            cache.Insert("myKey","myValue");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ICache cache = CacheFactory.Create();
            this.button2.Text= cache.Get("myKey") as string;
        }
    }
}
