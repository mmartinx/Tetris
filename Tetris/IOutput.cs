namespace Tetris
{
    public interface IOutput
    {
        void Draw(Board board, Piece droppingPiece, int score);
    }
}