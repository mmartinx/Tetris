using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class MonoOutput : IOutput
    {
        private const int SquareSize = 15;

        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _rect;

        public MonoOutput(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _rect = CreateRect(SquareSize, SquareSize, Color.White, graphicsDevice);
        }

        public void Draw(Board board, Piece droppingPiece, int score)
        {
            for (var y = 0; y < board.Height; ++y)
            {
                for (var x = 0; x < board.Width; ++x)
                {
                    char type = droppingPiece.OccupiesPosition(x, y) ? 'X' : (board[x, y] ? '#' : ' ');
                    DrawRect(x, y, type);
                }
            }
        }

        private Texture2D CreateRect(int width, int height, Color color, GraphicsDevice graphicsDevice)
        {
            var rect = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            rect.SetData(data);
            return rect;
        }

        private void DrawRect(int x, int y, char type)
        {
            var position = new Vector2(x * SquareSize, y * SquareSize);
            _spriteBatch.Draw(_rect, position, type == ' ' ? Color.Black : Color.White);
        }

        public void Dispose()
        {
            _rect.Dispose();
        }

        ~MonoOutput()
        {
            Dispose();
        }
    }
}
