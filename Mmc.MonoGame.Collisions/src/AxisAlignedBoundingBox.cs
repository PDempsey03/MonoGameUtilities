using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Collisions
{
    public readonly struct AxisAlignedBoundingBox
    {
        private readonly float x, y, width, height;

        public readonly float X => x;

        public readonly float Y => y;

        public readonly float Width => width;

        public readonly float Height => height;

        public readonly float Top => y - height / 2;

        public readonly float Left => x - width / 2;

        public readonly float Right => x + width / 2;

        public readonly float Bottom => y + height / 2;

        public readonly Vector2 Position => new Vector2(x, y);

        public readonly float Area => width * height;

        public AxisAlignedBoundingBox(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public bool ContainsPoint(Vector2 point)
        {
            return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
        }

        public static bool DoIntersect(AxisAlignedBoundingBox aabb1, AxisAlignedBoundingBox aabb2)
        {
            Vector2 topLeftRect1 = new(aabb1.X - aabb1.Width / 2, aabb1.Y - aabb1.Height / 2);
            Vector2 topLeftRect2 = new(aabb2.X - aabb2.Width / 2, aabb2.Y - aabb2.Height / 2);


            return topLeftRect1.X < topLeftRect2.X + aabb2.Width &&
                topLeftRect2.X < topLeftRect1.X + aabb1.Width &&
                topLeftRect1.Y < topLeftRect2.Y + aabb2.Height &&
                topLeftRect2.Y < topLeftRect1.Y + aabb1.Height;
        }
    }
}
