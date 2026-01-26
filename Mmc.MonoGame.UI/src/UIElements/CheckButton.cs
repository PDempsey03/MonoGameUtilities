using Mmc.MonoGame.UI.Models.Brushes;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.UIElements
{
    public class CheckButton : Button
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    CheckedChanged?.Invoke(this, EventArgs.Empty);
                    MarkDirty();
                }
            }
        }

        /// <summary>
        /// Brush used when the check button is enabled
        /// </summary>
        public IBrush ToggledBrush { get; set; }

        public event EventHandler<EventArgs>? CheckedChanged;

        public CheckButton()
        {
            ToggledBrush = new CheckBrush();

            Clicked += (s, e) => IsChecked = !IsChecked;
        }

        public override void InternalDrawContent(RenderContext renderContext)
        {
            base.InternalDrawContent(renderContext);

            if (_isChecked) ToggledBrush.Draw(this, renderContext, ContentBounds);
        }
    }
}
