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

        private int worldWidth;

        public int WorldWidth
        {
            get { return worldWidth; }
            private set { worldWidth = value; }
        }

        private int worldHeight;

        public int WorldHeight
        {
            get { return worldHeight; }
            private set { worldHeight = value; }
        }


        public WorldCreationForm()
        {
            InitializeComponent();
        }

        private void worldWidthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse((sender as TextBox).Text, out worldWidth))
            {
                (sender as TextBox).Text = "ERROR";
            }
        }

        private void worldHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse((sender as TextBox).Text, out worldHeight))
            {
                (sender as TextBox).Text = "ERROR";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void WorldCreationForm_Load(object sender, EventArgs e)
        {
            worldHeightTextBox.Text = 150.ToString();
            worldWidthTextBox.Text = 150.ToString();
        }
    }
}
