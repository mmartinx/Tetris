using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tetris
{
    public class Game
    {
        public Board GameBoard { get; set; }
        public Piece DroppingPiece { get; set; }
        public Random RNG { get; set; }
        public Dictionary<string, Point[]> PieceReference { get; set; }
        public int Score { get; set; }

        public Game(int width, int height)
        {
            GameBoard = new Board(width, height);
            RNG = new Random();
            PieceReference = Piece.GetPieceReference();
            Score = 0;
            SpawnPiece();
        }

        public static Game NewGame(int width, int height)
        {
            var ng = new Game(width, height);
            return ng;
        }

        public void Start()
        {
            Tick();
            do
            {
                while (!Console.KeyAvailable)
                {
                    if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                    {
                        DroppingPiece.TryMoveLeft(GameBoard);
                    }
                    if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                    {
                        DroppingPiece.TryMoveRight(GameBoard);
                    }
                    if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                    {
                        DroppingPiece.TryRotate(GameBoard, PieceReference, Direction.RIGHT);
                    }
                    Tick();
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        public void Tick()
        {
            Thread.Sleep(10);

            if (DroppingPiece.TryDrop(GameBoard))
            {
                // collided, spawn a new one
                SpawnPiece();
            }

            Score += GameBoard.ClearLines();
            Draw();
        }

        public void SpawnPiece()
        {
            DroppingPiece = new Piece(RNG, PieceReference);
        }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"Score: {Score}");
            Console.Write("\t   ");
            for (int i = 0; i < GameBoard.Width; ++i)
            {
                Console.Write(i);
            }
            Console.Write("\r\n\t  +");
            for (int i = 0; i < GameBoard.Width; ++i)
            {
                Console.Write('-');
            }
            Console.Write("+\r\n");

            for (var y = 0; y < GameBoard.Height; ++y)
            {
                Console.Write($"\t{y.ToString().PadLeft(2, ' ')}|");

                for (var x = 0; x < GameBoard.Width; ++x)
                {
                    if (DroppingPiece.Cells.Any(c => (c.X + DroppingPiece.PosX) == x && (c.Y + DroppingPiece.PosY) == y))
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(GameBoard[x, y] ? '#' : ' ');
                    }
                }
                Console.Write("|\r\n");
            }

            Console.Write("\t  +");
            for (int i = 0; i < GameBoard.Width; ++i)
            {
                Console.Write('-');
            }
            Console.Write('+');
        }
    }
}
