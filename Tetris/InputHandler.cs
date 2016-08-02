using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class InputHandler
    {
        private KeyboardState _prevState;
        private KeyboardState _currState;

        public bool KeyUp(Keys key) => _prevState.IsKeyDown(key) && _currState.IsKeyUp(key);

        public void Update()
        {
            _prevState = _currState;
            _currState = Keyboard.GetState();
        }
    }
}
