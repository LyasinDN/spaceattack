using System;

namespace spaceattack
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpaceAttackGame game = new SpaceAttackGame())
            {
                game.Run();
            }
        }
    }
}