using Microsoft.Xna.Framework.Graphics;

namespace Mmc.MonoGame.UI.Models.Text
{
    public class FontFamily
    {
        public SpriteFont Regular { get; set; }
        public SpriteFont? Bold { get; set; }
        public SpriteFont? Italic { get; set; }
        public SpriteFont? BoldItalic { get; set; }

        public FontFamily(SpriteFont regular)
        {
            Regular = regular;
        }

        public SpriteFont GetFont(bool bold, bool italic)
        {
            if (bold && italic && BoldItalic != null) return BoldItalic;
            if (bold && Bold != null) return Bold;
            if (italic && Italic != null) return Italic;
            return Regular;
        }
    }
}
