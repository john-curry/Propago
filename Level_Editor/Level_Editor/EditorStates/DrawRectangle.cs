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
    class DrawRectangle : EditorState
    {


        int xStart;
        int yStart;
        int xEnd;
        int yEnd;
        
        
        public DrawRectangle()
        {
        }



        public void Click(MouseState ms)
        {
            Console.WriteLine("Click");
            xStart = ms.X;
            yStart = ms.Y;
            

        }

        public void Release(MouseState ms)
        {
            Console.WriteLine("Release");
            yEnd = ms.Y;
            xEnd = ms.X;
            
            

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


        public void Drag(MouseState ms)
        {
            yEnd = ms.Y;
            xEnd = ms.X;

            int width = Math.Abs(xStart - xEnd);
            int height = Math.Abs(yStart - yEnd);

            if (xEnd >= xStart && yEnd >= yStart) ;
            // LevelEditor.tempObject = new EditorBlock(xStart, yStart, width, height);
            else if (xStart >= xEnd && yStart >= yEnd) ;
            //LevelEditor.tempObject = new EditorBlock(xEnd, yEnd, width, height);
            else if (xEnd >= xStart && yEnd <= yStart) ;
            //LevelEditor.tempObject = new EditorBlock(xStart, yEnd, width, height);
            else if (xEnd <= xStart && yEnd >= yStart) ;
                //LevelEditor.tempObject = new EditorBlock(xEnd, yStart, width, height);
        }

        public void Draw(SpriteBatch sp) { }
        public void Save() { }

    }
}
