using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.Base
{
    public abstract class ContentElement : UIElement
    {
        private UIElement? _content;

        public UIElement? Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    if (_content != null) _content.Parent = null;
                    _content = value;
                    if (_content != null) _content.Parent = this;
                    MarkDirty();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Content?.Update(gameTime);
        }

        public override void InternalDrawContent(RenderContext renderContext)
        {
            base.InternalDrawContent(renderContext);

            Content?.Draw(renderContext);
        }

        public override void Measure(Vector2 availableSize)
        {
            float marginWidth = Margin.Horizontal;
            float marginHeight = Margin.Vertical;

            float internalWidth = Border.Horizontal + Padding.Horizontal;
            float internalHeight = Border.Vertical + Padding.Vertical;

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

            Vector2 desiredChildSize = Vector2.Zero;

            if (_content != null)
            {
                _content.Measure(childConstraint);
                desiredChildSize = _content.DesiredSize;
            }

            // if using auto sizing on either dimension, assign it now
            if (bodyWidth == 0) bodyWidth = desiredChildSize.X + internalWidth;
            if (bodyHeight == 0) bodyHeight = desiredChildSize.Y + internalHeight;

            // set the desired size representing the its size (content, padding, and border) with its margins to encapsulate its true desired size
            DesiredSize = new Vector2(
                bodyWidth + marginWidth,
                bodyHeight + marginHeight
            );

            _isLayoutDirty = false;
        }

        public override void Arrange(Rectangle finalRect)
        {
            base.Arrange(finalRect);

            if (_content != null)
            {
                // determine how much space the child should be given
                Rectangle childSlot = _content.CalculateChildRectangle(_contentBounds);

                // pass in how much space the child has been given
                _content.Arrange(childSlot);
            }
        }

        public override UIElement? FindUIElementByName(string name)
        {
            var baseResult = base.FindUIElementByName(name);

            return baseResult ?? Content?.FindUIElementByName(name) ?? null;
        }
    }
}
