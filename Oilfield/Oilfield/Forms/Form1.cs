using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Oilfield
{
    public partial class MainForm : Form
    {
        User user = new User();
        Trainer trainer;
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
            double dt = (int)((DateTime.Now.Ticks - lastTime) / 10000);
            lastTime = DateTime.Now.Ticks;


            if (trainer != null)
            {
                if (trainer.World.Ready)
                {
                    UIConfig.Move(MousePosition, Width, Height, dt);

                    trainer.Update(dt);

                    toolStripStatusLabel1.Text = "Money: " + ((int)trainer.World.Money).ToString();
                    toolStripStatusLabel2.Text = "TotalMoney: " + ((int)trainer.World.TotalMoney).ToString();
                    toolStripStatusLabel3.Text = "Income: " + ((int)trainer.World.Income).ToString();

                    toolStripStatusLabel4.Text = "MoneyState: " + Util.StateString[trainer.World.GetState().Money];
                    toolStripStatusLabel5.Text = "IncomeState: " + Util.StateString[trainer.World.GetState().Income];
                    toolStripStatusLabel6.Text = "ExtState: " + Util.StateString[trainer.World.GetState().ExtCount];

                    toolStripStatusLabel7.Text = "Iteration: " + trainer.curIteration;

                    toolStripStatusLabel8.Text = "TotalResourceAmount: " + (int)trainer.World.totalResourceAmount +
                        " totalRecourceCost: " + (int)trainer.World.totalResourceCost +
                        " avgRecourceCost: " + (int)trainer.World.avgResourceCost;

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



                trainer = new Trainer(UIConfig.WorldWidth, UIConfig.WorldHeight, false, wcf.Iterations);

                if (wcf.Training)
                {
                    
                }

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

        private void button1_Click(object sender, EventArgs e)
        {
            string s = AppDomain.CurrentDomain.BaseDirectory;

            s = s.Remove(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));
            s = s.Remove(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));
            s = s.Remove(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));


            AppDomain.CurrentDomain.SetData("DataDirectory", s);

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UD.mdf;Integrated Security=True;Connect Timeout=30;");

            string s1 = "select count(*) from Login where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'";
            SqlDataAdapter ad = new SqlDataAdapter(s1, con);

            DataTable dt = new DataTable();

            ad.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                loginPanel.Visible = false;
                newWorldToolStripMenuItem.Enabled = true;
                drawingONToolStripMenuItem.Enabled = true;
                loginPanel.Enabled = false;
            }

            con.Close();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            panel1.Location = new Point(loginPanel.Width / 2 - panel1.Width, loginPanel.Height / 2 - panel1.Height);
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainer.World.debug = !trainer.World.debug;
        }
    }
}
