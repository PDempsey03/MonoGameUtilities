using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.Collisions.GameTests.Scenes;
using Mmc.MonoGame.Input;

namespace Mmc.MonoGame.Collisions.GameTests
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            Globals.Game1 = this;
            Globals.Content = Content;

            Drawer.Initialize(_graphics.GraphicsDevice);

            SceneManager.ChangeScene<ContainsPointTestScene>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Drawer.LoadContent(Content);

            // set global spritebatch
            Globals.SpriteBatch = _spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            // set global gametime
            Globals.GameTime = gameTime;

            // update keyboard + mouse state
            InputHelper.UpdateState();

            SceneManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            SceneManager.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
