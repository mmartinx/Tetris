using System;
using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Example2
{
    public class MonoTetrisGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Tetris.Game _tetrisGame;

        public MonoTetrisGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            int width = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_WIDTH"]);
            int height = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_HEIGHT"]);
            _tetrisGame = Tetris.Game.NewGame(width, height, _spriteBatch, _graphics.GraphicsDevice);
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            Dispose();
            _tetrisGame.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _tetrisGame.Update(gameTime, Keyboard.GetState());
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _tetrisGame.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}