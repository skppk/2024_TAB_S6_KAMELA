using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAB_Stacja
{
    public partial class PacketTicket : Form
    {
        public PacketTicket()
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
                            price.Text = "15.00zł";
                            break;
                        case 1:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "25.00zł";
                            break;
                        case 2:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "35.00zł";
                            break;
                        case 3:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "45.00zł";
                            break;
                        case 4:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "65.00zł";
                            break;
                        case 5:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "100.00zł";
                            break;
                        case 6:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "190.00zł";
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
