using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class Game
    {
        private readonly Board _board;
        private readonly Random _rng;
        private readonly PieceReference _pieceReference;
        private Piece _piece;
        private int _score;
        private float _timer = 1.0f;
        private const float DELAY = 1.0f;

        private readonly IOutput _output;
        private readonly IOutput _console;

        private Game(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _board = new Board(width, height);
            _rng = new Random();
            _pieceReference = new PieceReference();
            _output = new MonoOutput(width, height, spriteBatch, graphicsDevice);
            _console = new ConsoleOutput(width, height);
            SpawnPiece();
        }

        public static Game NewGame(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            return new Game(width, height, spriteBatch, graphicsDevice);
        }

        private void HandleInput(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left))
                _piece.TryMoveLeft(_board);

            if (state.IsKeyDown(Keys.Right))
                _piece.TryMoveRight(_board);

            if (state.IsKeyDown(Keys.Up))
                _piece.TryRotate(_board, _pieceReference, Direction.RIGHT);

            if (state.IsKeyDown(Keys.Down))
                _piece.TryDrop(_board);
        }

        private void Tick()
        {
            if (_piece.TryDrop(_board))
            {
                SpawnPiece();
            }
            _score += _board.ClearLines();
        }

        public void Update(GameTime gameTime, KeyboardState state)
        {
            HandleInput(state);
            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer < 0)
            {
                Tick();
                _timer = DELAY;
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
    }
}
