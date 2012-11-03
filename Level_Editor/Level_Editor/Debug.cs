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
using Microsoft.Xna.Framework.Media;
using System.Management.Automation;

namespace WindowsGame1
{
    class DebugC
    {
        static MyConsole debugConsole = new MyConsole();

        public static MyConsole DebugConsole
        {
            get
            {
                return debugConsole;
            }
        }


        public static void WriteLine(string st)
        {
          
        }

        public static void AddVariable(string name, Object o)
        {
           
            debugConsole.AddVariable(name, o);
        }
    }
}
