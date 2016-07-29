using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public enum Direction
    {
        LEFT,
        RIGHT
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Piece
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Point[] Cells { get; set; }
        public char Type { get; set; }
        public int State { get; set; }

        public Piece(Random rng, Board board, Dictionary<string, Point[]> pieceReference)
        {
            PosX = board.Width / 2; PosY = 0;
            Type = pieceNames[rng.Next(7)];
            State = 1;
            Cells = pieceReference[Type + State.ToString()];
        }

        private static readonly string pieceNames = "IJLOSTZ";

        public static Dictionary<string, Point[]> GetPieceReference()
        {
            return new Dictionary<string, Point[]>
            {
                { "I1", new[] { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) } },
                { "I2", new[] { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) } },
                { "I3", new[] { new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2) } },
                { "I4", new[] { new Point(2, 0), new Point(2, 1), new Point(2, 2), new Point(2, 3) } },

                { "J1", new[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(2, 2) } },
                { "J2", new[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(0, 2) } },
                { "J3", new[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) } },
                { "J4", new[] { new Point(0, 1), new Point(0, 2), new Point(1, 1), new Point(1, 2) } },

                { "L1", new[] { new Point(0, 1), new Point(0, 2), new Point(1, 1), new Point(2, 1) } },
                { "L2", new[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(1, 2) } },
                { "L3", new[] { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) } },
                { "L4", new[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(2, 0) } },

                { "O1", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },
                { "O2", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },
                { "O3", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },
                { "O4", new[] { new Point(1, 1), new Point(2, 1), new Point(1, 2), new Point(2, 2) } },

                { "S1", new[] { new Point(1, 1), new Point(2, 1), new Point(0, 2), new Point(1, 2) } },
                { "S2", new[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(2, 2) } },
                { "S3", new[] { new Point(1, 1), new Point(2, 1), new Point(0, 2), new Point(1, 2) } },
                { "S4", new[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(2, 2) } },

                { "T1", new[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(1, 2) } },
                { "T2", new[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(1, 2) } },
                { "T3", new[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) } },
                { "T4", new[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) } },

                { "Z1", new[] { new Point(0, 1), new Point(1, 1), new Point(1, 2), new Point(2, 2) } },
                { "Z2", new[] { new Point(2, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) } },
                { "Z3", new[] { new Point(0, 1), new Point(1, 1), new Point(1, 2), new Point(2, 2) } },
                { "Z4", new[] { new Point(2, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) } }
            };
        }

        public bool OccupiesPosition(int x, int y) => Cells.Any(c => (c.X + PosX) == x && (c.Y + PosY) == y);

        public bool TryDrop(Board board)
        {
            if (Cells.Any(c => (c.Y + PosY + 1 >= board.Height) || board[c.X + PosX, c.Y + PosY + 1]))
            {
                foreach (var cell in Cells)
                {
                    board[cell.X + PosX, cell.Y + PosY] = true;
                }
                return true;
            }

            PosY++;
            return false;
        }

        public bool TryMoveLeft(Board board)
        {
            if (Cells.Any(c => (c.X + PosX - 1 < 0) || board[c.X + PosX - 1, c.Y + PosY]))
                return true;

            PosX--;
            return false;
        }

        public bool TryMoveRight(Board board)
        {
            if (Cells.Any(c => (c.X + PosX + 1 >= board.Width) || board[c.X + PosX + 1, c.Y + PosY]))
                return true;

            PosX++;
            return false;
        }

        public bool TryRotate(Board board, Dictionary<string, Point[]> pieceReference, Direction direction)
        {
            int nextState = direction == Direction.LEFT
                ? (State - 1 < 1 ? 4 : State - 1)
                : (State + 1 > 4 ? 1 : State + 1);

            Point[] nextPoints = pieceReference[Type + nextState.ToString()];

            foreach (var p in nextPoints)
            {
                var x = p.X + PosX;
                var y = p.Y + PosY;

                if (x <= 0 || x >= board.Width || y <= 0 || y >= board.Height)
                    return true;
            }

            State = nextState;
            Cells = nextPoints;
            return false;
        }
    }
}