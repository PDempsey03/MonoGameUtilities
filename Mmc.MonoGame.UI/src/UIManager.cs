using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Primitives;
using Mmc.MonoGame.UI.Rendering;
using Mmc.MonoGame.UI.Systems.Input;
using System.Diagnostics;

namespace Mmc.MonoGame.UI
{
    public class UIManager
    {
        private readonly Game _game;

        public ContainerElement Root { get; private set; }

        public RenderContext RenderContext { get; private set; }

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

            RenderContext = new RenderContext(
                new SpriteBatch(game.GraphicsDevice),
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                DepthStencilState.Default,
                new RasterizerState()
                {
                    CullMode = CullMode.None,
                    ScissorTestEnable = true // allow for clipping
                });

            game.Window.ClientSizeChanged += (s, e) => UpdateRootSize();

            game.Window.ClientSizeChanged += (s, e) =>
            {
                Debug.WriteLine("Window Bounds: " + game.GraphicsDevice.Viewport.Bounds);
            };

            InputService = new InputService();
            InteractionService = new InteractionService(InputService, Root);

            UpdateRootSize();
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
            RenderContext.Begin();

            Root.Draw(RenderContext);

            RenderContext.End();
        }
    }
}
