using Mmc.MonoGame.Collisions.CollisionShapes;

namespace Mmc.MonoGame.Collisions
{
    public static class CollisionChecker
    {
        public static bool Intersects(CircleCollisionShape circle, CircleCollisionShape circle2)
        {
            return false;
        }

        public static bool Intersects(RectangleCollisionShape rectangle, RectangleCollisionShape rectangle2)
        {
            return false;
        }

        public static bool Intersects(CapsuleCollisionShape capsule, CapsuleCollisionShape capsule2)
        {
            return false;
        }

        public static bool Intersects(RectangleCollisionShape rectangle, CircleCollisionShape circle) => Intersects(circle, rectangle);

        public static bool Intersects(CircleCollisionShape circle, RectangleCollisionShape rectangle)
        {
            return false;
        }

        public static bool Intersects(CapsuleCollisionShape capsule, CircleCollisionShape circle) => Intersects(circle, capsule);

        public static bool Intersects(CircleCollisionShape circle, CapsuleCollisionShape capsule)
        {
            return false;
        }

        public static bool Intersects(CapsuleCollisionShape capsule, RectangleCollisionShape rectangle) => Intersects(rectangle, capsule);

        public static bool Intersects(RectangleCollisionShape rectangle, CapsuleCollisionShape capsule)
        {
            return false;
        }
    }
}
