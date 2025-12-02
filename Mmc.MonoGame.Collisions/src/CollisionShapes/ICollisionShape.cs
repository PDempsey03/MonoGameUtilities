using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Collisions.CollisionShapes
{
    public interface ICollisionShape
    {
        Vector2 Position { get; set; }

        bool ContainsPoint(Vector2 point);
    }
}
