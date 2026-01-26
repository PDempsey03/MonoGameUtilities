using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mmc.MonoGame.UI.Models.Brushes;
using Mmc.MonoGame.UI.Models.Primitives;
using Mmc.MonoGame.UI.Models.Text;
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

            FontFamily fontFamily = new FontFamily(Content.Load<SpriteFont>("TestFont_Regular"))
            {
                Bold = Content.Load<SpriteFont>("TestFont_Bold"),
                BoldItalic = Content.Load<SpriteFont>("TestFont_BoldItalic"),
                Italic = Content.Load<SpriteFont>("TestFont_Italic"),
            };

            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
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

            stackPanel.MouseEntered += (s, e) =>
            {
                if (stackPanel.BorderBrush is SolidBrush solidBrush)
                {
                    solidBrush.Color = Color.Green;
                }
            };

            stackPanel.MouseLeft += (s, e) =>
            {
                if (stackPanel.BorderBrush is SolidBrush solidBrush)
                {
                    solidBrush.Color = Color.Red;
                }
            };

            root.AddChild(stackPanel);

            var buttonLabel = new Label()
            {
                Text = "Clicked 0 times",
                FontFamily = fontFamily,
                Wrap = true,
                TextColor = Color.White,
                Border = new Thickness(0),
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextHorizontalAlignment = TextHorizontalAlignment.Center,
                TextVerticalAlignment = TextVerticalAlignment.Center,
            };

            int buttonClickedCount = 0;
            void UpdateButtonLabelCount()
            {
                buttonClickedCount++;
                buttonLabel.Text = $"Clicked {buttonClickedCount} times";
            }

            Thickness baseButtonBorderThickness = new Thickness(2);
            var button = new Button()
            {
                Size = new Vector2(300, 100),
                Border = baseButtonBorderThickness,
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
                    Color = Color.Red
                },
                Content = buttonLabel
            };

            button.Clicked += (s, e) => UpdateButtonLabelCount();

            button.MouseEntered += (s, e) =>
            {
                if (button.BorderBrush is SolidBrush solidBrush)
                {
                    solidBrush.Color = Color.Green;
                }
            };

            button.MouseLeft += (s, e) =>
            {
                if (button.BorderBrush is SolidBrush solidBrush)
                {
                    solidBrush.Color = Color.Red;
                }
                button.Border = baseButtonBorderThickness;
            };

            button.MouseButtonPressed += (s, e) =>
            {
                switch (e.MouseButton)
                {
                    case MouseButton.Left:
                        button.Border += new Thickness(2);
                        break;
                    case MouseButton.Middle:
                        break;
                    case MouseButton.Right:
                        break;
                }
            };

            button.MouseButtonReleased += (s, e) =>
            {
                switch (e.MouseButton)
                {
                    case MouseButton.Left:
                        button.Border = baseButtonBorderThickness;
                        break;
                    case MouseButton.Middle:
                        break;
                    case MouseButton.Right:
                        break;
                }
            };

            stackPanel.AddChild(button);

            CheckButton checkButton = new CheckButton()
            {
                Size = new Vector2(50),
                Border = new Thickness(2),
                Padding = new Thickness(2),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                ToggledBrush = new CheckBrush()
                {
                    Color = Color.White,
                    Thickness = 3
                },
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Black,
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Blue
                }
            };

            stackPanel.AddChild(checkButton);

            stackPanel.AddChild(new UIImage()
            {
                Texture = Content.Load<Texture2D>("WarrenWide"),
                Size = new Vector2(200, 200),
                StretchMode = ImageStretchMode.Uniform,
                ClipToBounds = true,
                Margin = new Thickness(0),
                Border = new Thickness(10),
                Padding = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Black
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Blue
                },
            });

            stackPanel.AddChild(new Label()
            {
                Text = "[c=blue][b]bold[/b][/c] [c=#ff00ea][i]italic[/i][/c] [c=97,230,225][b][i]bolditalic[/b][/i][/c] [u]underlined[/u] " +
                "and we built this city on rock and roll! + rock on donkey kong we need to make this as long as mutua [b]ps si[/b]blesss " +
                "aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxxyyyyzzzz " +
                "this is a story about kevin\nhi\noops\nhello kevin\n\n2 n's\n\n\n3n's",
                FontFamily = fontFamily,
                Wrap = true,
                ClipToBounds = true,
                Size = new Vector2(200, 200),
                TextColor = Color.Green,
                Border = new Thickness(2),
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                TextHorizontalAlignment = TextHorizontalAlignment.Left,
                TextVerticalAlignment = TextVerticalAlignment.Top,
                BackgroundBrush = new SolidBrush()
                {
                    Color = Color.Brown
                },
                BorderBrush = new BorderBrush()
                {
                    Color = Color.Blue
                },
            });
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
