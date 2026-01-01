namespace Mmc.MonoGame.UI.Models.Primitives
{
    public enum HorizontalAlignment
    {
        Left,
        Right,
        Center,
        Stretch
    }

    public enum VerticalAlignment
    {
        Top,
        Bottom,
        Center,
        Stretch
    }

    public enum TextHorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum TextVerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    public enum Orientation
    {
        Vertical,
        Horizontal
    }

    public enum MouseButton
    {
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }

    public enum TextureMode
    {
        // stretch texture accross entire destination rectangle
        Stretch,
        // place texture in the middle of the destination rectangle with no stretching
        Center,
        // tile the texture to fill space, (this requires SamplerState.LinearWrap or PointWrap to be used in UIManager)
        Tile,
        // split texture into nine segments, not stretching the four corners, but stretching remaining segments to fill space
        NineSlice
    }
}
