using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Primitives;
using Mmc.MonoGame.UI.Primitives.Brushes;
using Mmc.MonoGame.UI.UIElements;

namespace Mmc.MonoGame.UI.GameTests
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private UIManager uiManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            uiManager = new UIManager(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var root = uiManager.Root;

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
            root.AddChild(stackPanel);

            stackPanel.AddChild(new ContainerElement()
            {
                Size = new Vector2(100, 50),
                Border = new Thickness(2),
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                BackgroundBrush = new TextureBrush()
                {
                    Texture = Content.Load<Texture2D>("Button"),
                    TextureMode = TextureMode.NineSlice,
                    EdgeSize = 2,
                    Color = Color.White
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Green
                },
            });

            stackPanel.AddChild(new ContainerElement()
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
            root.AddChild(outerPanel);

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            uiManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            uiManager.Draw();

            base.Draw(gameTime);
        }
    }
}
