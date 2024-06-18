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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void password_txt_TextChanged(object sender, EventArgs e)
        {
            password_txt.PasswordChar = '\u25CF';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                string query = "INSERT INTO Osoby VALUES(NULL, '" + textBox2.Text + "', '" + textBox1.Text + "');";
                database.exNonQuery(query);
                string query2 = "INSERT INTO Narciarze (id_osoby) SELECT MAX(id) FROM Osoby;";
                database.exNonQuery(query2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas ładowania danych: " + ex.Message);
            }
            finally
            {
                MessageBox.Show("Konto stworzone");
                database.getCon().Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            password_txt.PasswordChar = '\u25CF';
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }
    }
}
