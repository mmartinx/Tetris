using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class TextureReference
    {
        private readonly Dictionary<char, Texture2D> _store;

        public TextureReference(GraphicsDevice graphicsDevice, int squareSize)
        {
            _store = new Dictionary<char, Texture2D>
            {
                {'I', CreateRect(squareSize, squareSize, Color.Cyan, graphicsDevice) },
                {'J', CreateRect(squareSize, squareSize, Color.Yellow, graphicsDevice) },
                {'L', CreateRect(squareSize, squareSize, Color.Purple, graphicsDevice) },
                {'O', CreateRect(squareSize, squareSize, Color.GreenYellow, graphicsDevice) },
                {'S', CreateRect(squareSize, squareSize, Color.Red, graphicsDevice) },
                {'T', CreateRect(squareSize, squareSize, Color.Blue, graphicsDevice) },
                {'Z', CreateRect(squareSize, squareSize, Color.SandyBrown, graphicsDevice) },
                {' ', CreateRect(squareSize, squareSize, Color.Black, graphicsDevice) }
            };
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

        public Texture2D this[char type] => _store[type];

        public void Dispose()
        {
            foreach (var rect in _store)
            {
                rect.Value.Dispose();
            }
        }
    }
}
