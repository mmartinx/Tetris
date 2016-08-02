using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class Game : IDisposable
    {
        private const double Delay = 1.0;
        private readonly Board _board;
        private readonly Random _rng;
        private readonly PieceReference _pieceReference;
        private readonly InputHandler _input;
        private Piece _piece;
        private int _score;
        private double _timer = Delay;
 
        private readonly IOutput _output;
        private readonly IOutput _console;

        private Game(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _board = new Board(width, height);
            _rng = new Random();
            _pieceReference = new PieceReference();
            _output = new MonoOutput(spriteBatch, graphicsDevice);
            _console = new ConsoleOutput(width, height);
            _input = new InputHandler();
            SpawnPiece();
        }

        public static Game NewGame(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            return new Game(width, height, spriteBatch, graphicsDevice);
        }

        private void Tick()
        {
            if (_piece.TryDrop(_board))
            {
                SpawnPiece();
            }
            _score += _board.ClearLines();
        }

        public void Update(GameTime gameTime)
        {
            _input.Update();

            if (_input.KeyUp(Keys.Left))
                _piece.TryMoveLeft(_board);

            if (_input.KeyUp(Keys.Right))
                _piece.TryMoveRight(_board);

            if (_input.KeyUp(Keys.Up))
                _piece.TryRotate(_board, _pieceReference, Direction.RIGHT);

            if (_input.KeyUp(Keys.Down))
                _piece.TryDrop(_board);

            _timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer < 0)
            {
                Tick();
                _timer = Delay;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _output.Draw(_board, _piece, _score);
            _console.Draw(_board, _piece, _score);
        }

        private void SpawnPiece()
        {
            _piece = new Piece(_rng, _board, _pieceReference);
        }

        public void Dispose()
        {
            _output.Dispose();
        }
    }
}
