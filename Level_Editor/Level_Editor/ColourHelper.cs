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
using System.Reflection;
namespace WindowsGame1
{
    class ColourHelper
    {
        public static Color GetColorByString(string st)
        {
            if (colors.ContainsKey(st)) return colors[st];
            else throw new Exception("HEllo");
        }

        public static List<string> GetColorNames()
        {
            return colors.Keys.ToList<string>();
        }

        private static Dictionary<string, Color> colors =
            DictionaryOf();



        private static Dictionary<string, Color> DictionaryOf()
        {
            List<Color> predefinedColors = new List<Color>();
            Dictionary<string, Color> colorByString = new Dictionary<string, Color>();
                // Get all of the public static properties
                PropertyInfo[] properties = typeof(Color).GetProperties(BindingFlags.Public|BindingFlags.Static);

                foreach(PropertyInfo propertyInfo in properties)
                {
                    // Check to make sure the property has a get method, and returns type "Color"
                    if (propertyInfo.GetGetMethod() != null && propertyInfo.PropertyType == typeof(Color))
                    { 
                        // Get the color returned by the property by invoking it
                        Color color = (Color)propertyInfo.GetValue(null, null);
                        string name = (string)propertyInfo.Name;
                        colorByString.Add(name, color);

                        predefinedColors.Add(color);
                    }
                }
            return colorByString;

        }
    }
}
