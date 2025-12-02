using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Collisions.CollisionShapes
{
    public struct CircleCollisionShape : ICollisionShape
    {
        public Vector2 Position { get; set; }

        public float Radius { get; set; }

        public CircleCollisionShape(Vector2 initialPosition, float radius)
        {
            Position = initialPosition;
            Radius = radius;
        }

        public CircleCollisionShape(float radius) : this(Vector2.Zero, radius) { }

        public CircleCollisionShape() : this(1) { }

        public readonly bool ContainsPoint(Vector2 point)
        {
            return Vector2.DistanceSquared(Position, point) <= Radius * Radius;
        }
    }
}
