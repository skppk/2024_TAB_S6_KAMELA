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
    public partial class LiftOperator : Form
    {
        public LiftOperator()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChangeLiftStatus().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new LiftStatus(1).Show();    
            this.Close();
        }
    }
}