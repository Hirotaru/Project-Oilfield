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
        Trainer trainer;

        private bool admin = false;
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

        

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitForm ef = new ExitForm();
           
            ef.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - ef.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - ef.Height / 2);

            if (ef.ShowDialog() == DialogResult.OK)
            {
                Close();
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (trainer != null)
                trainer.World.Draw(e.Graphics);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Update();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void NewWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorldCreationForm wcf = new WorldCreationForm();

            if (wcf.ShowDialog() == DialogResult.OK)
            {
                UIConfig.WorldWidth = wcf.WorldWidth;
                UIConfig.WorldHeight = wcf.WorldHeight;



               

                if (wcf.Training)
                {
                    trainer = new Trainer(UIConfig.WorldWidth, UIConfig.WorldHeight, false, wcf.Iterations);
                }
                else
                {
                    trainer = new Trainer(UIConfig.WorldWidth, UIConfig.WorldHeight, false, 1);
                }

                drawingONToolStripMenuItem.Text = "Drawing: ON";
                debugToolStripMenuItem.Enabled = true;

            }

            
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainer.Reset();
        }

        private void DrawingONToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm hf = new HelpForm();
            hf.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string s = AppDomain.CurrentDomain.BaseDirectory;

            s = s.Remove(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));
            s = s.Remove(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));
            s = s.Remove(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));


            AppDomain.CurrentDomain.SetData("DataDirectory", s);

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UD.mdf;Integrated Security=True;Connect Timeout=30;");

            string s1 = "select count(*) from Login where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'";
            SqlDataAdapter ad = new SqlDataAdapter(s1, con);

            SqlDataAdapter ad1 = new SqlDataAdapter(s1 + " and (admin is not null and admin=1)", con);

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            ad.Fill(dt);
            ad1.Fill(dt1);

            if (dt.Rows[0][0].ToString() == "1")
            {
                
                loginPanel.Visible = false;
                newWorldToolStripMenuItem.Enabled = true;
                drawingONToolStripMenuItem.Enabled = true;
                loginPanel.Enabled = false;

                if (dt1.Rows[0][0].ToString() == "1")
                {
                    admin = true;
                    newUserToolStripMenuItem1.Visible = true;
                }
            }
            else
            {
                LoginErrorProvider.SetError(textBox1, "Unable to log in. Please check that you have entered your login and password correctly.");
            }

            con.Close();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            panel1.Location = new Point(loginPanel.Width / 2 - panel1.Width, loginPanel.Height / 2 - panel1.Height);
        }

        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainer.World.debug = !trainer.World.debug;
            
        }

        private void NewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserAddForm uaf = new UserAddForm();

            if (uaf.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (trainer == null) return;
            if (trainer.World == null) return;

            trainer.World.UpdateExts();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (trainer == null) return;
            if (trainer.World == null) return;

            trainer.World.CreateNewTexts();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (trainer == null) return;
            if (trainer.World == null) return;

            trainer.World.CreateNewTexts();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (trainer == null) return;
            if (trainer.World == null) return;

            trainer.World.UpdateExts();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trainer.pause = !trainer.pause;
        }
    }
}
