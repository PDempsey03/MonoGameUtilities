using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Models.Brushes;
using Mmc.MonoGame.UI.Models.Primitives;

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

        /// <summary>
        /// Assign DesiredSize to the desired size of the entire element, including margins, border, padding, and any internal content
        /// </summary>
        /// <param name="availableSize">how much size could be given</param>
        public virtual void Measure(Vector2 availableSize)
        {
            float marginWidth = Margin.Horizontal;
            float marginHeight = Margin.Vertical;

            float internalWidth = Border.Horizontal + Padding.Horizontal;
            float internalHeight = Border.Vertical + Padding.Vertical;

            // if user specified a specific size, use that size, otherwise default to 0 which will use auto sizing
            float bodyWidth = (Size.X > 0) ? Size.X : internalWidth;
            float bodyHeight = (Size.Y > 0) ? Size.Y : internalHeight;

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
            // slot is simply the given bounds without the margins
            Rectangle slot = new Rectangle(
                parentContentBounds.X + (int)Margin.Left,
                parentContentBounds.Y + (int)Margin.Top,
                parentContentBounds.Width - (int)Margin.Horizontal,
                parentContentBounds.Height - (int)Margin.Vertical
            );

            // dont need to use the entirety of the bounds we're given, so may scale down to what is needed

            int width = (HorizontalAlignment == HorizontalAlignment.Stretch)
                ? slot.Width
                : (int)DesiredSize.X - (int)Margin.Horizontal;

            int height = (VerticalAlignment == VerticalAlignment.Stretch)
                 ? slot.Height
                 : (int)DesiredSize.Y - (int)Margin.Vertical;

            int x = slot.X;
            if (HorizontalAlignment == HorizontalAlignment.Center)
                x += (slot.Width - width) / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                x += slot.Width - width;

            int y = slot.Y;
            if (VerticalAlignment == VerticalAlignment.Center)
                y += (slot.Height - height) / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                y += slot.Height - height;

            _globalBounds = new Rectangle(x, y, Math.Max(0, width), Math.Max(0, height));
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
