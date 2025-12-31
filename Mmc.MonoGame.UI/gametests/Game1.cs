using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Primitives;
using Mmc.MonoGame.UI.Primitives.Brushes;
using Mmc.MonoGame.UI.Primitives.Text;
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
            root.AddChild(stackPanel);

            stackPanel.AddChild(new ContainerElement()
            {
                Size = new Vector2(300, 100),
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

            FontFamily fontFamily = new FontFamily(Content.Load<SpriteFont>("TestFont_Regular"))
            {
                Bold = Content.Load<SpriteFont>("TestFont_Bold"),
                BoldItalic = Content.Load<SpriteFont>("TestFont_BoldItalic"),
                Italic = Content.Load<SpriteFont>("TestFont_Italic"),
            };

            stackPanel.AddChild(new Label()
            {
                Text = "[c=blue][b]bold[/b][/c] [c=#ff00ea][i]italic[/i][/c] [c=97,230,225][b][i]bolditalic[/b][/i][/c] [u]underlined[/u] " +
                "and we built this city on rock and roll! + rock on donkey kong we need to make this as long as mutua [b]ps si[/b]blesss " +
                "aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxxyyyyzzzz " +
                "this is a story about kevin",
                FontFamily = fontFamily,
                Wrap = true,
                Size = new Vector2(200, 350),
                TextColor = Color.Green,
                Border = new Thickness(2),
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                TextHorizontalAlignment = TextHorizontalAlignment.Center,
                TextVerticalAlignment = TextVerticalAlignment.Center,
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
