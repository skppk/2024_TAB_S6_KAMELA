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
    public partial class WorkersManagement : Form
    {
        List<String> lifts = new List<String>();
        List<String> shops = new List<String>();
        public WorkersManagement()
        {
            InitializeComponent();
        }

        private void LoadCombo()
        {
            comboBox1.Items.Add("sprzedawca");
            comboBox1.Items.Add("obsługa");
            comboBox1.SelectedIndex = 0;
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                database.getCon().Open();
                string query = "SELECT nazwa FROM Wyciagi;";
                MySqlCommand command = new MySqlCommand(query, database.getCon());
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lifts.Add(reader.GetString(reader.GetOrdinal("nazwa")));
                    }
                    reader.Close();
                }
                string query2 = "SELECT nazwa FROM Punktsprzedazy;";
                MySqlCommand command2 = new MySqlCommand(query2, database.getCon());
                MySqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        shops.Add(reader2.GetString(reader2.GetOrdinal("nazwa")));
                    }
                    reader2.Close();
                }
                comboBox3.Items.AddRange(shops.ToArray());
                string query3 = "SELECT imię, nazwisko FROM Osoby o JOIN Pracownicy p ON o.id=p.id_osoby;";
                MySqlCommand command3 = new MySqlCommand(query3, database.getCon());
                MySqlDataReader reader3 = command3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        comboBox2.Items.Add(reader3.GetString(0) + " " + reader3.GetString(1));
                    }
                    reader3.Close();
                }
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas ładowania danych: " + ex.Message);
            }
            finally
            {
                database.getCon().Close();
            }
        }

        private void WorkersManagement_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AdminForm().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = -1;
            comboBox3.Items.Clear();

            if (comboBox1.SelectedIndex == 0)
            {
                comboBox3.Items.AddRange(shops.ToArray());
            }
            else
            {
                comboBox3.Items.AddRange(lifts.ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                string query = "UPDATE Pracownicy SET stanowisko='" + comboBox1.SelectedItem.ToString() + "' WHERE id_pracownika=" + (comboBox2.SelectedIndex+1) + ";";
                database.exNonQuery(query);
                string query2 = "UPDATE Punktsprzedazy SET id_pracownika=NULL WHERE id_pracownika=" + (comboBox2.SelectedIndex + 1) + "; UPDATE Wyciagi SET id_pracownika=NULL WHERE id_pracownika=" + (comboBox2.SelectedIndex + 1) + ";";
                database.exNonQuery(query2);
                string query3;
                if (comboBox1.SelectedIndex == 0)
                {
                    query3 = "UPDATE Punktsprzedazy SET id_pracownika=" + (comboBox2.SelectedIndex + 1) + " WHERE nazwa='" + comboBox3.SelectedItem.ToString() + "';";
                }
                else
                {
                    query3 = "UPDATE Wyciagi SET id_pracownika=" + (comboBox2.SelectedIndex + 1) + " WHERE nazwa='" + comboBox3.SelectedItem.ToString() + "';";
                }
                database.exNonQuery(query3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas ładowania danych: " + ex.Message);
            }
            finally
            {
                MessageBox.Show("Dane pracownika zmienione");
                database.getCon().Close();
            }
        }
    }
}
