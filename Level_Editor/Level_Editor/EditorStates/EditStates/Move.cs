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
    class Move
    {
        static TRectangularObject initial;
        static MouseState msi;
        static TRectangularObject current;
        public static bool IsState(MouseState ms, List<GameObject> gameobjects)
        {
            foreach (GameObject go in gameobjects)
            {
                if (go is TRectangularObject)
                {
                    var r = go as TRectangularObject;
                    if (r.Intersects(ms.X,ms.Y))
                        return true;   
                }
            }
            return false;
        }

        public static void Click(MouseState ms, List<GameObject> gameobjects)
        {
            foreach (GameObject go in gameobjects)
            {
                if (go is TRectangularObject)
                {
                    var r = go as TRectangularObject;
                    if (r.Intersects(ms.X, ms.Y))
                    {
                        initial = new TRectangularObject(r.X,r.Y,r.Width,r.Height);
                        current = r;
                        msi = ms;
                    }
                }
            }

        }

        public static void Release(MouseState ms, List<GameObject> gameobjects)
        {
            Console.WriteLine("MoveRelease");

        }

        public static void Drag(MouseState ms, List<GameObject> gameobjects)
        {
            current.X = initial.X + ms.X - msi.X;
            current.Y = initial.Y + ms.Y - msi.Y;

        }
    }
}
