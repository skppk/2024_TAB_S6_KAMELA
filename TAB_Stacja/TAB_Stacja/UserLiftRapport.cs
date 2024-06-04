using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
    public partial class UserLiftRapport : Form
    {
        string date = "1970-01-01";
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public UserLiftRapport()
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
                //DateTime date = DateTime.Now;
                string query = "SELECT t.poziom_trudnosci AS TRASA, t.długosc AS DŁUGOŚĆ, w.nazwa AS WYCIĄG, h.data_uzycia AS DATA FROM Trasy t JOIN Wyciagi w ON t.id_wyciagu = w.id_wyciagu JOIN Historiabiletu h ON h.id_wyciagu = w.id_wyciagu WHERE h.id_narciarza = 1";
                if (!date.Equals("1970-01-01"))
                {
                    query += " AND DATE(h.data_uzycia) = '" + date + "'";
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
                decimal totalLength = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["DŁUGOŚĆ"] != DBNull.Value)
                    {
                        totalLength += Convert.ToDecimal(row["DŁUGOŚĆ"]);
                    }
                }
                DataRow summaryRow = dataTable.NewRow();
                summaryRow["TRASA"] = "SUMA";
                summaryRow["DŁUGOŚĆ"] = totalLength;
                dataTable.Rows.Add(summaryRow);
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
            HashSet<string> uniqueDates = new HashSet<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["DATA"] != DBNull.Value)
                {
                    DateTime dateTime = DateTime.Parse(row["DATA"].ToString());
                    uniqueDates.Add(dateTime.ToString("yyyy-MM-dd"));
                }
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Wszystko");
            comboBox1.Items.AddRange(uniqueDates.ToArray());
            /*DatabaseConnector database = new DatabaseConnector();
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
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserForm(1).Show();
            this.Close();
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

        private void UserLiftRapport_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                date = "1970-01-01";
                LoadData();
                return;
            }
            date = comboBox1.GetItemText(comboBox1.SelectedItem);
            LoadData();
        }
    }
}
