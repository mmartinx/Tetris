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
            bool clearRow;
            int score = 0;
            for (var y = Height - 1; y > 0; --y)
            {
                clearRow = true;
                for (var x = 0; x < Width; ++x)
                {
                    if (!Tiles[x, y])
                    {
                        clearRow = false;
                        break;
                    }
                }

                if (clearRow)
                {
                    ShiftRowsDown(y);
                    score++;
                }
            }
            return score;
        }

        private void ShiftRowsDown(int destRow)
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
