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

            // root should be a plain background panel that anything can be placed into
            Root = new Panel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10,
                Border = new Thickness(5),
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Black
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Red,
                },
            };
            Root.AddChild(stackPanel);

            stackPanel.AddChild(new Panel()
            {
                Size = new Vector2(100, 50),
                Border = new Thickness(2),
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.White
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Green
                },
            });

            stackPanel.AddChild(new Panel()
            {
                Size = new Vector2(100, 50),
                Border = new Thickness(2),
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.White
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Blue
                },
            });

            /*var outerPanel = new Panel()
            {
                Border = new Thickness(20),
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Gray
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Red,
                },
            };
            Root.AddChild(outerPanel);

            outerPanel.AddChild(new Panel()
            {
                Size = new Vector2(10, 10),
                Border = new Thickness(2),
                Padding = new Thickness(0),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Black
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Green
                },
            });*/

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
