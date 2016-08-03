using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class MonoOutput : IOutput
    {
        private const int SquareSize = 15;

        private readonly SpriteBatch _spriteBatch;
        private readonly TextureReference _textureRef;

        public MonoOutput(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _textureRef = new TextureReference(graphicsDevice, SquareSize);
        }

        public void Draw(Board board, Piece droppingPiece, int score)
        {
            for (var y = 0; y < board.Height; ++y)
            {
                for (var x = 0; x < board.Width; ++x)
                {
                    char type = GetCellType(board, droppingPiece, x, y);
                    DrawRect(x, y, type);
                }
            }
        }

        private char GetCellType(Board board, Piece droppingPiece, int x, int y) 
            => droppingPiece.OccupiesPosition(x, y) ? droppingPiece.Type : board[x, y]; 

        private void DrawRect(int x, int y, char type)
        {
            var position = new Vector2(x * SquareSize, y * SquareSize);
            _spriteBatch.Draw(_textureRef[type], position);
        }

        public void Dispose()
        {
            _textureRef.Dispose();
        }

        ~MonoOutput()
        {
            Dispose();
        }
    }
}
