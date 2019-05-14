using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core;
using Core.Plugins;
using MonoGame.ContentLoading;
using DefaultEcs.System;
using MonoGame.Rendering;

namespace MonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ARA2D : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Engine engine;
        TimeService time;
        SpriteLoader spriteLoader;

        public ARA2D()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spriteLoader = new SpriteLoader(Content);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            time = new TimeService();

            ISystem<RenderContext> rendering = new SequentialSystem<RenderContext>
            (
                new SpriteLoader(Content),
                new RenderBegin(spriteBatch),
                new BasicSpriteRender(spriteBatch),
                new RenderEnd(spriteBatch)
            );

            EnginePlugins plugins = new EnginePlugins(rendering, time);
            engine = new Engine(plugins);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            engine.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            engine.Render();
        }
    }
}
