using ScottPlot;

namespace Mmc.MonoGame.Noise.Tests.ColorMaps
{
    public class BasicTerrainColorMap : IColormap
    {
        public string Name => "BasicTerrain";

        public double WaterCutoff { get; init; }

        public double LandCutoff { get; init; }

        public BasicTerrainColorMap(double waterCutoff = 0, double landCutoff = 20)
        {
            WaterCutoff = waterCutoff;
            LandCutoff = landCutoff;
        }

        public Color GetColor(double position)
        {
            double val = position * 256 - 128;

            if (val <= WaterCutoff)
            {
                // Blue gradient from dark (-128) to bright (WaterCutoff)
                double t = (val + 128) / (WaterCutoff + 128);
                return new Color(0, 0, (byte)(255 * t + 50 * (1 - t)));
            }
            else if (val <= LandCutoff)
            {
                // Sandy colors from dark (waterCutoff) to bright (LandCutoff)
                double t = (val - LandCutoff) / (128 - LandCutoff);
                return new Color(
                    (byte)(194 * t + 210 * (1 - t)),
                    (byte)(178 * t + 180 * (1 - t)),
                    (byte)(128 * t + 140 * (1 - t))
                );
            }
            else
            {
                // Green gradient from light (LandCutoff) to dark (128)
                double t = (val - WaterCutoff) / (128 - WaterCutoff);
                return new Color(
                    (byte)(50 * t + 34 * (1 - t)),
                    (byte)(80 * t + 160 * (1 - t)),
                    (byte)(50 * t + 34 * (1 - t))
                );
            }
        }
    }
}
