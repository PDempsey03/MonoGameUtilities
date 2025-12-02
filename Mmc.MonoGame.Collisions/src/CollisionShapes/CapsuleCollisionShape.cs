using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Collisions.CollisionShapes
{
    public struct CapsuleCollisionShape : ICollisionShape
    {
        public Vector2 Position { get; set; }

        public Vector2 HalfExtents { get; set; }

        public float Rotation { get; set; }

        public readonly AxisAlignedBoundingBox BoundingBox => new AxisAlignedBoundingBox(); // TODO: implement

        public CapsuleCollisionShape(Vector2 position, Vector2 halfExtents, float rotation)
        {
            Position = position;
            HalfExtents = halfExtents;
            Rotation = rotation;
        }

        public CapsuleCollisionShape(Vector2 position, Vector2 halfExtents) : this(position, halfExtents, 0) { }

        public CapsuleCollisionShape(Vector2 position) : this(position, Vector2.One) { }

        public CapsuleCollisionShape() : this(Vector2.Zero) { }

        public readonly bool ContainsPoint(Vector2 point)
        {
            // TODO: implement
            return false;
        }
    }
}
