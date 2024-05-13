using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAB_Stacja
{
    public partial class RemovePriceList : Form
    {
        public RemovePriceList()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new PriceList().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
