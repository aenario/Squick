using System;

namespace Squick
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Squick game = new Squick())
            {
                game.Run();
            }
        }
    }
#endif
}

