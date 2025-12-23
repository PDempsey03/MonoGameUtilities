using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Primitives;
using Mmc.MonoGame.UI.Primitives.Brushes;

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

        // brushes
        public IBrush? BorderBrush { get; set; }
        public IBrush? BackgroundBrush { get; set; }

        // hierarchy
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
            if (_isLayoutDirty) Rebuild();

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            BackgroundBrush?.Draw(this, spriteBatch, _globalBounds);
            BorderBrush?.Draw(this, spriteBatch, _globalBounds);

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(spriteBatch);
            }
        }

        public virtual void Rebuild()
        {
            Rectangle parentContentBounds = (Parent != null)
                ? Parent.ContentBounds
                : new Rectangle(0, 0, (int)_size.X, (int)_size.Y);

            float availableWidth = parentContentBounds.Width - Margin.Left - Margin.Right;
            float availableHeight = parentContentBounds.Height - Margin.Top - Margin.Bottom;

            Vector2 alignPos = CalculateAlignment(availableWidth - Margin.Left - Margin.Right, availableHeight - Margin.Top - Margin.Bottom);

            float finalX = parentContentBounds.X + alignPos.X + Margin.Left + Offset.X;
            float finalY = parentContentBounds.Y + alignPos.Y + Margin.Top + Offset.Y;

            int currentWidth = (int)((HorizontalAlignment == HorizontalAlignment.Stretch) ? availableWidth : Size.X);
            int currentHeight = (int)((VerticalAlignment == VerticalAlignment.Stretch) ? availableHeight : Size.Y);

            _globalBounds = new Rectangle((int)finalX, (int)finalY, currentWidth, currentHeight);
            _contentBounds = new Rectangle(
                _globalBounds.X + (int)Padding.Left + (int)Border.Left,
                _globalBounds.Y + (int)Padding.Top + (int)Border.Top,
                _globalBounds.Width - (int)(Border.Left + Border.Right + Padding.Left + Padding.Right),
                _globalBounds.Height - (int)(Border.Top + Border.Bottom + Padding.Top + Padding.Bottom)
            );

            _isLayoutDirty = false;
            foreach (var child in Children)
            {
                child.Rebuild();
            }
        }

        private Vector2 CalculateAlignment(float availableWidth, float availableHeight)
        {
            // horizontal
            float x = HorizontalAlignment switch
            {
                HorizontalAlignment.Center => (availableWidth - Size.X) / 2,
                HorizontalAlignment.Right => availableWidth - Size.X,
                _ => 0
            };

            // vertical
            float y = VerticalAlignment switch
            {
                VerticalAlignment.Center => (availableHeight - Size.Y) / 2,
                VerticalAlignment.Bottom => availableHeight - Size.Y,
                _ => 0
            };

            return new Vector2(x, y);
        }

        public void MarkDirty()
        {
            _isLayoutDirty = true;
            foreach (var child in Children) child.MarkDirty();
        }
    }
}
