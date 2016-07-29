using System;

namespace Tetris
{
    public class ConsoleOutput : IOutput
    {
        private readonly int _width;
        private readonly int _height;
        public ConsoleOutput(int width, int height)
        {
            _width = width;
            _height = height;
        }
        private void DrawLine()
        {
            Console.Write("\t  +");
            for (int i = 0; i < _width; ++i)
            {
                Console.Write('-');
            }
            Console.Write("+\r\n");
        }

        private void DrawHeader(int score)
        {
            Console.WriteLine();
            Console.WriteLine($"Score: {score}");
            Console.Write("\t   ");
            for (int i = 0; i < _width; ++i)
            {
                Console.Write(i);
            }
            Console.Write("\r\n");
        }

        private void DrawBoard(Board board, Piece droppingPiece)
        {
            for (var y = 0; y < board.Height; ++y)
            {
                Console.Write($"\t{y.ToString().PadLeft(2, ' ')}|");

                for (var x = 0; x < board.Width; ++x)
                {
                    Console.Write(droppingPiece.OccupiesPosition(x, y) ? 'X' : (board[x, y] ? '#' : ' '));
                }
                Console.Write("|\r\n");
            }
        }

        public void Draw(Board board, Piece droppingPiece, int score)
        {
            Console.Clear();
            
            DrawHeader(score);
            DrawLine();
            DrawBoard(board, droppingPiece);
            DrawLine();
        }
    }
}
