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
using John.LevelEditor;
namespace WindowsGame1
{
    class Default
    {
        
        public static EditState GetState(MouseState ms, List<GameObject> gameobjects)
        {
            foreach (GameObject g in gameobjects)
            {
                if (g is TRectangularObject)
                {
                    var trec = g as TRectangularObject;

                    foreach (Rectangle r in Edit.CalcCorners(trec))
                    {
                        if (r.X < ms.X && r.Y < ms.Y && r.X + r.Width > ms.X && r.Y + r.Height > ms.Y)
                            return EditState.Resize;
                    }

                    if (trec.Intersects(ms.X, ms.Y))
                    {
                        return EditState.Move;
                    }
                }
            }

        }

    }

    class Resize
    {
        public delegate void State(MouseState ms, List<GameObject> gameobjects);
        delegate bool isState(MouseState ms, List<GameObject> gameobjects);

        
        public bool isResize(MouseState ms, List<GameObject> gameobjects)
        {
            foreach (GameObject g in gameobjects)
            {
                if (g is TRectangularObject)
                {
                    var trec = g as TRectangularObject;

                    foreach (Rectangle r in Edit.CalcCorners(trec))
                    {
                        if (r.X < ms.X && r.Y < ms.Y && r.X + r.Width > ms.X && r.Y + r.Height > ms.Y)
                            return true;
                    }
                }
            }
            return false;
        }

        public void ResizeClick(MouseState ms, List<GameObject> gameobjects)
        {
            
        }




        public void ResizeRelease(MouseState ms, List<GameObject> gameobjects);

        public static void ResizeDrag(MouseState ms, List<GameObject> gameobjects);
    }
}
