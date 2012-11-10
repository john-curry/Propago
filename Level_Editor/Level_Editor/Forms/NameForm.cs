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
    public partial class NameForm : Form
    {
        GameObject go;
        public NameForm(GameObject go)
        {
            InitializeComponent();
            this.go = go;
        }

        private void NameForm_Load(object sender, EventArgs e)
        {
            button1.Click += new EventHandler(button1_Click);
        }

        void button1_Click(object sender, EventArgs e)
        {
            (go as TRectangularObject).Name = textBox1.Text;
            this.Close();
        }

    }
}
