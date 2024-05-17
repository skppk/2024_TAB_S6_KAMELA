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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TAB_Stacja
{
    public partial class AddLiftSchedule : Form
    {
        public AddLiftSchedule()
        {
            InitializeComponent();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ModifyLift().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = 0;

            // Walidacja ID
            if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ID wyciągu.");
                return;
            }

            DateTime date = dateTimePicker1.Value;
            TimeSpan open = dateTimePicker2.Value.TimeOfDay;
            TimeSpan close = dateTimePicker3.Value.TimeOfDay;
            string dayofweek = textBox3.Text.Trim();

            // Zabezpieczenie przed pustymi wartościami dla dnia tygodnia
            if (string.IsNullOrEmpty(dayofweek))
            {
                dayofweek = "all";
            }

            try
            {
                string query = "INSERT INTO Rozklady VALUES (NULL, '" + dayofweek + "', '" + open + "', '" + close + "', " + id + ", '" + date.ToString("yyyy-MM-dd") + "'); ";
                DatabaseConnector database = new DatabaseConnector();
                database.exNonQuery(query);
                MessageBox.Show("Rozkład dodany poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd dodawania rozkładu!");
            }
        }

        private void AddLiftSchedule_Load(object sender, EventArgs e)
        {

        }
    }
}
