using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Primitives;
using Mmc.MonoGame.UI.Systems.Input;
using System.Diagnostics;

namespace Mmc.MonoGame.UI
{
    public class UIManager
    {
        private readonly Game _game;

        public ContainerElement Root { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        // sprite batch settings
        public SpriteSortMode SpriteSortMode { get; set; } = SpriteSortMode.Deferred;
        public BlendState BlendState { get; set; } = BlendState.AlphaBlend;
        public SamplerState SamplerState { get; set; } = SamplerState.PointWrap;
        public DepthStencilState DepthStencilState { get; set; } = DepthStencilState.Default;
        public RasterizerState RasterizerState { get; set; } = RasterizerState.CullNone;

        // input
        private InputService InputService { get; init; }
        private InteractionService InteractionService { get; init; }

        public UIManager(Game game)
        {
            _game = game;

            // root should be a plain background panel that anything can be placed into
            Root = new ContainerElement()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            game.Window.ClientSizeChanged += (s, e) => UpdateRootSize();

            game.Window.ClientSizeChanged += (s, e) =>
            {
                Debug.WriteLine("Window Bounds: " + game.GraphicsDevice.Viewport.Bounds);
            };

            InputService = new InputService();
            InteractionService = new InteractionService(InputService, Root);

            UpdateRootSize();

            Drawer.Initialize(game.GraphicsDevice);
        }

        public UIElement? FindUIElementByName(string name)
        {
            return Root.FindUIElementByName(name);
        }

        public T? FindUIElementByBame<T>(string name) where T : UIElement
        {
            return Root.FindUIElementByName(name) as T;
        }

        public void UpdateRootSize()
        {
            Viewport viewPort = _game.GraphicsDevice.Viewport;
            Root.Size = new Vector2(viewPort.Width, viewPort.Height);
            Root.MarkDirty();
        }

        public void Update(GameTime gameTime)
        {
            InputService.UpdateState();
            InteractionService.Update();
            InputService.UpdateEvents();

            if (Root.IsLayoutDirty)
            {
                Viewport viewPort = _game.GraphicsDevice.Viewport;

                // force root to always be screen size
                Root.Measure(new Vector2(viewPort.Width, viewPort.Height));
                Root.Arrange(new Rectangle(0, 0, viewPort.Width, viewPort.Height));
            }

            Root.Update(gameTime);
        }

        public void Draw()
        {
            SpriteBatch.Begin(
                SpriteSortMode,
                BlendState,
                SamplerState,
                DepthStencilState,
                RasterizerState
            );

            Root.Draw(SpriteBatch);

            SpriteBatch.End();
        }
    }
}
