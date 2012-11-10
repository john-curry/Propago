using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsGame1
{
    public partial class BackgroundForm : Form
    {
        public BackgroundForm()
        {
            InitializeComponent();
        }

        private void BackgroundForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = LevelEditor.Textures.Keys.ToList();
            this.comboBox1.SelectedIndex = 1;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            LevelEditor.Background = LevelEditor.GetTextureByName(comboBox1.Text);
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
