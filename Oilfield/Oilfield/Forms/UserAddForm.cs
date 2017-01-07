using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oilfield
{
    public partial class UserAddForm : Form
    {
        public UserAddForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UD.mdf;Integrated Security=True;Connect Timeout=30;");

            //SqlDataAdapter ad = new SqlDataAdapter("insert into Login (Username, Password, Admin) values (" + textBox1.Text + ", " + textBox2.Text + ", "+ (checkBox1.Checked ? 1.ToString() : 0.ToString()) + ")", con);

            SqlCommand command = new SqlCommand("insert into Login (Username, Password, Admin) values ('" + textBox1.Text + "', '" + textBox2.Text + "', " + (checkBox1.Checked ? 1.ToString() : 0.ToString()) + ")", con);
            con.Open();
            command.ExecuteNonQuery();

            con.Close();
        }
    }
}
