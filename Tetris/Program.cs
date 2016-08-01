using System;
using Example2;

namespace Tetris
{
    class Program
    {
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
