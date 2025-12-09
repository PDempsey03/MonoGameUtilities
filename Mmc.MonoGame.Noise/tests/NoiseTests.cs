using Mmc.MonoGame.Noise.Cellular;
using Mmc.MonoGame.Noise.ColoredNoise;
using Mmc.MonoGame.Noise.Fractal;
using Mmc.MonoGame.Noise.Tests.ColorMaps;
using Mmc.MonoGame.Noise.Transforms;
using ScottPlot;
using System.Reflection;

namespace Mmc.MonoGame.Noise.Tests;

[TestClass]
public class NoiseTests
{
    private const string OutputFolder = "Noise";

    [TestMethod]
    public void TestPerlinNoise1D()
    {
        const int SampleCount = 1000;

        const int Seed = 50;
        const int ZoomFactor = 100;

        var noise = new PerlinNoise(Seed, ZoomFactor);

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
        const int ZoomFactor = 100;

        var noise = new PerlinNoise(Seed, ZoomFactor);

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
        heatmap.Colormap = new NoiseGrayScale();
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
        heatmap.Colormap = new NoiseGrayScale();
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
    public void TestFractalPerlinNoise2D()
    {
        const int Seed = 50;
        const int ZoomFactor = 100;

        const int Octaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new FractalNoise(new PerlinNoise(Seed, ZoomFactor), Octaves, Lacunarity, Gain);

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
        heatmap.Colormap = new NoiseGrayScale();
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
        const int ZoomFactor = 100;

        const int Octaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new RidgidFractalNoise(new PerlinNoise(Seed, ZoomFactor), Octaves, Lacunarity, Gain);

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
        heatmap.Colormap = new NoiseGrayScale();
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
        const int ZoomFactor = 100;

        const int Octaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new TurbulentFractalNoise(new PerlinNoise(Seed, ZoomFactor), Octaves, Lacunarity, Gain);

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
        heatmap.Colormap = new NoiseGrayScale();
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
        const int ZoomFactor = 100;

        const int Octaves = 6;
        const float Lacunarity = 2;
        const float Gain = .5f;

        var noise = new BillowFractalNoise(new PerlinNoise(Seed, ZoomFactor), Octaves, Lacunarity, Gain);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new CellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new SecondOrderCellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new EdgeCellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new InvertedCellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new BlendedCellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new MultiplicativeCellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
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

        var noise = new RatioCellularNoise(Seed, CellSize);

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
        heatmap.Colormap = new NoiseGrayScale();
        heatmap.Smooth = true;
        plt.Title("2D Ratio Cellular Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestBinnedGaussianWhiteNoise1D()
    {
        const int Seed = 76;
        const float StandrdDeviationRange = 3;

        var noise = new GaussianWhiteNoise(Seed, StandrdDeviationRange);

        const int SampleCount = 100000;

        // binning
        double min = -1;
        double max = 1;
        double step = 0.1;
        int binCount = (int)Math.Round((max - min) / step);
        int[] counts = new int[binCount]; // how many values fall within the range of its bin
        double[] centers = new double[binCount];

        // calculate centers
        for (int b = 0; b < binCount; b++)
            centers[b] = min + (b + .5) * step;

        // determine which bin each point will go in
        for (int i = 0; i < SampleCount; i++)
        {
            double val = noise.GetValue(i, 1);
            int bin = (int)Math.Floor((val - min) / step);
            if (bin >= 0 && bin < binCount)
                counts[bin]++;
        }

        var plt = new Plot();
        plt.Add.Scatter(centers, counts.Select(c => (double)c).ToArray());
        plt.Title("1D Gaussian NWhite oise Bin Counts");
        plt.XLabel("Bins");
        plt.YLabel("Noise Counts");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestGaussianWhiteNoise2D()
    {
        const int Seed = 94;

        var noise = new GaussianWhiteNoise(Seed);

        const int SampleCountX = 100;
        const int SampleCountY = 100;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j);
                Console.WriteLine(values[i, j]);
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        heatmap.Colormap = new NoiseGrayScale();
        heatmap.Smooth = true;
        plt.Title("2D Gaussian White Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestOpenSimplexNoise2D()
    {
        const int Seed = 50;
        const float ZoomFactor = 100f;

        var noise = new OpenSimplexNoise(Seed, ZoomFactor);

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
        heatmap.Colormap = new NoiseGrayScale();
        heatmap.Smooth = true;
        plt.Title("2D OpenSimplex Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestFractalOpenSimplexNoise2D()
    {
        const int Seed = 50;
        const float ZoomFactor = 100f;
        const int Octaves = 6;

        var baseNoise = new OpenSimplexNoise(Seed, ZoomFactor);
        var noise = new FractalNoise(baseNoise, Octaves);

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
        heatmap.Colormap = new NoiseGrayScale();
        heatmap.Smooth = true;
        plt.Title("2D Fractal OpenSimplex Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestFractalOpenSimplexNoise2DToTerrainMap()
    {
        const int Seed = 50;
        const float ZoomFactor = 100f;
        const int Octaves = 4;

        var baseNoise = new OpenSimplexNoise(Seed, ZoomFactor);
        var noise = new FractalNoise(baseNoise, Octaves);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j) * 128;
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);

        const double WaterCutoff = 0;
        const double LandCutoff = 20;

        heatmap.Colormap = new BasicTerrainColorMap(WaterCutoff, LandCutoff);
        heatmap.Smooth = true;
        plt.Title("2D Fractal OpenSimplex Noise Terrain");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestFractalPerlinNoise2DToTerrainMap()
    {
        const int Seed = 4;
        const int ZoomFactor = 100;
        const int Octaves = 12;

        var noise = new FractalNoise(new PerlinNoise(Seed, ZoomFactor), Octaves);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j) * 128;
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);

        const double WaterCutoff = 0;
        const double LandCutoff = 10;

        heatmap.Colormap = new BasicTerrainColorMap(WaterCutoff, LandCutoff);
        heatmap.Smooth = true;
        plt.Title("2D Perlin Noise Terrain");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestDyanmicDomainRotationOpenSimplexNoise2DToTerrainMap()
    {
        const int Seed = 50;
        const int ZoomFactor = 100;
        const int Octaves = 4;

        var baseNoise = new FractalNoise(new OpenSimplexNoise(Seed, ZoomFactor), Octaves);

        const int Seed2 = 34;
        const int ZoomFactor2 = 100;
        const int Octaves2 = 4;
        var rotationNoise = new FractalNoise(new PerlinNoise(Seed2, ZoomFactor2), Octaves2);

        const float MaxRotationAngleRadians = MathF.PI / 2;
        var noise = new DynamicDomainRotateNoise(baseNoise, rotationNoise, MaxRotationAngleRadians);

        const int SampleCountX = 1000;
        const int SampleCountY = 1000;

        double[,] values = new double[SampleCountX, SampleCountY];

        for (int i = 0; i < SampleCountX; i++)
        {
            for (int j = 0; j < SampleCountY; j++)
            {
                values[i, j] = noise.GetValue(i, j) * 128;
            }
        }

        var plt = new Plot();
        var heatmap = plt.Add.Heatmap(values);
        const double WaterCutoff = 0;
        const double LandCutoff = 10;

        heatmap.Colormap = new BasicTerrainColorMap(WaterCutoff, LandCutoff);
        heatmap.Smooth = true;
        plt.Title("2D Dyanmic Domain Rotation Perlin Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }

    [TestMethod]
    public void TestFractalValueNoise2D()
    {
        const int Seed = 13;
        const int Octaves = 4;
        const int ZoomFactor = 100;

        var noise = new FractalNoise(new ValueNoise(Seed, ZoomFactor), Octaves);

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
        heatmap.Colormap = new NoiseGrayScale();
        heatmap.Smooth = true;
        plt.Title("2D Value Noise");
        plt.XLabel("X");
        plt.YLabel("Y");

        Directory.CreateDirectory(OutputFolder);
        string path = Path.Combine(OutputFolder, $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png");
        plt.SavePng(path, 500, 500);
        Console.WriteLine($"Saved plot to {path}");
    }
}
