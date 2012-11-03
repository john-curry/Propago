using System;

namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            using (NotAGame game = new NotAGame())
            {
                game.Run();
            }
        }
    }
#endif
}

