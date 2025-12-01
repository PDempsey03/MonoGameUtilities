using ScottPlot;

namespace Mmc.MonoGame.Noise.Tests.ColorMaps
{
    public class NoiseGrayScale : IColormap
    {
        public string Name => "NoiseGrayscale";

        public Color GetColor(double normalizedIntensity)
        {
            normalizedIntensity = (NumericConversion.Clamp(normalizedIntensity, -1, 1) + 1) / 2;
            byte value = (byte)(255 * normalizedIntensity);
            return Color.Gray(value);
        }
    }
}
