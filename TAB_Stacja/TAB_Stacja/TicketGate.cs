using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Expr;
using System;
using System.Collections;
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
    public partial class TicketGate : Form
    {
        string id = "-1";
        int user;
        int lift = 1;
        string typ;
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public TicketGate(int user)
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
                string query = "SELECT Bilety.id_biletu AS ID, rodzaj_biletu as RODZAJ, data_zakupu as DATA, data_konca as WYGASA FROM Bilety INNER JOIN Biletyczasowe ON Bilety.id_biletu=Biletyczasowe.id_biletu WHERE Biletyczasowe.data_konca >= CURRENT_TIME AND Bilety.id_narciarza = " + user + " UNION " +
                    "SELECT Bilety.id_biletu AS ID, rodzaj_biletu as RODZAJ, data_zakupu as DATA_ZAKUPU, ilość_przejazdow as WYGASA FROM Bilety INNER JOIN Biletypakietowe ON Bilety.id_biletu=Biletypakietowe.id_biletu WHERE biletypakietowe.ilość_przejazdow>0 AND Bilety.id_narciarza = " + user + " ";
                dataAdapter = new MySqlDataAdapter(query, database.getCon());
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                string query2 = "SELECT nazwa FROM Wyciagi;";
                MySqlCommand command2 = new MySqlCommand(query2, database.getCon());
                MySqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        comboBox1.Items.Add(reader2.GetString(0));
                    }
                    reader2.Close();
                    comboBox1.SelectedIndex = 0;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TicketGate_Load_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new UserForm(user).Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                id = Convert.ToString(selectedRow.Cells["ID"].Value);
                typ = Convert.ToString(selectedRow.Cells["RODZAJ"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                id = Convert.ToString(selectedRow.Cells["ID"].Value);
                typ = Convert.ToString(selectedRow.Cells["RODZAJ"].Value);
            }
            if (id == "-1")
            {
                MessageBox.Show("Wybierz bilet");
                return;
            }
            if (typ == "pakietowy")
            {
                try
                {
                    int nr = 0;
                    DateTime dateTime = DateTime.Now;
                    DatabaseConnector database = new DatabaseConnector();
                    database.getCon().Open();
                    string query = "UPDATE Biletypakietowe SET ilość_przejazdow = ilość_przejazdow-1 WHERE id_biletu=" + id + ";";
                    string query2 = "SELECT COALESCE(max(nr_uzycia),0)+1 FROM Historiabiletu WHERE id_biletu=" + id + ";";
                    MySqlCommand command2 = new MySqlCommand(query2, database.getCon());
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            nr = reader2.GetInt32(0);
                        }
                        reader2.Close();
                    }
                    database.getCon().Close();
                    string query3 = "INSERT INTO Historiabiletu VALUES(NULL, '" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + user + ", " + lift + ", " + id + ", " + nr + "); ";
                    database.exNonQuery(query);
                    database.exNonQuery(query3);
                    MessageBox.Show("Bilet skasowany poprawnie!");
                    return;

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Błąd kupowania biletu!");
                    return;
                }
            }
            else
            {   try
                {
                    int nr = 0;
                    DateTime dateTime = DateTime.Now;
                    DatabaseConnector database = new DatabaseConnector();
                    database.getCon().Open();
                    string query = "SELECT COALESCE(max(nr_uzycia),0)+1 FROM Historiabiletu WHERE id_biletu=" + id + ";";
                    MySqlCommand command2 = new MySqlCommand(query, database.getCon());
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            nr = reader2.GetInt32(0);
                        }
                        reader2.Close();
                    }
                    database.getCon().Close();
                    string query2 = "INSERT INTO Historiabiletu VALUES(NULL, '" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + user + ", " + lift + ", " + id + ", " + nr + ");";
                    database.exNonQuery(query2);
                    MessageBox.Show("Bilet skasowany poprawnie!");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lift = comboBox1.SelectedIndex + 1;
        }
    }
}
