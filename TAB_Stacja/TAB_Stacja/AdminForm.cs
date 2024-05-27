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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
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

        private void button5_Click(object sender, EventArgs e)
        {
            new PriceList().Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new ModifyLift().Show();   
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new LiftRapport().Show();
            this.Close();
        }
    }
}
