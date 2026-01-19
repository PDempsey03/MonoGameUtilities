using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mmc.MonoGame.UI.Rendering
{
    public sealed partial class RenderContext
    {
        public void DrawLine(Vector2 start, Vector2 end, Color color, float thickness = 1)
        {
            Vector2 line = end - start;
            float angle = MathF.Atan2(line.Y, line.X);

            SpriteBatch.Draw(_pixel, start, null, color, angle, Vector2.Zero, new Vector2(line.Length(), thickness), SpriteEffects.None, 0);
        }

        public void DrawRectangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Vector2 vertex4, Color color, float thickness = 1)
        {
            DrawLine(vertex1, vertex2, color, thickness);
            DrawLine(vertex2, vertex3, color, thickness);
            DrawLine(vertex3, vertex4, color, thickness);
            DrawLine(vertex4, vertex1, color, thickness);
        }

        public void FillRectangle(Rectangle rectangle, Color color)
        {
            SpriteBatch.Draw(_pixel, rectangle, color);
        }

        public void DrawCircle(Vector2 center, float radius, Color color, int samplePoints = 50, float thickness = 1, float startingAngleRadians = 0, float endingAngleRadians = MathF.Tau)
        {
            float currentAngle = startingAngleRadians;
            float deltaAngle = (endingAngleRadians - startingAngleRadians) / samplePoints;

            Vector2 currentPoint;
            Vector2 previousPoint = center + radius * new Vector2(MathF.Cos(currentAngle), MathF.Sin(currentAngle));
            currentAngle += deltaAngle;

            for (int i = 1; i <= samplePoints; i++)
            {
                currentPoint = center + radius * new Vector2(MathF.Cos(currentAngle), MathF.Sin(currentAngle));

                DrawLine(previousPoint, currentPoint, color, thickness);

                previousPoint = currentPoint;
                currentAngle += deltaAngle;
            }
        }

        public void DrawText(string text, SpriteFont spriteFont, Vector2 position, Color color)
        {
            SpriteBatch.DrawString(spriteFont, text, position, color);
        }
    }
}
