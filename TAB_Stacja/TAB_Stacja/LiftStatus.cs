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
        int liftScheduleID = 0;
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
            database.getCon().Open();
            DateTime date = DateTime.Now;
            string query = "SELECT id_rozkladu FROM Rozklady WHERE data_obowiazywania <= '" + date.ToString("yyyy-MM-dd") + "' ORDER BY data_obowiazywania DESC, id_rozkladu DESC LIMIT 1;";
            MySqlCommand command = new MySqlCommand(query, database.getCon());
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    liftScheduleID = reader.GetInt16(0);
                }
                reader.Close();
                try
                {
                    string query2 = "SELECT w.id_wyciagu AS ID, w.ilosc_miejsc AS MIEJSCA, w.czy_czynny AS STATUS, r.godz_otwarcia AS OTWARCIE, r.godz_zamkniecia AS ZAMKNIECIE FROM Wyciagi w JOIN (" +
                        "SELECT id_wyciagu, godz_otwarcia, godz_zamkniecia FROM Rozklady WHERE (id_wyciagu, data_obowiazywania, id_rozkladu) IN (SELECT id_wyciagu, data_obowiazywania, MAX(id_rozkladu) AS id_rozkladu FROM Rozklady WHERE data_obowiazywania <= CURRENT_DATE GROUP BY id_wyciagu, data_obowiazywania) AND data_obowiazywania = (SELECT MAX(data_obowiazywania) FROM Rozklady r2 WHERE r2.id_wyciagu = Rozklady.id_wyciagu AND r2.data_obowiazywania <= CURRENT_DATE)" +
                        ") r ON w.id_wyciagu = r.id_wyciagu;";

                    dataAdapter = new MySqlDataAdapter(query2, database.getCon());
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;



                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    TimeSpan currentTime = DateTime.Now.TimeOfDay;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["ZAMKNIECIE"].Value != null)
                        {
                            if (TimeSpan.TryParse(row.Cells["ZAMKNIECIE"].Value.ToString(), out TimeSpan cellTime))
                            {
                                if (cellTime < currentTime)
                                {
                                    row.Cells["STATUS"].Value = false;
                                }
                            }
                        }
                        if (row.Cells["OTWARCIE"].Value != null)
                        {
                            if (TimeSpan.TryParse(row.Cells["OTWARCIE"].Value.ToString(), out TimeSpan cellTime))
                            {
                                if (cellTime > currentTime)
                                {
                                    row.Cells["STATUS"].Value = false;
                                }
                            }
                        }
                    }

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
            if (e.ColumnIndex == 2 && e.Value != null)
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
    }
}
