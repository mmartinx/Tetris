using System;

namespace Tetris
{
    public interface IOutput : IDisposable
    {
        void Draw(Board board, Piece droppingPiece, int score);
    }
}