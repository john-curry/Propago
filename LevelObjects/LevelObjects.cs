using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LevelObjects
{
    public interface LevelObject { }

    interface IRectangular
    {
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        bool Intersects(int x, int y);

    }


    interface IIdentifiable
    {
        string Name { get; set; }
        string Type { get; set; }
    }

    interface ITextured
    {
        string TextureName { get; set; }
    }

    interface IColored
    {
        string ColorName { get; set; }
    }

    public class RectangularObject : IRectangular, IIdentifiable, ITextured, IColored,  LevelObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ColorName { get; set; }
        public string TextureName { get; set; }
        
        public bool Intersects(int x, int y)
        { return x > this.X && y > this.Y && y < this.Y + this.Height && x < this.Width + this.X; }

        public Rectangle ToRectangle()
        { return new Rectangle(X, Y, Width, Height); }


        public RectangularObject() { }
         
        public RectangularObject(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = "untitled";
            this.Type = "Rectangle";
        }

        public RectangularObject(int x, int y, int w, int h, string Name, string Type)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = Name;
            this.Type = Type;
        }

        public RectangularObject(int x, int y, int w, int h, string Name, string Type, string ColorName, string TextureName)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = Name;
            this.Type = Type;
            this.ColorName = ColorName;
            this.TextureName = TextureName;
        }
    }
}
