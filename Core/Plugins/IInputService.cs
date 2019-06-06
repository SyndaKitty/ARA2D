using Microsoft.Xna.Framework.Input;

namespace Core.Plugins
{
    public interface IInputService
    {
        KeyboardState KeyboardState { get; }
        MouseState MouseState { get; }
    }
}
