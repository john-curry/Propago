using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{

    public interface GameObject {}

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

    class TRectangularObject : IRectangular, IIdentifiable , ITextured , IColored , GameObject
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

        
        public TRectangularObject(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = "untitled";
            this.Type = "Rectangle";
        }

        public TRectangularObject(int x, int y, int w, int h, string Name, string Type)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = Name;
            this.Type = Type;
        }

        public TRectangularObject(int x, int y, int w, int h, string Name, string Type, string ColorName, string TextureName)
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

    class TPlatform : IRectangular , IIdentifiable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Intersects(int x, int y)
        { return x > this.X && y > this.Y && y < this.Y + this.Height && x < this.Width + this.X; }
        public TPlatform(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = "untitled";
            this.Type = "Platform";
        }

        public TPlatform(int x, int y, int w, int h, string Name)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Name = Name;
            this.Type = "Platform";
        }
    }

    class TLadder : IRectangular
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Intersects(int x, int y)
        { return x > this.X && y > this.Y && y < this.Y + this.Height && x < this.Width + this.X; }
        public TLadder(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
        }
    }
}
