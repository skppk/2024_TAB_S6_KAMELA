using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TAB_Stacja
{
    public partial class EditUserTickets : Form
    {
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        int user = 0;
        string id = "-1";
        string typ;
        public EditUserTickets(int user)
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
                string query = "SELECT Bilety.id_biletu AS ID, rodzaj_biletu as RODZAJ, data_zakupu as DATA, data_konca as WYGASA, czy_aktywny AS AKTYWNY FROM Bilety INNER JOIN Biletyczasowe ON Bilety.id_biletu=Biletyczasowe.id_biletu WHERE Biletyczasowe.data_konca >= CURRENT_TIME AND Bilety.id_narciarza = " + user + " UNION " +
                    "SELECT Bilety.id_biletu AS ID, rodzaj_biletu as RODZAJ, data_zakupu as DATA_ZAKUPU, ilość_przejazdow as WYGASA, czy_aktywny AS AKTYWNY FROM Bilety INNER JOIN Biletypakietowe ON Bilety.id_biletu=Biletypakietowe.id_biletu WHERE Bilety.id_narciarza = " + user + " ";
                dataAdapter = new MySqlDataAdapter(query, database.getCon());
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "AKTYWNY";
                checkBoxColumn.Name = "AKTYWNY";
                checkBoxColumn.DataPropertyName = "AKTYWNY";

                dataGridView1.Columns.Add(checkBoxColumn);

                dataGridView1.Columns.Remove("AKTYWNY");
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

        private void button1_Click(object sender, EventArgs e)
        {
            new UserForm(user).Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EditUserTickets_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                int v = Convert.ToInt32(e.Value);
                if (v == 0)
                {
                    e.CellStyle.BackColor = Color.Red;

                }
                else
                {
                    e.CellStyle.BackColor = Color.Green;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
            try
            {
                DatabaseConnector database = new DatabaseConnector();
                string query = "UPDATE Bilety SET czy_aktywny = CASE WHEN czy_aktywny = 0 THEN 1 ELSE 0 END WHERE id_biletu = " + id + ";";
                database.exNonQuery(query);
                MessageBox.Show("Zmiana statusu biletu poprawna!");
                LoadData();
                return;
            }
            catch (MySqlException ex)
            { 
                MessageBox.Show("Błąd zmiany stanu biletu!");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
            if (typ == "czasowy")
            {
                new EditTimeTIcket(user, Convert.ToInt16(id)).Show();
                this.Close();
            }
            else
            {
                new EditPacketTicket(user, Convert.ToInt16(id)).Show();
                this.Close();
            }
        }
    }
}
