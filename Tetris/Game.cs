using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class Game
    {
        public Board GameBoard { get; set; }
        public Piece DroppingPiece { get; set; }
        public int Score { get; set; }
        public Random RNG { get; set; }
        public PieceReference PieceRef { get; set; }
        private float timer = 1.0f;
        private const float DELAY = 1.0f;

        public IOutput Output { get; set; }
        public IOutput Console { get; set; }

        private Game(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            GameBoard = new Board(width, height);
            RNG = new Random();
            PieceRef = new PieceReference();
            Output = new Tetris.MonoOutput(width, height, spriteBatch, graphicsDevice);
            Console = new Tetris.ConsoleOutput(width, height);
            Score = 0;
            SpawnPiece();
        }

        public static Game NewGame(int width, int height, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            return new Game(width, height, spriteBatch, graphicsDevice);
        }

        private void HandleInput(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left))
                DroppingPiece.TryMoveLeft(GameBoard);

            if (state.IsKeyDown(Keys.Right))
                DroppingPiece.TryMoveRight(GameBoard);

            if (state.IsKeyDown(Keys.Up))
                DroppingPiece.TryRotate(GameBoard, PieceRef, Direction.RIGHT);

            if (state.IsKeyDown(Keys.Down))
                DroppingPiece.TryDrop(GameBoard);
        }

        private void Tick()
        {
            if (DroppingPiece.TryDrop(GameBoard))
            {
                SpawnPiece();
            }
            Score += GameBoard.ClearLines();
        }

        public void Update(GameTime gameTime, KeyboardState state)
        {
            HandleInput(state);
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer < 0)
            {
                Tick();
                timer = DELAY;
            }
        }

        public void Draw(GameTime gameTime)
        {
            Output.Draw(GameBoard, DroppingPiece, Score);
            Console.Draw(GameBoard, DroppingPiece, Score);
        }

        private void SpawnPiece()
        {
            DroppingPiece = new Piece(RNG, GameBoard, PieceRef);
        }
    }
}
