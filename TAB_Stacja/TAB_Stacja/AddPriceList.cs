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

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int id = 0;
            float time1 = 0.00f, time2 = 0.00f, time3 = 0.00f, time4 = 0.00f, time5 = 0.00f, time6 = 0.00f, time7 = 0.00f;
            float pak1 = 0.00f, pak2 = 0.00f, pak3 = 0.00f, pak4 = 0.00f, pak5 = 0.00f, pak6 = 0.00f, pak7 = 0.00f;

            // Walidacja ID
            /*if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ID cennika.");
                return;
            }*/

            // Walidacja biletów czasowych
            if (!float.TryParse(textBox2.Text, out time1) ||
                !float.TryParse(textBox4.Text, out time2) ||
                !float.TryParse(textBox5.Text, out time3) ||
                !float.TryParse(textBox6.Text, out time4) ||
                !float.TryParse(textBox13.Text, out time5) ||
                !float.TryParse(textBox12.Text, out time6) ||
                !float.TryParse(textBox11.Text, out time7))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ceny biletów czasowych.");
                return;
            }

            // Walidacja biletów pakietowych
            if (!float.TryParse(textBox10.Text, out pak1) ||
                !float.TryParse(textBox9.Text, out pak2) ||
                !float.TryParse(textBox8.Text, out pak3) ||
                !float.TryParse(textBox7.Text, out pak4) ||
                !float.TryParse(textBox16.Text, out pak5) ||
                !float.TryParse(textBox15.Text, out pak6) ||
                !float.TryParse(textBox14.Text, out pak7))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ceny biletów pakietowych.");
                return;
            }

            DateTime date = dateTimePicker1.Value;
            string dayofweek = textBox3.Text.Trim();

            // Zabezpieczenie przed pustymi wartościami dla dnia tygodnia
            if (string.IsNullOrEmpty(dayofweek))
            {
                dayofweek = "all";
            }

            try
            {
                string cennikquery = "INSERT INTO Cennik(data_obowiazywania, dzien_tygodnia) VALUES ('" + date.ToString("yyyy-MM-dd") + "', '" + dayofweek + "');";
                DatabaseConnector database = new DatabaseConnector();
                database.exNonQuery(cennikquery);
                string query2 = "SELECT MAX(id_cennika) FROM Cennik;";
                database.getCon().Open();
                MySqlCommand command2 = new MySqlCommand(query2, database.getCon());
                MySqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        id = reader2.GetInt32(0);
                    }
                    reader2.Close();
                }
                database.getCon().Close();

                string cennikczas = "INSERT INTO Cennikczasowy(dlugosc_obowiazywania, cena, id_c) VALUES ('1h', " + time1 + ", " + id + "), ('2h', " + time2 + ", " + id + "), ('3h', " + time3 + ", " + id + "), ('6h', " + time4 + ", " + id + "), ('1d', " + time5 + ", " + id + "), ('3d', " + time6 + ", " + id + "), ('1t', " + time7 + ", " + id + ");";
                string cennikpak = "INSERT INTO Cennikpakietowy(ilosc_przejazdow, cena, id_c) VALUES (5, " + pak1 + ", " + id + "), (10, " + pak2 + ", " + id + "), (15, " + pak3 + ", " + id + "), (20, " + pak4 + ", " + id + "), (30, " + pak5 + ", " + id + "), (50, " + pak6 + ", " + id + "), (100, " + pak7 + ", " + id + ");";

                database.exNonQuery(cennikczas);
                database.exNonQuery(cennikpak);
                MessageBox.Show("Cennik dodany poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd dodawania cennika!");
                MessageBox.Show(ex.Message);
            }
        }

        private void AddPriceList_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
