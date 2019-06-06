using Core.Plugins;
using Microsoft.Xna.Framework.Input;

namespace MonoGame
{
    public class InputService : IInputService
    {
        public KeyboardState KeyboardState => Keyboard.GetState();
        public MouseState MouseState => Mouse.GetState();
    }
}
