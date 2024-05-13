using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAB_Stacja
{
    public partial class RemovePriceList : Form
    {
        public RemovePriceList()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new PriceList().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            DatabaseConnector connector = new DatabaseConnector();
            
            int id = 0;

            // Sprawdzenie poprawności formatu 
            if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("Proszę wprowadzić poprawne ID cennika do usunięcia.");
                return;
            }

            // Usunięcie cennika z bazy
            string qrczas = "DELETE FROM Cennikczasowy WHERE `id_c` = " + id + ";";
            connector.exNonQuery(qrczas);
            string qrpak = "DELETE FROM Cennikpakietowy WHERE `id_c` = " + id + ";";
            connector.exNonQuery(qrpak);
            string qr = "DELETE FROM Cennik WHERE `id_cennika` = " + id + ";";
            connector.exNonQuery(qr);
        }
    }
}
