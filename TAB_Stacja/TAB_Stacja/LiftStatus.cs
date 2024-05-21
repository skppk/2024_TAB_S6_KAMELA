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
    public partial class LiftStatus : Form
    {
        int where;
        public LiftStatus(int where)
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadData();
            this.where = where;
        }
        private MySqlDataAdapter dataAdapter;
        private DataTable dataTable;
        private void LoadData()
        {
            DatabaseConnector database = new DatabaseConnector();
            try
            {

                database.getCon().Open();
                string query = "SELECT id_wyciagu AS ID, ilosc_miejsc AS MIEJSCA, czy_czynny AS STATUS from wyciagi";

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

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void back_button_Click(object sender, EventArgs e)
        {
            if (where == 0)
            {
                new UserForm().Show();
                this.Close();
            }
            else
            {
                new LiftOperator().Show();
                this.Close();
            }
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void LiftStatus_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 2 && e.Value != null)
            {
                int v = Convert.ToInt32(e.Value); 
                if(v == 0)
                {
                    e.CellStyle.BackColor = Color.Red; 

                }
                else
                {
                    e.CellStyle.BackColor = Color.Green; 
                }
            }
        }
    }
}
