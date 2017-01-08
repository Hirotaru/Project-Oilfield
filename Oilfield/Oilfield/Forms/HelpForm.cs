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
    public partial class HelpForm : Form
    {
        private List<string> MenuItems = new List<string>()
        {

            "Вход в систему",
            "Новый пользователь",
            "Создание карты",
            "Параметры мира",           
        };

        string HelpPath = @"..\..\Help\";
        string rtf = ".rtf";

        private Dictionary<string, string> HelpFiles = new Dictionary<string, string>();
        public HelpForm()
        {
            InitializeComponent();

            for (int i = 0; i < MenuItems.Count; i++)
            {
                HelpFiles.Add(MenuItems[i], HelpPath + MenuItems[i] + rtf);
                comboBox1.Items.Add(MenuItems[i]);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            richTextBox1.LoadFile(HelpFiles[comboBox1.SelectedItem.ToString()]);
            richTextBox1.ReadOnly = true;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.LoadFile(HelpFiles[comboBox1.SelectedItem.ToString()]);
        }
    }
}
