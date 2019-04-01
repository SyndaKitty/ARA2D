using Nez;

namespace ARA2D
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Core
    {
        public World world;

        public Game() : base(isFullScreen: false, enableEntitySystems: true, windowTitle: "ARA2D Prototype")
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            scene = new TestScene(this);
        }
    }
}
