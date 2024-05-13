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
    public partial class RemoveLiftSchedule : Form
    {
        public RemoveLiftSchedule()
        {
            InitializeComponent();
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

            try
            {
                string query = "DELETE FROM Rozklady WHERE id_rozkladu="+id+";";
                DatabaseConnector database = new DatabaseConnector();
                database.exNonQuery(query);
                MessageBox.Show("Rozkład usunięty poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd usuwania rozkładu!");
            }
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
    }
}
