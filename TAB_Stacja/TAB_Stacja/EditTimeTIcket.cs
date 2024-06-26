﻿using MySql.Data.MySqlClient;
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
    public partial class EditTimeTIcket : Form
    {
        int where;
        int user;
        int choosen = 0;
        int priceListID = -1;
        float multiplier = 1.0f;
        float[] prices = { 20.00f, 35.00f, 50.00f, 90.00f, 120.00f, 300.00f, 650.00f };
        public EditTimeTIcket(int user, int where)
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
                        string query2 = "SELECT cena FROM Cennikczasowy WHERE id_c = " + priceListID + ";";
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

        private void button2_Click(object sender, EventArgs e)
        {
            new UserForm(user).Show();
            this.Close();
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
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
                DatabaseConnector database = new DatabaseConnector();


                string query2 = "UPDATE Biletyczasowe SET data_konca = DATE_ADD(data_konca, INTERVAL " + choosen + " HOUR) WHERE id_biletu=" + where + ";";

                database.exNonQuery(query2);
                MessageBox.Show("Bilet przedłużony poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd przedłużenia biletu!");
            }
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
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
                            price.Text = (prices[0] * multiplier).ToString() + "zł";
                            choosen = 1;
                            break;
                        case 1:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = (prices[1] * multiplier).ToString() + "zł";
                            choosen = 2;
                            break;
                        case 2:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = (prices[2] * multiplier).ToString() + "zł";
                            choosen = 3;
                            break;
                        case 3:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = (prices[3] * multiplier).ToString() + "zł";
                            choosen = 6;
                            break;
                        case 4:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = (prices[4] * multiplier).ToString() + "zł";
                            choosen = 24;
                            break;
                        case 5:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = (prices[5] * multiplier).ToString() + "zł";
                            choosen = 72;
                            break;
                        case 6:
                            checkedListBox1.SetItemChecked(x, true);
                            price.Text = (prices[6] * multiplier).ToString() + "zł";
                            choosen = 168;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
