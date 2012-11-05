using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace WindowsGame1
{
    class GameObjectFactory
    {

        public static GameObject MakeObject(int X, int Y, int W, int H)
        {

            try
            {
                if (W <= 5 || H <= 5 || X < 0 || Y < 0)
                    throw new Exception("Bad Click event. You should fix this stupid");
                if (X == 0 && Y == 0)
                    throw new Exception("This is not how you fix this bug");

                GameObject go;

                go = new TRectangularObject(
                    X,
                    Y,
                    W,
                    H,
                    "untitled",
                    LevelEditor.Type,
                    LevelEditor.Coloring,
                    LevelEditor.Texturing);


                return go;
            }


            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + "\n" + e.ToString() + "\n" + e.Message);
                return null;
            }
        }

        public static GameObject MakeObject(int X, int Y, int W, int H, Texture2D t)
        {

            try
            {
                if (W <= 5 || H <= 5 || X < 0 || Y < 0)
                    throw new Exception("Bad Click event. You should fix this stupid");


                GameObject go;

                go = new TRectangularObject(
                    X,
                    Y,
                    W,
                    H,
                    "untitled",
                    LevelEditor.Type,
                    LevelEditor.Coloring,
                    LevelEditor.Texturing);


                return go;
            }


            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + "\n" + e.ToString() + "\n" + e.Message);
                return null;
            }
        }
    }
    
}
