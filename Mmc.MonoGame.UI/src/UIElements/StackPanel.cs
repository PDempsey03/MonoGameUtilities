using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Primitives;

namespace Mmc.MonoGame.UI.UIElements
{
    public class StackPanel : Panel
    {
        public Orientation Orientation { get; set; } = Orientation.Vertical;

        public int Spacing { get; set; } = 0;

        public override void Measure(Vector2 availableSize)
        {
            float marginWidth = Margin.Left + Margin.Right;
            float marginHeight = Margin.Top + Margin.Bottom;

            float internalWidth = Border.Left + Border.Right + Padding.Left + Padding.Right;
            float internalHeight = Border.Top + Border.Bottom + Padding.Top + Padding.Bottom;

            float bodyWidth = (Size.X > 0) ? Size.X : 0;
            float bodyHeight = (Size.Y > 0) ? Size.Y : 0;

            Vector2 childConstraint = new Vector2(
                (bodyWidth == 0) ? (availableSize.X - marginWidth - internalWidth) : (bodyWidth - internalWidth),
                (bodyHeight == 0) ? (availableSize.Y - marginHeight - internalHeight) : (bodyHeight - internalHeight)
            );

            childConstraint.X = Math.Max(0, childConstraint.X);
            childConstraint.Y = Math.Max(0, childConstraint.Y);

            // --- above is the same as base ---

            float totalStackWidth = 0;
            float totalStackHeight = 0;
            int visibleChildren = 0;

            foreach (var child in Children)
            {
                child.Measure(childConstraint);

                if (Orientation == Orientation.Vertical)
                {
                    // find total stack height with the max stack width
                    totalStackHeight += child.DesiredSize.Y;
                    totalStackWidth = Math.Max(totalStackWidth, child.DesiredSize.X);
                }
                else
                {
                    // find total stack width with the max stack height
                    totalStackWidth += child.DesiredSize.X;
                    totalStackHeight = Math.Max(totalStackHeight, child.DesiredSize.Y);
                }
                visibleChildren++;
            }

            // add additional room for spacing
            if (visibleChildren > 1)
            {
                if (Orientation == Orientation.Vertical)
                    totalStackHeight += (visibleChildren - 1) * Spacing;
                else
                    totalStackWidth += (visibleChildren - 1) * Spacing;
            }

            // auto sizing logic
            if (bodyWidth == 0) bodyWidth = totalStackWidth + internalWidth;
            if (bodyHeight == 0) bodyHeight = totalStackHeight + internalHeight;

            // update desired size
            DesiredSize = new Vector2(
                bodyWidth + marginWidth,
                bodyHeight + marginHeight
            );

            // no longer dirty
            _isLayoutDirty = false;
        }

        public override void Arrange(Rectangle finalRect)
        {
            CalculateGlobalBounds(finalRect);
            CalculateContentBounds();

            // place children linearly
            float currentOffset = 0;

            foreach (var child in Children)
            {
                Rectangle childSlot;

                if (Orientation == Orientation.Vertical)
                {
                    childSlot = new Rectangle(
                        ContentBounds.X,
                        ContentBounds.Y + (int)currentOffset,
                        ContentBounds.Width,
                        (int)child.DesiredSize.Y // give the child its desired height and allow clipping later
                    );

                    // move offset along to where the next child should start
                    currentOffset += child.DesiredSize.Y + Spacing;
                }
                else
                {
                    childSlot = new Rectangle(
                        ContentBounds.X + (int)currentOffset,
                        ContentBounds.Y,
                        (int)child.DesiredSize.X,
                        ContentBounds.Height
                    );

                    currentOffset += child.DesiredSize.X + Spacing;
                }

                // now have the child arrange itself in its designated spot in the stack
                child.Arrange(childSlot);
            }
        }
    }
}
