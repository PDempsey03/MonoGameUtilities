using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mmc.MonoGame.UI
{
    public static class Drawer
    {
        private static Texture2D _pixel;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1)
        {
            Vector2 line = end - start;
            float angle = MathF.Atan2(line.Y, line.X);

            spriteBatch.Draw(_pixel, start, null, color, angle, Vector2.Zero, new Vector2(line.Length(), thickness), SpriteEffects.None, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Vector2 vertex4, Color color, float thickness = 1)
        {
            DrawLine(spriteBatch, vertex1, vertex2, color, thickness);
            DrawLine(spriteBatch, vertex2, vertex3, color, thickness);
            DrawLine(spriteBatch, vertex3, vertex4, color, thickness);
            DrawLine(spriteBatch, vertex4, vertex1, color, thickness);
        }

        public static void FillRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(_pixel, rectangle, color);
        }


        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 center, float radius, Color color, int samplePoints = 50, float thickness = 1, float startingAngleRadians = 0, float endingAngleRadians = MathF.Tau)
        {
            float currentAngle = startingAngleRadians;
            float deltaAngle = (endingAngleRadians - startingAngleRadians) / samplePoints;

            Vector2 currentPoint;
            Vector2 previousPoint = center + radius * new Vector2(MathF.Cos(currentAngle), MathF.Sin(currentAngle));
            currentAngle += deltaAngle;

            for (int i = 1; i <= samplePoints; i++)
            {
                currentPoint = center + radius * new Vector2(MathF.Cos(currentAngle), MathF.Sin(currentAngle));

                DrawLine(spriteBatch, previousPoint, currentPoint, color, thickness);

                previousPoint = currentPoint;
                currentAngle += deltaAngle;
            }
        }
    }
}
