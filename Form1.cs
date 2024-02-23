using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Login
{
    public partial class Form1 : Form
    {
        string login;
        string password;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Login";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            login = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
            textBox2.UseSystemPasswordChar = true;
        }
        private void login_button_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

        void RunQuery()
        {
            string cs = @"server=localhost;userid=testing;password=Psik123;database=users"; 
            using (var con = new MySqlConnection(cs))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM login_password WHERE login = @login AND password = @password";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        label1.ForeColor = Color.Green;
                        label1.Text = "You have successfully logged into your account";
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Wrong password or login. Try again";
                    }
                }
            }
        }

    }
}
