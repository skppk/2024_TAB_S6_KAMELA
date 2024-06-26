﻿using System;
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
    public partial class ModifyLift : Form
    {
        public ModifyLift()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            new AdminForm().Show();
            this.Close();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AddLiftSchedule().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RemoveLiftSchedule().Show();
            this.Close();
        }
    }
}
