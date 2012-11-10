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
    public partial class Attributes : Form
    {
        GameObject go;
        public Attributes(GameObject go)
        {
            InitializeComponent();
            this.go = go;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ColorCombo.DataSource = ColourHelper.GetColorNames();
            tbox.DataSource = LevelEditor.Textures.Keys.ToList();
            tbox.SelectedItem = ((List<string>)tbox.DataSource).Where((p) => ((p == (go as TRectangularObject).TextureName)));
            checkBox1.CheckStateChanged += new EventHandler(checkBox1_CheckStateChanged);
            checkBox2.CheckedChanged += new EventHandler(checkBox2_CheckedChanged);
        }

        void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            tbox.Enabled = !tbox.Enabled;
        }

        void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            ColorCombo.Enabled = !ColorCombo.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = go as TRectangularObject;
            try
            {
                if (this.width.Text != "")
                {
                    t.Width = Int32.Parse(this.width.Text.ToString());        
                }

                if (this.height.Text != "")
                    t.Height = Int32.Parse(this.height.Text.ToString());

                if (name.Text != "")
                    t.Name = name.Text;

                if (ColorCombo.Text != "" && ColorCombo.Enabled)
                    t.ColorName = ColorCombo.Text;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("format not supported");
            }


        }


    
    }
}
