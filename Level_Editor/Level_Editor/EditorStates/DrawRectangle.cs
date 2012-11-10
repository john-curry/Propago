using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace WindowsGame1
{
    class DrawRectangle
    {


        static int xStart;
        static int yStart;
        static int xEnd;
        static int yEnd;
        
        
    

        public static void Click(int x, int y)
        {
            var ms = Mouse.GetState();
            Console.WriteLine("Click");
            xStart = x;
            yStart = y;
            

        }

        public static void Release(int x, int y)
        {
            var ms = Mouse.GetState();

            Console.WriteLine("Release");
            yEnd = y;
            xEnd = x;
            
            

            int width = Math.Abs(xStart - xEnd);
            int height = Math.Abs(yStart - yEnd);

         
            if (xEnd >= xStart && yEnd >= yStart)
                LevelEditor.AddToEditor(GameObjectFactory.MakeObject(xStart, yStart, width, height));
            else if (xStart >= xEnd && yStart >= yEnd)
                LevelEditor.AddToEditor(GameObjectFactory.MakeObject(xEnd, yEnd, width, height));
            else if (xEnd >= xStart && yEnd <= yStart)
                LevelEditor.AddToEditor(GameObjectFactory.MakeObject(xStart, yEnd, width, height));
            else if (xEnd <= xStart && yEnd >= yStart)
                LevelEditor.AddToEditor(GameObjectFactory.MakeObject(xEnd, yStart, width, height));
        }


        public static void Drag(MouseState ms)
        {
            yEnd = ms.Y;
            xEnd = ms.X;

            int width = Math.Abs(xStart - xEnd);
            int height = Math.Abs(yStart - yEnd);

     
        }
    }
}
