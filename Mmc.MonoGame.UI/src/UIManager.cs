using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Primitives;
using Mmc.MonoGame.UI.Primitives.Brushes;
using Mmc.MonoGame.UI.UIElements;
using System.Diagnostics;

namespace Mmc.MonoGame.UI
{
    public class UIManager
    {
        private readonly Game _game;

        public UIElement Root { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        // sprite batch settings
        public SpriteSortMode SpriteSortMode { get; set; } = SpriteSortMode.Deferred;
        public BlendState BlendState { get; set; } = BlendState.AlphaBlend;
        public SamplerState SamplerState { get; set; } = SamplerState.PointClamp;
        public DepthStencilState DepthStencilState { get; set; } = DepthStencilState.Default;
        public RasterizerState RasterizerState { get; set; } = RasterizerState.CullNone;

        public UIManager(Game game)
        {
            _game = game;

            Root = new Panel()
            {
                Border = new Thickness(60),
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Gray
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Red,
                },
            };

            Root.AddChild(new Panel()
            {
                Size = new Vector2(10, 10),
                Border = new Thickness(20),
                Padding = new Thickness(0),
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Black
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Green
                },
            });

            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            game.Window.ClientSizeChanged += (s, e) => UpdateRootSize();

            game.Window.ClientSizeChanged += (s, e) =>
            {
                Debug.WriteLine("Window Bounds: " + game.GraphicsDevice.Viewport.Bounds);
            };

            UpdateRootSize();

            Drawer.Initialize(game.GraphicsDevice);
        }

        public void UpdateRootSize()
        {
            Viewport viewPort = _game.GraphicsDevice.Viewport;
            Root.Size = new Vector2(viewPort.Width, viewPort.Height);
            Root.MarkDirty();
        }

        public void Update(GameTime gameTime)
        {
            Root?.Update(gameTime);
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
