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
    public partial class TypeForm : Form
    {
        public TypeForm()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (typebox.Text != "")
            {
                LevelEditor.ObjectTypes.Add(typebox.Text);
                LevelEditor.Editor.TypeBox.DataSource = LevelEditor.ObjectTypes.ToList();
            }
        }
        private void Done_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
