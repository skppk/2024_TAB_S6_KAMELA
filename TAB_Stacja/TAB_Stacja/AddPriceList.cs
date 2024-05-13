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
            int time1 = 0, time2 = 0, time3 = 0, time4 = 0;
            int pak1 = 0, pak2 = 0, pak3 = 0, pak4 = 0;

            // Walidacja ID
            if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ID cennika.");
                return;
            }

            // Walidacja biletów czasowych
            if (!int.TryParse(textBox2.Text, out time1) ||
                !int.TryParse(textBox4.Text, out time2) ||
                !int.TryParse(textBox5.Text, out time3) ||
                !int.TryParse(textBox6.Text, out time4))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ceny biletów czasowych.");
            }

            // Walidacja biletów pakietowych
            if (!int.TryParse(textBox10.Text, out pak1) ||
                !int.TryParse(textBox9.Text, out pak2) ||
                !int.TryParse(textBox8.Text, out pak3) ||
                !int.TryParse(textBox7.Text, out pak4))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ceny biletów pakietowych.");
                return;
            }

            DateTime date = dateTimePicker1.Value;
            string dayofweek = textBox3.Text.Trim();

            // Zabezpieczenie przed pustymi wartościami dla dnia tygodnia
            if (string.IsNullOrEmpty(dayofweek))
            {
                MessageBox.Show("Proszę wprowadzić poprawny dzień tygodnia.");
                return;
            }

            try
            {
                string cennikquery = "INSERT INTO Cennik(id_cennika, data_obowiazywania, dzien_tygodnia) VALUES (" + id + ", '" + date.ToString("yyyy-MM-dd") + "', '" + dayofweek + "');";
                DatabaseConnector database = new DatabaseConnector();
                database.exNonQuery(cennikquery);


                string cennikczas = "INSERT INTO Cennikczasowy(dlugosc_obowiazywania, cena, id_c) VALUES ('1h', " + time1 + ", " + id + "), ('2h', " + time2 + ", " + id + "), ('3h', " + time3 + ", " + id + "), ('5h', " + time4 + ", " + id + ");";
                string cennikpak = "INSERT INTO Cennikpakietowy(ilosc_przejazdow, cena, id_c) VALUES (2, " + pak1 + ", " + id + "), (5, " + pak2 + ", " + id + "), (10, " + pak3 + ", " + id + "), (20, " + pak4 + ", " + id + ");";

                database.exNonQuery(cennikczas);
                database.exNonQuery(cennikpak);
                MessageBox.Show("Cennik dodany poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd dodawania cennika!");
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
