﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

            world = new World(150, 150); //---------------------------------------------------------
        }

        long lastTime = 0;



        new public void Update()
        {
            int dt = (int)((DateTime.Now.Ticks - lastTime) / 10000);
            lastTime = DateTime.Now.Ticks;

            UIConfig.Move(MousePosition, Width, Height, dt);

            Refresh();
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
            world.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
