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
    public partial class WorkersRapport : Form
    {
        int worker = 0;
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public WorkersRapport()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void LoadData()
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                database.getCon().Open();
                DateTime date = DateTime.Now;
                string query = "SELECT o.Nazwisko AS NAZWISKO, b.id_biletu AS ID_BILETU, b.rodzaj_biletu AS RODZAJ, b.data_zakupu AS DATA, pp.nazwa AS PUNKT_SPRZEDAŻY FROM Osoby o JOIN Pracownicy p ON o.id = p.id_osoby JOIN Bilety b ON b.id_pracownika = p.id_pracownika JOIN Punktsprzedazy pp ON pp.id_pracownika = b.id_pracownika ";
                if (worker != 0)
                {
                    query += "WHERE p.id_pracownika = " + worker;
                }
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

        private void LoadCombo()
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                comboBox1.Items.Add("Wszyscy");
                database.getCon().Open();
                string query3 = "SELECT nazwisko FROM Osoby o JOIN Pracownicy p ON o.id=p.id_osoby WHERE p.stanowisko = 'sprzedawca';";
                MySqlCommand command3 = new MySqlCommand(query3, database.getCon());
                MySqlDataReader reader3 = command3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        comboBox1.Items.Add(reader3.GetString(0));
                    }
                    reader3.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            new AdminForm().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void WorkersRapport_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                worker = 0;
                LoadData();
                return;
            }
            DatabaseConnector database = new DatabaseConnector();
            try
            {
                database.getCon().Open();
                string query3 = "SELECT id_pracownika FROM Osoby o JOIN Pracownicy p ON o.id=p.id_osoby WHERE o.nazwisko = '" + comboBox1.GetItemText(comboBox1.SelectedItem) + "';";
                MySqlCommand command3 = new MySqlCommand(query3, database.getCon());
                MySqlDataReader reader3 = command3.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        worker = reader3.GetInt16(0);
                    }
                    reader3.Close();
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
            //worker = comboBox1.SelectedIndex;
            LoadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
