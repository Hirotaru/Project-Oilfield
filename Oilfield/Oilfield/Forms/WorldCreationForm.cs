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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public bool Training
        {
            get { return checkBox1.Checked; }
        }

        public int Iterations
        {
            get { return Convert.ToInt32(IterationsTextBox.Text); }
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

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (IterationsErrorProvider.GetError(IterationsTextBox) != "")
            {
                DialogResult = DialogResult.None;
            }

            if (WidthErrorProvider.GetError(worldWidthTB) != "")
            {
                DialogResult = DialogResult.None;
            }

            if (HeightErrorProvider.GetError(worldHeightTB) != "")
            {
                DialogResult = DialogResult.None;
            }


        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            IterationsTextBox.Enabled = !IterationsTextBox.Enabled;
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            CheckStringWithInt(IterationsTextBox, IterationsErrorProvider);
        }

        private void CheckStringWithInt(TextBox t, ErrorProvider err)
        {
            if (!int.TryParse(t.Text, out int res))
            {
                err.SetError(t, "Error: Integer Required");
            }
            else
            {
                err.Clear();
            }
        }

        private void WorldWidthTB_Leave(object sender, EventArgs e)
        {
             CheckStringWithInt(worldWidthTB, WidthErrorProvider);
        }

        private void WorldHeightTB_Leave(object sender, EventArgs e)
        {
            CheckStringWithInt(worldHeightTB, HeightErrorProvider);
        }
    }
}
