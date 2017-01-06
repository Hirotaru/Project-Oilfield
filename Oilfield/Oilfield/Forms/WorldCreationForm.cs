using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oilfield
{
    public partial class WorldCreationForm : Form
    {
        public WorldCreationForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public bool Training
        {
            get { return checkBox1.Checked; }
        }

        public int Iterations
        {
            get { return Convert.ToInt32(textBox1.Text); }
        }

        private void WorldCreationForm_Load(object sender, EventArgs e)
        {
            worldWidthTB.Text = 100.ToString();
            worldHeightTB.Text = 100.ToString();


        }

        public int WorldWidth
        {
            get { return Convert.ToInt32(worldWidthTB.Text); }
        }

        public int WorldHeight
        {
            get { return Convert.ToInt32(worldHeightTB.Text); }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) != "")
            {
                DialogResult = DialogResult.None;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox1.Enabled = !textBox1.Enabled;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int res;
            if (checkBox1.Checked)
            {
                if (!int.TryParse(textBox1.Text, out res))
                {
                    errorProvider1.SetError(textBox1, "Error");
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
        }

        private void CheckStringWithInt(TextBox t)
        {
            int res;

            if (!int.TryParse(t.Text, out res))
            {
                errorProvider1.SetError(t, "Error");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void worldWidthTB_Leave(object sender, EventArgs e)
        {
            int res;
            if (checkBox1.Checked)
            {
                if (!int.TryParse(textBox1.Text, out res))
                {
                    errorProvider1.SetError(textBox1, "Error");
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
        }
    }
}
