using System;
using System.Threading;

namespace Tetris
{
    public class Game
    {
        public Board GameBoard { get; set; }
        public Piece DroppingPiece { get; set; }
        public int Score { get; set; }

        public Random RNG { get; set; }
        public PieceReference PieceRef { get; set; }
        
        public IOutput Term { get; set; }

        private Game(int width, int height)
        {
            GameBoard = new Board(width, height);
            RNG = new Random();
            PieceRef = new PieceReference();
            Score = 0;
            SpawnPiece();
            Term = new Output(width, height);
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
                        DroppingPiece.TryRotate(GameBoard, PieceRef, Direction.RIGHT);
                    }
                    Tick();
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private void Tick()
        {
            Thread.Sleep(10);

            if (DroppingPiece.TryDrop(GameBoard))
            {
                // collided, spawn a new one
                SpawnPiece();
            }

            Score += GameBoard.ClearLines();
            Term.Draw(GameBoard, DroppingPiece, Score);
        }

        private void SpawnPiece()
        {
            DroppingPiece = new Piece(RNG, GameBoard, PieceRef);
        }
    }
}
