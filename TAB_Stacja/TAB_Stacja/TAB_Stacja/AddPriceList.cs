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
    public partial class AddPriceList : Form
    {
        public AddPriceList()
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

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox2.SelectedIndex;

            for (int x = 0; x < 2; x++)
            {
                if (index != x)
                {
                    checkedListBox2.SetItemChecked(x, false);
                }
                else
                {
                    checkedListBox2.SetItemChecked(x, true);
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;

            if (index != 0)
            {
                checkedListBox1.SetItemChecked(0, false);
            }
            else
            {
                checkedListBox1.SetItemChecked(0, true);
            }
        }
    }
}
