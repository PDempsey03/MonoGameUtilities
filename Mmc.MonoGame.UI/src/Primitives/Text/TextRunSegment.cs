using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mmc.MonoGame.UI.Primitives.Text
{
    public class TextRunSegment
    {
        public required string Text;
        public required SpriteFont Font;
        public required Color Color;
        public required bool IsUnderlined;
        public required Vector2 PositionOffset;
        public required Vector2 Size; // pre-calculated size of this run
    }
}
