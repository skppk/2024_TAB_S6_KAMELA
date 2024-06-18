using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAB_Stacja
{
    public partial class UserRapport : Form
    {
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        int user;
        public UserRapport(int user)
        {
            InitializeComponent();
            InitializeDataGridView();
            this.user = user;
        }

        private void LoadData()
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                database.getCon().Open();
                DateTime date = DateTime.Now;
                string query = "SELECT h.data_uzycia AS DATA, w.nazwa AS WYCIĄG, h.id_biletu AS BILET, b.rodzaj_biletu AS RODZAJ, nr_uzycia AS UZYCIE FROM Historiabiletu h JOIN Wyciagi w ON h.id_wyciagu=w.id_wyciagu JOIN Bilety b ON h.id_biletu=b.id_biletu  WHERE h.id_narciarza=" + user + ";";
                dataAdapter = new MySqlDataAdapter(query, database.getCon());
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
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

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserForm(user).Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UserRapport_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
