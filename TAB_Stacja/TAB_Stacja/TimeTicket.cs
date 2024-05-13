using MySql.Data.MySqlClient;
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
        int choosen = 0;
        public TimeTicket()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (choosen == 0)
            {
                MessageBox.Show("Nie wybrano biletu!");
                return;
            }

            DateTime date = DateTime.Now;
            DateTime expire = date.AddHours(choosen);
            try
            {
                string query = "INSERT INTO Bilety VALUES (NULL, 'czasowy', '" + date.ToString("yyyy-MM-dd") + "', 0, 1, 1, 1, 1);";
                DatabaseConnector database = new DatabaseConnector();
                database.exNonQuery(query);


                string query2 = "INSERT INTO Biletyczasowe VALUES (NULL, '" + date.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + expire.ToString("yyyy-MM-dd HH:mm:ss") + "', (SELECT MAX(id_biletu) FROM Bilety));";

                database.exNonQuery(query2);
                MessageBox.Show("Bilet zakupiony poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd kupowania biletu!");
            }
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
                            choosen = 1;
                            break;
                        case 1:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "35.00zł";
                            choosen = 2;
                            break;
                        case 2:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "50.00zł";
                            choosen = 3;
                            break;
                        case 3:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "90.00zł";
                            choosen = 6;
                            break;
                        case 4:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 24;
                            break;
                        case 5:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "300.00zł";
                            choosen = 72;
                            break;
                        case 6:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = "650.00zł";
                            choosen = 168;
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

        private void TimeTicket_Load(object sender, EventArgs e)
        {

        }
    }
}
