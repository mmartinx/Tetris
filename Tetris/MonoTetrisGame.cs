using System;
using System.Configuration;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Example2
{
    public class MonoTetrisGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;
        Tetris.Game tetrisGame;

        public MonoTetrisGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            position = new Vector2(graphics.GraphicsDevice.Viewport.
                       Width / 2,
                                    graphics.GraphicsDevice.Viewport.
                                    Height / 2);

            int width = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_WIDTH"]);
            int height = Convert.ToInt16(ConfigurationManager.AppSettings["BOARD_HEIGHT"]);
            tetrisGame = Tetris.Game.NewGame(width, height, spriteBatch, GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tetrisGame.Update(gameTime, Keyboard.GetState());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            tetrisGame.Draw(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}