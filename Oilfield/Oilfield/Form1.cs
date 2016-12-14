﻿using System;
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
        World world;
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            Bounds = Screen.PrimaryScreen.Bounds;

            UIConfig.WindowHeight = Height;
            UIConfig.WindowWidth = Width;

            //world = new World(150, 150); //---------------------------------------------------------
        }

        long lastTime = 0;

        new public void Update()
        {
            Util.sw.Stop();
            //int dt = (int)((DateTime.Now.Ticks - lastTime) / 10000);
            int dt = (int)Util.sw.ElapsedTicks;
            lastTime = DateTime.Now.Ticks;

            if (world != null)
            {
                if (world.Ready)
                {
                    UIConfig.Move(MousePosition, Width, Height, dt);

                    world.Update(dt);
                    toolStripMenuItem1.Text = ((int)world.Money).ToString();

                    Refresh();
                }
            }

            Util.sw.Restart();
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
            if (world != null)
            world.Draw(e.Graphics);
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

                world = new World(UIConfig.WorldWidth, UIConfig.WorldHeight);
                Util.sw.Start();
            }

            
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            world.randomPath();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
