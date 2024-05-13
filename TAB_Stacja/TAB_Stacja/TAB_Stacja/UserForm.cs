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
    public partial class UserForm : Form
    {
        string checkedItemText;
        public UserForm()
        {
           //jesli sa aktywne bilety to dodaj do label1
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            new Login().Show(); 
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new EditUserTickets().Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;

            for (int x = 0; x < 2; x++)
            {
                if (index != x)
                {
                    checkedListBox1.SetItemChecked(x, false);
                }
                else
                {
                    checkedListBox1.SetItemChecked(x, true);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems)
            {
                checkedItemText = item.ToString();

                if (checkedItemText == "Czasowy")
                {
                    new TimeTicket().Show();
                    this.Hide();
                }
                if (checkedItemText == "Pakietowy")
                {
                    new PacketTicket().Show();
                    this.Hide();
                }


            }

            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Proszę zaznaczyć co jedną opcję aby przejść dalej.");
            }
        }
    }
}
