using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace WindowsGame1
{
    class PicturePanel : Panel
    {
        public Image Picture
        { get; set; }

        public PicturePanel() : base()
        {
            this.DoubleClick += new EventHandler(panel_DoubleClick);
        }

        void panel_DoubleClick(object sender, EventArgs e)
        {
            var p = sender as Panel;
            var gfx = p.CreateGraphics();
            var ms = new MemoryStream();
            LevelEditor.Textures[LevelEditor.Texturing].SaveAsPng(ms, p.Width, p.Height);
            gfx.DrawImage(Image.FromStream(ms), 0, 0);
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }


}
