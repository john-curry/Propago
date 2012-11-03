using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LevelObjects;

namespace WindowsGame1
{
    class Level
    {
        List<GameObject> levelObjects;
        Rectangle bounds;
        XmlTextWriter textWriter;
        string path;
        Texture2D background;
        public string Path { get; set; }
        string name;
        public string Name 
        { 
            get 
            { 
                return name; 
            }
        }

        public Level(string Name, List<GameObject> levelObjects, Rectangle bounds)
        {
            this.levelObjects = levelObjects;
            this.bounds = bounds;
            this.name = Name;
        }

        
        
        public void LevelStream(Stream st)
        {
            List<LevelObjects.RectangularObject> ro = new List<RectangularObject>();

            foreach (GameObject g in levelObjects)
            {
                if (g is TRectangularObject)
                {
                    var r = g as TRectangularObject;
                    ro.Add(new RectangularObject(
                        r.X,
                        r.Y,
                        r.Width,
                        r.Height,
                        r.Name,
                        r.Type,
                        r.ColorName,
                        r.TextureName));
                }
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(st,settings))
            {
                IntermediateSerializer.Serialize(writer, ro, null);
            }
        }
    }
}
