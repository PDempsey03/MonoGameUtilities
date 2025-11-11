using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils.Noise;
using ScottPlot;
using ScottPlot.Colormaps;
using System.Reflection;

namespace Mmc.MonoGame.Utils.Tests;

[TestClass]
public class NoiseTests
{
    private const string OutputFolder = "Noise";

    [TestMethod]
    public void TestPerlinNoise1D()
    {
        const int SampleCount = 1000;

        const int Seed = 50;
        const int Octaves = 12;
        const int ZoomFactor = 100;

        var noise = new PerlinNoise(Seed, ZoomFactor, Octaves);

        double[] x = new double[SampleCount];
        double[] y = new double[SampleCount];

        for (int i = 0; i < SampleCount; i++)
        {
            x[i] = i;
            y[i] = noise.GetValue(i, 1);
        }

        var plt = new Plot();
        var scatter = plt.Add.Scatter(x, y);
        scatter.MarkerSize = 0;
        plt.Title("1D Perlin Noise");
        plt.XLabel("T");
        plt.YLabel("Noise Value");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestPerlinNoise2D()
    {
        const int Seed = 50;
        const int Octaves = 12;
        const int ZoomFactor = 100;

        const float FinalScalar = 2;

        var noise = new PerlinNoise(Seed, ZoomFactor, Octaves);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = MathHelper.Clamp(FinalScalar * noise.GetValue(i, j), -1, 1);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Perlin Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }
}
