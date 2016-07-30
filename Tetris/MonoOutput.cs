using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class MonoOutput : IOutput
    {
        private readonly int _width;
        private readonly int _height;
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private const int SQUARE_SIZE = 20;
        public MonoOutput(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _width = width;
            _height = height;
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
        }
        public void Draw(Board board, Piece droppingPiece, int score)
        {
            for (var y = 0; y < board.Height; ++y)
            {
                for (var x = 0; x < board.Width; ++x)
                {
                    char type = droppingPiece.OccupiesPosition(x, y) ? 'X' : (board[x, y] ? '#' : ' ');
                    DrawShape(x, y, type);
                }
            }
        }

        private void DrawShape(int x, int y, char type)
        {
            Texture2D rect = new Texture2D(_graphicsDevice, SQUARE_SIZE, SQUARE_SIZE);
            Color[] data = new Color[80 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);

            Vector2 coor = new Vector2(x * SQUARE_SIZE, y * SQUARE_SIZE);
            _spriteBatch.Draw(rect, coor, type == ' ' ? Color.Black : Color.White);
        }
    }
}
