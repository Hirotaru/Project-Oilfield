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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                DialogResult = DialogResult.None;
                return;
            }

            if (textBox2.Text == "")
            {
                DialogResult = DialogResult.None;
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UD.mdf;Integrated Security=True;Connect Timeout=30;");

            SqlDataAdapter ad = new SqlDataAdapter("select count(*) from Login where username = '" + textBox1.Text + "'", con);

            DataTable dt = new DataTable();

            ad.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                errorProvider1.SetError(textBox1, "User with this name is already exists");
                DialogResult = DialogResult.None;
                return;
            }

                //SqlDataAdapter ad = new SqlDataAdapter("insert into Login (Username, Password, Admin) values (" + textBox1.Text + ", " + textBox2.Text + ", "+ (checkBox1.Checked ? 1.ToString() : 0.ToString()) + ")", con);

            SqlCommand command = new SqlCommand("insert into Login (Username, Password, Admin) values ('" + textBox1.Text + "', '" + textBox2.Text + "', " + (checkBox1.Checked ? 1.ToString() : 0.ToString()) + ")", con);
            con.Open();
            command.ExecuteNonQuery();

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
