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
    class Default
    {
        public static bool IsState(MouseState ms, List<GameObject> gameobjects)
        {
            return true;
        }

        public static void DefaultClick(MouseState ms, List<GameObject> gameobjects)
        {
            Console.WriteLine("DClick");
        }

        public static void DefaultDrag(MouseState ms, List<GameObject> gameobjects)
        {
            Console.WriteLine("DDrag");
        }

        public static void DefaultRelease(MouseState ms, List<GameObject> gameobjects)
        {
            Console.WriteLine("DRelease");
        }
    }
}