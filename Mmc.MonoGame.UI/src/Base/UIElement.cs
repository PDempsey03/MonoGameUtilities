using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Primitives;
using Mmc.MonoGame.UI.Primitives.Brushes;

namespace Mmc.MonoGame.UI.Base
{
    public abstract class UIElement
    {
        protected bool _isLayoutDirty = true;
        protected Vector2 _offset = Vector2.Zero;
        protected Vector2 _size = Vector2.Zero;
        protected Rectangle _globalBounds;
        protected Rectangle _backgroundBounds;
        protected Rectangle _contentBounds;

        // transform
        public Vector2 Offset
        {
            get => _offset;
            set
            {
                if (_offset != value)
                {
                    _offset = value;
                    MarkDirty();
                }
            }
        }

        public Vector2 DesiredSize { get; protected set; }

        public Vector2 Size
        {
            get => _size;
            set
            {
                if (_size != value)
                {
                    _size = value;
                    MarkDirty();
                }
            }
        }

        // bounds
        public Rectangle GlobalBounds => _globalBounds;
        public Rectangle BackgroundBounds => _backgroundBounds;
        public Rectangle ContentBounds => _contentBounds;

        // layout
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }
        public Thickness Border { get; set; }

        // brushes
        public IBrush? BorderBrush { get; set; }
        public IBrush? BackgroundBrush { get; set; }

        // hierarchy
        public UIElement? Parent { get; protected internal set; }
        public bool IsLayoutDirty { get => _isLayoutDirty; private set => _isLayoutDirty = value; }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            BackgroundBrush?.Draw(this, spriteBatch, BackgroundBounds);
            BorderBrush?.Draw(this, spriteBatch, GlobalBounds);
        }

        public virtual void Measure(Vector2 availableSize)
        {
            float marginWidth = Margin.Left + Margin.Right;
            float marginHeight = Margin.Top + Margin.Bottom;

            // if user specified a specific size, use that size, otherwise default to 0 which will use auto sizing
            float bodyWidth = (Size.X > 0) ? Size.X : 0;
            float bodyHeight = (Size.Y > 0) ? Size.Y : 0;

            // set the desired size representing the its size (content, padding, and border) with its margins to encapsulate its true desired size
            DesiredSize = new Vector2(
                bodyWidth + marginWidth,
                bodyHeight + marginHeight
            );

            _isLayoutDirty = false;
        }

        public virtual void Arrange(Rectangle finalRect)
        {
            CalculateAllBounds(finalRect);
        }

        protected void CalculateAllBounds(Rectangle parentContentBounds)
        {
            CalculateGlobalBounds(parentContentBounds);
            CalculateBackgroundBounds();
            CalculateContentBounds();
        }

        protected virtual void CalculateGlobalBounds(Rectangle parentContentBounds)
        {
            // global bounds is simply the given bounds without the margins
            _globalBounds = new Rectangle(
                parentContentBounds.X + (int)Margin.Left,
                parentContentBounds.Y + (int)Margin.Top,
                parentContentBounds.Width - (int)(Margin.Left + Margin.Right),
                parentContentBounds.Height - (int)(Margin.Top + Margin.Bottom)
            );
        }

        protected virtual void CalculateBackgroundBounds()
        {
            // background bounds is the global bounds without the border
            _backgroundBounds = new Rectangle(
                _globalBounds.X + (int)Border.Left,
                _globalBounds.Y + (int)Border.Top,
                Math.Max(0, _globalBounds.Width - (int)(Border.Left + Border.Right)),
                Math.Max(0, _globalBounds.Height - (int)(Border.Top + Border.Bottom))
            );
        }

        protected virtual void CalculateContentBounds()
        {
            // content bounds is the global bounds without the border or the padding (where children can be placed)
            _contentBounds = new Rectangle(
                _globalBounds.X + (int)(Border.Left + Padding.Left),
                _globalBounds.Y + (int)(Border.Top + Padding.Top),
                Math.Max(0, _globalBounds.Width - (int)(Border.Left + Border.Right + Padding.Left + Padding.Right)),
                Math.Max(0, _globalBounds.Height - (int)(Border.Top + Border.Bottom + Padding.Top + Padding.Bottom))
            );
        }

        public void MarkDirty()
        {
            _isLayoutDirty = true;
            Parent?.MarkDirty();
        }
    }
}
