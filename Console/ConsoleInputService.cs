using Core.Plugins;
using Microsoft.Xna.Framework.Input;

namespace Console
{
    public class ConsoleInputService : IInputService
    {
        public KeyboardState KeyboardState => new KeyboardState();

        public MouseState MouseState => new MouseState();
    }
}
