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
    public partial class LiftRapport : Form
    {
        int lift = 0;
        int skier = 0;
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public LiftRapport()
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
                string query = "SELECT h.data_uzycia AS DATA, w.nazwa AS WYCIĄG, h.id_biletu AS BILET, o.Nazwisko AS NAZWISKO FROM Historiabiletu h JOIN Wyciagi w ON h.id_wyciagu=w.id_wyciagu JOIN Bilety b ON h.id_biletu=b.id_biletu JOIN Narciarze n ON h.id_narciarza=n.id_narciarza JOIN Osoby o ON n.id_osoby=o.id ";
                if(skier!=0 || lift !=0) { 
                    query += ((skier == 0) ? "" : "WHERE h.id_narciarza = " + skier);
                    query += ((lift == 0) ? "" : (skier == 0 ? "WHERE w.id_wyciagu=" + lift : " AND w.id_wyciagu=" + lift));
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
                comboBox2.Items.Add("Wszystkie");
                comboBox1.Items.Add("Wszyscy");
                database.getCon().Open();
                string query2 = "SELECT nazwa FROM Wyciagi;";
                MySqlCommand command2 = new MySqlCommand(query2, database.getCon());
                MySqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        comboBox2.Items.Add(reader2.GetString(0));
                    }
                    reader2.Close();
                }
                string query3 = "SELECT nazwisko FROM Osoby o JOIN Narciarze n ON o.id=n.id_osoby;";
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
        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LiftRapport_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCombo();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lift = comboBox2.SelectedIndex;
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            skier = comboBox1.SelectedIndex;
            LoadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
