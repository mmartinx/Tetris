using System;
using System.Configuration;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_WIDTH"]);
            int height = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_HEIGHT"]);
            var game = Game.NewGame(width, height);
            game.Start();
        }
    }
}
