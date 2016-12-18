using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Oilfield
{
    public partial class MainForm : Form
    {
        Trainer trainer;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            WindowState = FormWindowState.Maximized;
            //FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.PrimaryScreen.Bounds;

            UIConfig.WindowHeight = Height;
            UIConfig.WindowWidth = Width;

            //world = new World(150, 150); //---------------------------------------------------------
        }

        long lastTime = 0;

        new public void Update()
        {
            double dt = (int)((DateTime.Now.Ticks - lastTime) / 10000);
            lastTime = DateTime.Now.Ticks;

            if (trainer != null)
            {
                if (trainer.World.Ready)
                {
                    UIConfig.Move(MousePosition, Width, Height, dt);

                    trainer.Update(dt);
                    toolStripMenuItem1.Text = "Money: " + ((int)trainer.World.Money).ToString() + " TotalMoney: " + ((int)trainer.World.TotalMoney).ToString();
                    toolStripMenuItem2.Text = "Income: " + ((int)trainer.World.Income).ToString();

                    toolStripMenuItem3.Text = "MoneyState: " + trainer.World.GetState().Money.ToString() +
                        " IncomeState: " + trainer.World.GetState().Income.ToString() +
                        " ExtState: " + trainer.World.GetState().ExtCount.ToString();

                    //if (trainer.World.Drawing)
                    Refresh();
                }
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }

                case Keys.Space:
                    {
                        UIConfig.dx = 0;
                        UIConfig.dy = 24;
                        break;
                    }
            }
        }

        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (trainer != null)
                trainer.World.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void newWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorldCreationForm wcf = new WorldCreationForm();

            if (wcf.ShowDialog() == DialogResult.OK)
            {
                UIConfig.WorldWidth = wcf.WorldWidth;
                UIConfig.WorldHeight = wcf.WorldHeight;

                trainer = new Trainer(UIConfig.WorldWidth, UIConfig.WorldHeight);

                drawingONToolStripMenuItem.Text = "Drawing: ON";
            }

            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainer.Reset();
        }

        private void drawingONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (trainer == null) return;
            if (trainer.World == null) return;
                
            bool d = trainer.World.Drawing;

            trainer.World.Drawing = !d;

            if (d)
            {
                drawingONToolStripMenuItem.Text = "Drawing: OFF";
            }
            else
            {
                drawingONToolStripMenuItem.Text = "Drawing: ON";
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
