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
    public partial class ChangeLiftStatus : Form
    {
        public ChangeLiftStatus()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new LiftOperator().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = 0;
            string czynny = "FALSE";

            // Walidacja ID
            if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ID wyciągu.");
                return;
            }

            if (checkBox1.Checked)
            {
                czynny = "TRUE";
            }

            try
            {
                string query = "UPDATE Wyciagi SET czy_czynny="+czynny+" WHERE id_wyciagu="+id+"; ";
                DatabaseConnector database = new DatabaseConnector();
                database.exNonQuery(query);
                MessageBox.Show("Status zmieniony poprawnie!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Błąd zmiany statusu!");
            }
        }
    }
}
