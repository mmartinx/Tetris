using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class TextureReference
    {
        private readonly int _squareSize;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Dictionary<char, Texture2D> _store;

        public TextureReference(GraphicsDevice graphicsDevice, int squareSize)
        {
            _squareSize = squareSize;
            _graphicsDevice = graphicsDevice;

            _store = new Dictionary<char, Texture2D>
            {
                {'I', CreateRect(Color.Cyan) },
                {'J', CreateRect(Color.Yellow) },
                {'L', CreateRect(Color.Purple) },
                {'O', CreateRect(Color.GreenYellow) },
                {'S', CreateRect(Color.Red) },
                {'T', CreateRect(Color.Blue) },
                {'Z', CreateRect(Color.SandyBrown) },
                {' ', CreateRect(Color.Black) }
            };
        }

        private Texture2D CreateRect(Color color)
        {
            Texture2D rect = new Texture2D(_graphicsDevice, _squareSize, _squareSize);
            Color[] data = new Color[_squareSize * _squareSize];
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
