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

        private void WorldCreationForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = 150.ToString();
            textBox2.Text = 150.ToString();


        }

        public int WorldWidth
        {
            get { return Convert.ToInt32(textBox1.Text); }
        }

        public int WorldHeight
        {
            get { return Convert.ToInt32(textBox2.Text); }
        }
    }
}
