using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAB_Stacja
{
    internal class DatabaseConnector
    {
        private static string connstring;
        private static MySqlConnection con;
        public DatabaseConnector()
        {
            connstring = "server=localhost;uid=root;pwd=root;database=mydb";
            con = new MySqlConnection(connstring);
        }

        public MySqlConnection getCon()
        {
            return con;
        }
        public void showTable()
        {
            try
            {
                con.Open();
                Console.WriteLine("Connected successfully to database");

                /*string insert_prac = "insert into pracownicy values (1, 'obsługa'), (2, 'administrator'), (3, 'zarząd'), (4, 'obsługa')";
                MySqlCommand cmd2 = new MySqlCommand(insert_prac, con);
                cmd2.ExecuteNonQuery();*/

                string sql = "select * from pracownicy";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Tabela pracownicy:");
                while (reader.Read())
                {
                    Console.WriteLine("ID: " + reader.GetInt32(0));
                    Console.WriteLine("Stanowisko: " + reader.GetString(1));
                }
                reader.Close();

                con.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void exNonQuery(string query)
        {
            try
            {
                con.Open();
                string sql = query;
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException ex) {
                MessageBox.Show("Błąd polecenia SQL");
                MessageBox.Show(ex.ToString());
                con.Close();
                throw ex;
            }
        }
    }
}
