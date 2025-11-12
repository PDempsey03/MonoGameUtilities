using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils.Noise;
using Mmc.MonoGame.Utils.Noise.Cellular;
using Mmc.MonoGame.Utils.Noise.Fractal;
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
        plt.XLabel("X");
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

        var noise = new PerlinNoise(Seed, ZoomFactor, Octaves);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
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

    [TestMethod]
    public void TestWhiteNoise1D()
    {
        const int Seed = 60;

        var noise = new WhiteNoise(Seed);

        const int SampleCount = 100;

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
        plt.Title("1D White Noise");
        plt.XLabel("X");
        plt.YLabel("Noise Value");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestWhiteNoise2D()
    {
        const int Seed = 50;

        var noise = new WhiteNoise(Seed);

        const int SampleCountX = 100;
        const int SampleCountY = 100;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D White Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestPinkNoise1D()
    {
        const int Seed = 44;
        const int Octaves = 2;

        var noise = new PinkNoise(Seed, Octaves);

        const int SampleCount = 50;

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
        plt.Title("1D Pink Noise");
        plt.XLabel("X");
        plt.YLabel("Noise Value");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestPinkNoise2D()
    {
        const int Seed = 256;
        const int Octaves = 2;

        var noise = new PinkNoise(Seed, Octaves);

        const int SampleCountX = 50;
        const int SampleCountY = 50;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Pink Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestBrownNoise1D()
    {
        const int Seed = 44;
        const int Octaves = 2;

        var noise = new BrownNoise(Seed, Octaves);

        const int SampleCount = 50;

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
        plt.Title("1D Brown Noise");
        plt.XLabel("X");
        plt.YLabel("Noise Value");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestBrownNoise2D()
    {
        const int Seed = 358;
        const int Octaves = 2;

        var noise = new BrownNoise(Seed, Octaves);

        const int SampleCountX = 50;
        const int SampleCountY = 50;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Brown Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestFractalPerlinNoise2D()
    {
        const int Seed = 50;
        const int PerlinOctaves = 12;
        const int ZoomFactor = 100;

        const int FractalOctaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new FractalNoise(new PerlinNoise(Seed, ZoomFactor, PerlinOctaves), FractalOctaves, Lacunarity, Gain);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Fractal Perlin Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestRidgidFractalPerlinNoise2D()
    {
        const int Seed = 50;
        const int PerlinOctaves = 12;
        const int ZoomFactor = 100;

        const int FractalOctaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new RidgidFractalNoise(new PerlinNoise(Seed, ZoomFactor, PerlinOctaves), FractalOctaves, Lacunarity, Gain);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Ridgid Fractal Perlin Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestTurbulenceFractalPerlinNoise2D()
    {
        const int Seed = 50;
        const int PerlinOctaves = 12;
        const int ZoomFactor = 100;

        const int FractalOctaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new TurbulentFractalNoise(new PerlinNoise(Seed, ZoomFactor, PerlinOctaves), FractalOctaves, Lacunarity, Gain);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Turbulent Fractal Perlin Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestBillowFractalPerlinNoise2D()
    {
        const int Seed = 50;
        const int PerlinOctaves = 12;
        const int ZoomFactor = 100;

        const int FractalOctaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new BillowFractalNoise(new PerlinNoise(Seed, ZoomFactor, PerlinOctaves), FractalOctaves, Lacunarity, Gain);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Billow Fractal Perlin Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new CellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestSecondOrderCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new SecondOrderCellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Second Order Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestEdgeCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new EdgeCellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Edge Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestInvertedCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new InvertedCellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Inverted Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestBlendedCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new BlendedCellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Blended Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestMultiplicativeCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new MultiplicativeCellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Multiplicative Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestRatioCellularNoise2D()
    {
        const int Seed = 24;
        const int CellSize = 80;
        Func<Vector2, Vector2, float> distanceMetric = Vector2.Distance;

        var noise = new RatioCellularNoise(Seed, CellSize, distanceMetric);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new Grayscale();
        heatmap.Smooth = true;
        plt.Title("2D Ratio Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }
}
