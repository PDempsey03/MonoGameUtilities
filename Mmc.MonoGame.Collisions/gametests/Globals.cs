using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mmc.MonoGame.Collisions.GameTests
{
    public static class Globals
    {
        private static GameTime gameTime;
        public static GameTime GameTime
        {
            get => gameTime;
            set
            {
                gameTime = value;
                GameTimeMilliseconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public static SpriteBatch SpriteBatch { get; set; }

        public static Game1 Game1 { get; set; }

        public static ContentManager Content { get; set; }

        public static float GameTimeMilliseconds { get; private set; }
    }
}
