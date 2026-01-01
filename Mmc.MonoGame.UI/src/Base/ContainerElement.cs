using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.Base
{
    public class ContainerElement : UIElement
    {
        public List<UIElement> Children { get; init; } = [];

        public void AddChild(UIElement child)
        {
            if (child.Parent is ContainerElement containerElement)
            {
                containerElement.RemoveChild(child);
            }

            child.Parent = this;
            Children.Add(child);
            MarkDirty();
        }

        public void RemoveChild(UIElement child)
        {
            if (Children.Remove(child))
            {
                child.Parent = null;
                MarkDirty();
            }

        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Update(gameTime);
            }
        }

        public override void InternalDraw(RenderContext renderContext)
        {
            base.InternalDraw(renderContext);

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(renderContext);
            }
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

        public override void Arrange(Rectangle finalRect)
        {
            base.Arrange(finalRect);

            // place children inside the content bounds
            foreach (var child in Children)
            {
                // determine how much space the child should be given
                Rectangle childSlot = child.CalculateChildRectangle(_contentBounds);

                // pass in how much space the child has been given
                child.Arrange(childSlot);
            }
        }

        public override UIElement? FindUIElementByName(string name)
        {
            var baseResult = base.FindUIElementByName(name);

            if (baseResult != null) return baseResult;

            foreach (var child in Children)
            {
                var childResult = child.FindUIElementByName(name);
                if (childResult != null) return childResult;
            }

            return null;
        }
    }
}
