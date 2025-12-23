using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Primitives;

namespace Mmc.MonoGame.UI.Base
{
    public abstract class UIElement
    {
        private bool _isLayoutDirty = true;
        private Vector2 _offset = Vector2.Zero;
        private Vector2 _size = Vector2.Zero;
        private Rectangle _globalBounds;
        private Rectangle _contentBounds;

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

        public Vector2 DesiredSize { get; private set; }

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
        public Rectangle ContentBounds => _contentBounds;

        // layout
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }
        public Thickness Border { get; set; }

        // hierarchy
        public bool IsLayoutDirty { get => _isLayoutDirty; private set => _isLayoutDirty = value; }
        public UIElement? Parent { get; private set; }
        public List<UIElement> Children { get; init; } = [];

        public void AddChild(UIElement child)
        {
            child.Parent?.RemoveChild(child);
            child.Parent = this;
            Children.Add(child);
        }

        public void RemoveChild(UIElement child)
        {
            if (Children.Remove(child)) child.Parent = null;
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(spriteBatch);
            }
        }

        public virtual void Measure(Vector2 availableSize)
        {
            float marginWidth = Margin.Left + Margin.Right;
            float marginHeight = Margin.Top + Margin.Bottom;

            float internalWidth = Border.Left + Border.Right + Padding.Left + Padding.Right;
            float internalHeight = Border.Top + Border.Bottom + Padding.Top + Padding.Bottom;

            // if user specified a specific size, use that size, otherwise default to 0 which will use auto sizing
            float bodyWidth = (Size.X > 0) ? Size.X : 0;
            float bodyHeight = (Size.Y > 0) ? Size.Y : 0;

            // case a: if the respective bosy size is zero, then its using auto sizing so take the whole available size and remove the margins and internal width
            // case b: if the respective body size is not zero, then that is the desired size, so subtract the internal width to get the content area
            Vector2 childConstraint = new Vector2(
                (bodyWidth == 0) ? (availableSize.X - marginWidth - internalWidth) : (bodyWidth - internalWidth),
                (bodyHeight == 0) ? (availableSize.Y - marginHeight - internalHeight) : (bodyHeight - internalHeight)
            );

            // ensure no negative numbers in constraint
            childConstraint.X = Math.Max(0, childConstraint.X);
            childConstraint.Y = Math.Max(0, childConstraint.Y);

            // measure children to see their desired sizes
            // this is the default by trying to use the max child size
            Vector2 maxDesiredChildSize = Vector2.Zero;
            foreach (var child in Children)
            {
                child.Measure(childConstraint);
                maxDesiredChildSize.X = Math.Max(maxDesiredChildSize.X, child.DesiredSize.X);
                maxDesiredChildSize.Y = Math.Max(maxDesiredChildSize.Y, child.DesiredSize.Y);
            }

            // if using auto sizing on either dimension, assign it now
            if (bodyWidth == 0) bodyWidth = maxDesiredChildSize.X + internalWidth;
            if (bodyHeight == 0) bodyHeight = maxDesiredChildSize.Y + internalHeight;

            // set the desired size representing the its size (content, padding, and border) with its margins to encapsulate its true desired size
            DesiredSize = new Vector2(
                bodyWidth + marginWidth,
                bodyHeight + marginHeight
            );

            _isLayoutDirty = false;
        }

        public virtual void Arrange(Rectangle finalRect)
        {
            // global bounds is simply the given bounds without the margins
            _globalBounds = new Rectangle(
                finalRect.X + (int)Margin.Left,
                finalRect.Y + (int)Margin.Top,
                finalRect.Width - (int)(Margin.Left + Margin.Right),
                finalRect.Height - (int)(Margin.Top + Margin.Bottom)
            );

            // content bounds is the global bounds without the border or the padding (where children can be placed)
            _contentBounds = new Rectangle(
                _globalBounds.X + (int)(Border.Left + Padding.Left),
                _globalBounds.Y + (int)(Border.Top + Padding.Top),
                Math.Max(0, _globalBounds.Width - (int)(Border.Left + Border.Right + Padding.Left + Padding.Right)),
                Math.Max(0, _globalBounds.Height - (int)(Border.Top + Border.Bottom + Padding.Top + Padding.Bottom))
            );

            // place children inside the content bounds
            foreach (var child in Children)
            {
                // determine how much space the child should be given
                Rectangle childSlot = CalculateChildRectangle(child, _contentBounds);

                // pass in how much space the child has been given
                child.Arrange(childSlot);
            }
        }

        protected virtual Rectangle CalculateChildRectangle(UIElement child, Rectangle contentBounds)
        {
            // determine width which for stretch is the whole rectangle's width, otherwise, clamp its desired width to rectangle width
            float width = child.HorizontalAlignment switch
            {
                HorizontalAlignment.Stretch => contentBounds.Width,
                _ => Math.Min(child.DesiredSize.X, contentBounds.Width)
            };

            // same for the height as the width
            float height = child.VerticalAlignment switch
            {
                VerticalAlignment.Stretch => contentBounds.Height,
                _ => Math.Min(child.DesiredSize.Y, contentBounds.Height)
            };

            // determine x based on the horizontal alignment type
            float x = child.HorizontalAlignment switch
            {
                HorizontalAlignment.Center => contentBounds.X + (contentBounds.Width - width) / 2f,
                HorizontalAlignment.Right => contentBounds.X + contentBounds.Width - width,
                _ => contentBounds.X // left and stretch both start at the far left
            };

            // determine y based on the vertical alignment type
            float y = child.VerticalAlignment switch
            {
                VerticalAlignment.Center => contentBounds.Y + (contentBounds.Height - height) / 2f,
                VerticalAlignment.Bottom => contentBounds.Y + contentBounds.Height - height,
                _ => contentBounds.Y // top and stretch both start at the top
            };

            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public void MarkDirty()
        {
            _isLayoutDirty = true;
            Parent?.MarkDirty();
        }
    }
}
