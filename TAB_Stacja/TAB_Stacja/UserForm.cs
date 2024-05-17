using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TAB_Stacja
{
    public partial class UserForm : Form
    {
        string checkedItemText;
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public UserForm()
        {
           //jesli sa aktywne bilety to dodaj do label1
            InitializeComponent();
            InitializeDataGridView();
        }

        private void LoadData()
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {  
                database.getCon().Open();
                string query = "SELECT rodzaj_biletu as RODZAJ, data_zakupu as DATA, data_konca as WYGASA FROM Bilety INNER JOIN Biletyczasowe ON Bilety.id_biletu=Biletyczasowe.id_biletu UNION " +
                    "SELECT rodzaj_biletu as RODZAJ, data_zakupu as DATA_ZAKUPU, ilość_przejazdow as WYGASA FROM Bilety INNER JOIN Biletypakietowe ON Bilety.id_biletu=Biletypakietowe.id_biletu"; 
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

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            new Login().Show(); 
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new EditUserTickets().Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;

            for (int x = 0; x < 2; x++)
            {
                if (index != x)
                {
                    checkedListBox1.SetItemChecked(x, false);
                }
                else
                {
                    checkedListBox1.SetItemChecked(x, true);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems)
            {
                checkedItemText = item.ToString();

                if (checkedItemText == "Czasowy")
                {
                    new TimeTicket().Show();
                    this.Hide();
                }
                if (checkedItemText == "Pakietowy")
                {
                    new PacketTicket().Show();
                    this.Hide();
                }


            }

            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Proszę zaznaczyć co jedną opcję aby przejść dalej.");
            }
        }
    }
}
