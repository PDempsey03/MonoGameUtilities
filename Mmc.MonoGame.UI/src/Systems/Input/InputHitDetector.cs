using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;

namespace Mmc.MonoGame.UI.Systems.Input
{
    public static class InputHitDetector
    {
        /// <summary>
        /// Recursive method to find the innermost hit child.
        /// </summary>
        /// <param name="root">current root to search through.</param>
        /// <param name="point">point to check containment.</param>
        /// <returns>the most deeply nested UIElement that contains the point.</returns>
        public static UIElement? FindElementAt(UIElement root, Vector2 point)
        {
            // container elements need to check all of its children
            // we assume no overlap between children because otherwise, all paths would need to be checked
            if (root is ContainerElement containerElement)
            {
                for (int i = containerElement.Children.Count - 1; i >= 0; i--)
                {
                    var hit = FindElementAt(containerElement.Children[i], point);
                    if (hit != null) return hit;
                }
            }

            // content elements need to check its child
            if (root is ContentElement contentElement && contentElement.Content != null)
            {
                var hit = FindElementAt(contentElement.Content, point);
                if (hit != null) return hit;
            }

            // not element cant contain children or not inside any children, then check itself
            if (root.GlobalBounds.Contains(point)) return root;

            return null;
        }
    }
}
