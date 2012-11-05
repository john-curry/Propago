using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class PicBoxEvents
    {
        public static void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            var p = ((PictureBox)sender);
            var ms = new MemoryStream();
            LevelEditor
                .Textures[LevelEditor.Texturing]
                .SaveAsPng(ms, p.Width, p.Height);

            p.Image = Image.FromStream(ms);
        }

        public static void pictureBox_Click(object sender, EventArgs e)
        {
            var t = LevelEditor.Textures[LevelEditor.Texturing];
            LevelEditor.AddToEditor(GameObjectFactory.MakeObject(0, 0, t.Width, t.Height));




        }

        public static void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Moose down");
            var p = (PictureBox)sender;
            if (p.Image != null)
            {
                p.DoDragDrop(GameObjectFactory.MakeObject(1,1,p.Image.Width,p.Image.Height), DragDropEffects.Copy);
            }
        }

        public static void gameForm_DoubleClick(object sender, EventArgs e)
        {
            
        }

        public static void gameForm_DragEnter(object sender, DragEventArgs e)
        {
           if (e.Data.GetDataPresent(typeof(GameObject)))
               e.Effect = DragDropEffects.Copy;
           else
               e.Effect = DragDropEffects.None;
        }

        public static void gameForm_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("drop");
            var g = (GameObject)e.Data.GetData(typeof(GameObject));
            var t = g as TRectangularObject;
            var ms = Mouse.GetState();
            t.X = ms.X;
            t.Y = ms.Y;
            LevelEditor.AddToEditor(t);
        }
    }
}
