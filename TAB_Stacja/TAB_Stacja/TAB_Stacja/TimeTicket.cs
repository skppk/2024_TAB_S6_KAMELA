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
    public partial class TimeTicket : Form
    {
        public TimeTicket()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;

            for (int x = 0; x < 7; x++)
            {
                if (index != x)
                {
                    checkedListBox1.SetItemChecked(x, false);
                }
                else
                {
                    switch (x)
                    {
                        case 0:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "20.00zł";
                            break;
                        case 1:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "35.00zł";
                            break;
                        case 2:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "50.00zł";
                            break;
                        case 3:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "90.00zł";
                            break;
                        case 4:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "110.00zł";
                            break;
                        case 5:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "300.00zł";
                            break;
                        case 6:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "650.00zł";
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserForm().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
