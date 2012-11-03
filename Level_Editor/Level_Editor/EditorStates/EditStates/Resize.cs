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
    class Resize
    {
        static TRectangularObject initial;
        static TRectangularObject currentobject;
        static NCorner currentcorner;
        
        public static bool isResize(MouseState ms, List<GameObject> gameobjects)
        {
            foreach (GameObject go in gameobjects)
            {
                if (go is TRectangularObject)
                {
                    Corners corners = new Corners(go as TRectangularObject, 10, 10);
                    foreach (NCorner nc in corners.ToList())
                    {
                        if (nc.Contains(ms.X, ms.Y))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }



        public static void ResizeClick(MouseState ms, List<GameObject> gameobjects)
        {
            foreach (GameObject go in gameobjects)
            {
                if (go is TRectangularObject)
                {
                    var corners = new Corners(go as TRectangularObject, 10, 10);
                    foreach (NCorner c in corners.ToList())
                    {
                        if (c.Contains(ms.X, ms.Y))
                        {
                            currentobject = go as TRectangularObject;
                            initial = new TRectangularObject(
                                (go as TRectangularObject).X,
                                (go as TRectangularObject).Y,
                                (go as TRectangularObject).Width,
                                (go as TRectangularObject).Height);
                            currentcorner = c;
                        }
                    }
                }
            }

        }
        //       TopLeft = 0, TopRight = 1, BottomLeft = 2, BottomRight = 3, None
        public static Rectangle[] CalcCorners(GameObject gameobject)
        {
            Rectangle[] recs = new Rectangle[4];
            var trec = gameobject as TRectangularObject;
            recs[0] = new Rectangle(trec.X - 5, trec.Y - 5, 10, 10);
            recs[1] = new Rectangle(trec.Width + trec.X - 5, trec.Y - 5, 10, 10);
            recs[2] = new Rectangle(trec.X - 5, trec.Y + trec.Height - 5, 10, 10);
            recs[3] = new Rectangle(trec.Width + trec.X - 5, trec.Y + trec.Height - 5, 10, 10);
            return recs;
        }

        public static Corner GetCorner(Rectangle[] rec, MouseState ms)
        {
            for (int i = 0; i < rec.Length; i++)
            {
                if (rec[i].X < ms.X && rec[i].Y < ms.Y && rec[i].Width + rec[i].X > ms.X && rec[i].Y + rec[i].Height > ms.Y)
                {
                    if (i == 0) return Corner.TopLeft;
                    else if (i == 1) return Corner.TopRight;
                    else if (i == 2) return Corner.BottomLeft;
                    else if (i == 3) return Corner.BottomRight;
                }
            }
            return Corner.None;
        }

        public static void ResizeRelease(MouseState ms, List<GameObject> gameobjects)
        {

        }

        public static void ResizeDrag(MouseState ms, List<GameObject> gameobjects)
        {
            if (currentobject is TRectangularObject)
            {
                var trec = currentobject as TRectangularObject;
                if (currentcorner is TopLeft)
                {
                    trec.X = ms.X;
                    trec.Y = ms.Y;
                    trec.Width = initial.Width + initial.X - ms.X;
                    trec.Height = initial.Height + initial.Y - ms.Y;
                }
                if (currentcorner is TopRight)
                {
                    trec.X = initial.X;
                    trec.Y = ms.Y;
                    trec.Width = ms.X - (initial.X + initial.Width) + initial.Width;
                    trec.Height = initial.Height + initial.Y - ms.Y;
                }
                if (currentcorner is BottomLeft)
                {
                    trec.X = ms.X;
                    trec.Width = initial.Width + initial.X - ms.X;
                    trec.Height = ms.Y - initial.Y;
                }
                if (currentcorner is BottomRight)
                {
                    trec.Height = ms.Y - initial.Y;
                    trec.Width = ms.X - initial.X;
                }
            }

        }
    }
}
