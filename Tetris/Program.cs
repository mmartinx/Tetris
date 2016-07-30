using System;
using System.Configuration;
using Example2;

namespace Tetris
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    int width = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_WIDTH"]);
        //    int height = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_HEIGHT"]);
        //    var game = Game.NewGame(width, height);
        //    game.Start();

        //    var stuff = new Game1();
        //    stuff.Run();
        //}
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new MonoTetrisGame())
            {
                game.Run();
            }
        }
    }
}
