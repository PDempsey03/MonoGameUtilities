using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Collisions.CollisionShapes
{
    public struct RectangleCollisionShape : ICollisionShape
    {
        public Vector2 Position { get; set; }

        public Vector2 HalfExtents { get; set; }

        public float Rotation { get; set; }

        public readonly AxisAlignedBoundingBox BoundingBox => new AxisAlignedBoundingBox(Position.X, Position.Y, 2 * MathF.Cos(Rotation), 2 * MathF.Sin(Rotation));

        public readonly Vector2 Right => new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        public readonly Vector2 Up => new Vector2(-MathF.Sin(Rotation), MathF.Cos(Rotation));

        public RectangleCollisionShape(Vector2 position, Vector2 halfExtents, float rotation)
        {
            Position = position;
            HalfExtents = halfExtents;
            Rotation = rotation;
        }

        public RectangleCollisionShape(Vector2 position, Vector2 halfExtents) : this(position, halfExtents, 0) { }

        public RectangleCollisionShape(Vector2 position) : this(position, Vector2.One) { }

        public RectangleCollisionShape() : this(Vector2.Zero) { }

        public readonly Vector2[] GetCorners()
        {
            var r = Right;
            var u = Up;
            var hx = HalfExtents.X;
            var hy = HalfExtents.Y;

            return
            [
                Position + r * hx + u * hy,
                Position - r * hx + u * hy,
                Position - r * hx - u * hy,
                Position + r * hx - u * hy
            ];
        }

        public readonly bool ContainsPoint(Vector2 point)
        {
            // center point about the center of the rectangle
            Vector2 localPoint = point - Position;

            // calculate rotation scaling factors
            float cos = MathF.Cos(-Rotation);
            float sin = MathF.Sin(-Rotation);

            // turn into axis aligned space for easy bounding box test
            float localX = localPoint.X * cos - localPoint.Y * sin;
            float localY = localPoint.X * sin + localPoint.Y * cos;

            // perform test in the rotated space
            return localX >= -HalfExtents.X && localX <= HalfExtents.X &&
                   localY >= -HalfExtents.Y && localY <= HalfExtents.Y;
        }
    }
}
