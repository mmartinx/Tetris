using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public class Board
    {
        public bool[,] Tiles { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool this[int x, int y]
        {
            get { return Tiles[x, y]; }
            set { Tiles[x, y] = value; }
        }

        public Board(int width, int height)
        {
            Tiles = new bool[width, height];
            Width = width;
            Height = height;
        }

        public int ClearLines()
        {
            var linesToClear = GetLinesToClear().OrderByDescending(x => x).ToList();
            if (linesToClear.Count == 0)
                return 0;

            ShiftRowsDown(linesToClear);
            return CalculateScore(linesToClear);
        }

        private IEnumerable<int> GetLinesToClear()
        {
            for (var y = Height - 1; y > 0; --y)
            {
                bool clearRow = true;
                for (var x = 0; x < Width; ++x)
                {
                    if (!Tiles[x, y])
                    {
                        clearRow = false;
                        break;
                    }
                }

                if (clearRow)
                    yield return y;
            }
        }

        private int Score(int consecutive)
        {
            switch (consecutive)
            {
                case 1:
                    return 40;
                case 2:
                    return 100;
                case 3:
                    return 300;
                case 4:
                    return 1200;
                default:
                    return 0;
            }
        }

        private int CalculateScore(List<int> linesToClear)
        {
            if (linesToClear.Count == 0 || linesToClear.Count == 1)
                return Score(linesToClear.Count);

            return linesToClear.GroupConsecutive().Sum(x => Score(x.Count()));
        }

        private void ShiftRowsDown(List<int> linesToClear)
        {
            foreach (var destRow in linesToClear)
            {
                for (var y = destRow; y > 0; --y)
                {
                    for (var x = 0; x < Width; ++x)
                    {
                        Tiles[x, y] = Tiles[x, y - 1];
                    }
                }
            }
        }
    }
}
