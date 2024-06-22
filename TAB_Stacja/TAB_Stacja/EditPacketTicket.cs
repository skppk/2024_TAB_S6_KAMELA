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
    public partial class EditPacketTicket : Form
    {
        int where;
        int user;
        int choosen = 0;
        int priceListID = -1;
        float multiplier = 1.0f;
        float[] prices = { 15.00f, 25.00f, 35.00f, 45.00f, 65.00f, 100.00f, 190.00f };
        public EditPacketTicket(int user, int where)
        {
            InitializeComponent();
            try
            {
                DatabaseConnector database = new DatabaseConnector();
                database.getCon().Open();
                DateTime date = DateTime.Now;
                string query = "SELECT id_cennika FROM Cennik WHERE data_obowiazywania <= '" + date.ToString("yyyy-MM-dd") + "' ORDER BY data_obowiazywania DESC, id_cennika DESC LIMIT 1;";
                MySqlCommand command = new MySqlCommand(query, database.getCon());
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        priceListID = reader.GetInt16(0);
                    }
                    reader.Close();
                    try
                    {
                        string query2 = "SELECT cena FROM Cennikpakietowy WHERE id_c = " + priceListID + ";";
                        MySqlCommand command2 = new MySqlCommand(query2, database.getCon());
                        MySqlDataReader reader2 = command2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            int i = 0;
                            while (reader2.Read())
                            {
                                prices[i] = reader2.GetFloat(0);
                                i++;
                            }
                            reader2.Close();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Błąd połączenia z bazą danych");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd połączenia z bazą danych");
            }

            this.where = where;
            this.user = user;
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
                            choosen = 5;
                            price.Text = (prices[0] * multiplier).ToString() + "zł";
                            break;
                        case 1:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 10;
                            price.Text = (prices[1] * multiplier).ToString() + "zł";
                            break;
                        case 2:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 15;
                            price.Text = (prices[2] * multiplier).ToString() + "zł";
                            break;
                        case 3:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 20;
                            price.Text = (prices[3] * multiplier).ToString() + "zł";
                            break;
                        case 4:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 30;
                            price.Text = (prices[4] * multiplier).ToString() + "zł";
                            break;
                        case 5:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 50;
                            price.Text = (prices[5] * multiplier).ToString() + "zł";
                            break;
                        case 6:
                            checkedListBox1.SetItemChecked(x, true);
                            choosen = 100;
                            price.Text = (prices[6] * multiplier).ToString() + "zł";
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserForm(user).Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (choosen == 0)
            {
                MessageBox.Show("Nie wybrano biletu!");
                return;
            }

            try
            {
                DatabaseConnector database = new DatabaseConnector();


                string query2 = "UPDATE Biletypakietowe SET ilość_przejazdow = ilość_przejazdow + " + choosen + " WHERE id_biletu=" + where + ";";

                database.exNonQuery(query2);
                MessageBox.Show("Bilet przedłużony poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd przedłużenia biletu!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
