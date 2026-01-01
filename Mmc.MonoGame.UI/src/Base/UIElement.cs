using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Models.Brushes;
using Mmc.MonoGame.UI.Models.Events;
using Mmc.MonoGame.UI.Models.Primitives;

namespace Mmc.MonoGame.UI.Base
{
    public abstract class UIElement
    {
        protected bool _isLayoutDirty = true;
        protected Vector2 _offset = Vector2.Zero;
        protected Vector2 _size = Vector2.Zero;

        protected HorizontalAlignment _horizontalAlignment = HorizontalAlignment.Left;
        protected VerticalAlignment _verticalAlignment = VerticalAlignment.Top;

        protected Thickness _margin = Thickness.None;
        protected Thickness _border = Thickness.None;
        protected Thickness _padding = Thickness.None;

        protected Rectangle _globalBounds;
        protected Rectangle _backgroundBounds;
        protected Rectangle _contentBounds;

        protected bool _isMouseOver = false;

        /// <summary>
        /// Used for searching for a ui element during run time
        /// </summary>
        public string? Name { get; set; }

        public bool IsMouseOver { get => _isMouseOver; private set => _isMouseOver = value; }

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
        public HorizontalAlignment HorizontalAlignment
        {
            get => _horizontalAlignment;
            set
            {
                if (_horizontalAlignment != value)
                {
                    _horizontalAlignment = value;
                    MarkDirty();
                }
            }
        }
        public VerticalAlignment VerticalAlignment
        {
            get => _verticalAlignment;
            set
            {
                if (_verticalAlignment != value)
                {
                    _verticalAlignment = value;
                    MarkDirty();
                }
            }
        }
        public Thickness Margin
        {
            get => _margin;
            set
            {
                if (_margin != value)
                {
                    _margin = value;
                    MarkDirty();
                }
            }
        }

        public Thickness Border
        {
            get => _border;
            set
            {
                if (_border != value)
                {
                    _border = value;
                    MarkDirty();
                }
            }
        }

        public Thickness Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding = value;
                    MarkDirty();
                }
            }
        }

        // brushes
        public IBrush? BorderBrush { get; set; }
        public IBrush? BackgroundBrush { get; set; }

        // events
        public event EventHandler<EventArgs>? MouseEntered;
        public event EventHandler<EventArgs>? MouseLeft;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonPressed;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonReleased;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonHeld;

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

        public virtual UIElement? FindUIElementByName(string name)
        {
            return name == Name ? this : null;
        }

        #region Bounds

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

        #endregion Bounds

        public void MarkDirty()
        {
            _isLayoutDirty = true;
            Parent?.MarkDirty();
        }

        #region Input

        internal void RaiseMouseEnter()
        {
            OnMouseEnter();

            Parent?.RaiseMouseEnter();
        }

        public virtual void OnMouseEnter()
        {
            _isMouseOver = true;
            MouseEntered?.Invoke(this, EventArgs.Empty);
        }

        internal void RaiseMouseLeave()
        {
            OnMouseLeave();

            Parent?.RaiseMouseLeave();
        }

        public virtual void OnMouseLeave()
        {
            _isMouseOver = false;
            MouseLeft?.Invoke(this, EventArgs.Empty);
        }

        internal void RaiseMouseButtonPressed(MouseButtonEventArgs args)
        {
            OnMouseButtonPressed(args);

            if (!args.Handled) Parent?.RaiseMouseButtonPressed(args);
        }

        protected virtual void OnMouseButtonPressed(MouseButtonEventArgs args)
        {
            MouseButtonPressed?.Invoke(this, args);
        }

        internal void RaiseMouseButtonReleased(MouseButtonEventArgs args)
        {
            OnMouseButtonReleased(args);

            if (!args.Handled) Parent?.RaiseMouseButtonReleased(args);
        }

        public virtual void OnMouseButtonReleased(MouseButtonEventArgs args)
        {
            MouseButtonReleased?.Invoke(this, args);
        }

        internal virtual void RaiseMouseButtonHeld(MouseButtonEventArgs args)
        {
            OnMouseButtonHeld(args);

            if (!args.Handled) Parent?.RaiseMouseButtonHeld(args);
        }

        public virtual void OnMouseButtonHeld(MouseButtonEventArgs args)
        {
            MouseButtonHeld?.Invoke(this, args);
        }

        #endregion Input

        public override string ToString()
        {
            return $"{GetType().Name} (Name: {Name ?? "Unnamed"})";
        }
    }
}
