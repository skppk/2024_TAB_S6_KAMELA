﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TAB_Stacja
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Registration().Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password_txt.PasswordChar = '\u25CF';


        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // czyszczenie textboxów z loginem i hasłem
            login_txt.Clear();
            password_txt.Clear();

            login_txt.Focus();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (password_txt.Text == "user" && login_txt.Text == "user")
            {
                new UserForm().Show();
                this.Hide();
            }
            else if (password_txt.Text == "admin" && login_txt.Text == "admin")
            {
                new AdminForm().Show();
                this.Hide();
            }
            else if (password_txt.Text == "seller" && login_txt.Text == "seller")
            {
                new SellerForm().Show();
                this.Hide();
            }
            else if (password_txt.Text == "service" && login_txt.Text == "service")
            {
                new ServiceForm().Show();
                this.Hide();
            }
            else if (password_txt.Text == "management" && login_txt.Text == "management")
            {
                new ManagementForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Niepoprawne hasło.");
            }


        }
    }
}