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

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
