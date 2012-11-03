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
using WindowsGame1;
namespace WindowsGame1
{
    interface NCorner
    {
        Rectangle Bounds { get; }
        int X { get; }
        int Y { get; }
        int Width { get; }
        int Height { get; }
        bool Contains(int x, int y);
    }

    class BottomRight : NCorner
    {
        Rectangle bounds;
        public Rectangle Bounds { get { return bounds; } }
        public int X { get { return Bounds.X; } }
        public int Y { get { return Bounds.Y; } }
        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public BottomRight(TRectangularObject rec, int w, int h)
        {
            bounds.X = rec.X + rec.Width - w / 2;
            bounds.Y = rec.Y + rec.Height - h / 2;
            bounds.Width = w;
            bounds.Height = h;
        }

        public bool Contains(int x, int y)
        { return bounds.Contains(x,y); }

    }

    class TopRight : NCorner
    {
        Rectangle bounds;
        public Rectangle Bounds { get { return bounds; } }
        public int X { get { return Bounds.X; } }
        public int Y { get { return Bounds.Y; } }
        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public TopRight(TRectangularObject rec, int w, int h)
        {
            bounds.X = rec.X + rec.Width - w / 2;
            bounds.Y = rec.Y - h / 2;
            bounds.Width = w;
            bounds.Height = h;
        }

        public bool Contains(int x, int y)
        { return bounds.Contains(x, y); }
    }

    class BottomLeft : NCorner
    {
        Rectangle bounds;
        public Rectangle Bounds { get { return bounds; } }
        public int X { get { return Bounds.X; } }
        public int Y { get { return Bounds.Y; } }
        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public BottomLeft(TRectangularObject rec, int w, int h)
        {
            bounds.X = rec.X - w / 2;
            bounds.Y = rec.Y + rec.Height - h / 2;
            bounds.Width = w;
            bounds.Height = h;
        }

        public bool Contains(int x, int y)
        { return bounds.Contains(x,y); }

    }

    class TopLeft : NCorner
    {
        Rectangle bounds;
        public Rectangle Bounds { get { return bounds; } }
        public int X { get { return Bounds.X; } }
        public int Y { get { return Bounds.Y; } }
        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public TopLeft(TRectangularObject rec, int w, int h)
        {
            bounds.X = rec.X  - w / 2;
            bounds.Y = rec.Y  - h / 2;
            bounds.Width = w;
            bounds.Height = h;
        }

        public bool Contains(int x, int y)
        { return bounds.Contains(x,y); }

    }

    class Corners
    {
        TopRight topRight;
        public TopRight TopRight { get { return topRight; } }
        TopLeft topLeft;
        public TopLeft TopLeft { get { return topLeft; } }
        BottomLeft bottomLeft;
        public BottomLeft BottomLeft { get { return bottomLeft; } }
        BottomRight bottomRight;
        public BottomRight BottomRight { get { return bottomRight; } }

        List<NCorner> corners = new List<NCorner>();
        public List<NCorner> ToList()
        { return corners; }

        TRectangularObject attachedTo;
        public TRectangularObject AttachedTo { get { return attachedTo; } }

        public Corners(TRectangularObject trec, int w, int h)
        {
            attachedTo = trec;
            corners.Add(topLeft = new TopLeft(trec, w, h));
            corners.Add(topRight = new TopRight(trec, w, h));
            corners.Add(bottomLeft = new BottomLeft(trec, w, h));
            corners.Add(bottomRight = new BottomRight(trec, w, h));

        }

        public void Foreach(Action<NCorner> doc)
        {
            corners.ForEach(doc);
        }

    }
}
