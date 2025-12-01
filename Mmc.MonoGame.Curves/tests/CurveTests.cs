using Microsoft.Xna.Framework;
using Mmc.MonoGame.Curves._2D;
using Mmc.MonoGame.Curves._2D.Geometric;
using Mmc.MonoGame.Curves._2D.Noise;
using Mmc.MonoGame.Curves._2D.Polynomial;
using Mmc.MonoGame.Curves._2D.Splines;
using Mmc.MonoGame.Curves._2D.Splines.Bezier;
using Mmc.MonoGame.Noise;
using Mmc.MonoGame.Noise.Fractal;
using System.Reflection;

namespace Mmc.MonoGame.Curves.Tests;

[TestClass]
public class CurveTests
{
    [TestMethod]
    public void TestQuadraticCurve2D()
    {
        QuadraticCurve2D test = new QuadraticCurve2D(new Vector2(-10, 10), new Vector2(10, 10), Vector2.Zero, .5f);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 51,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png"
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestQuadraticCurve2DWithTangents()
    {
        QuadraticCurve2D test = new QuadraticCurve2D(new Vector2(-10, 10), new Vector2(10, 10), Vector2.Zero, .5f);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 51,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowTangents = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestQuadraticCurve2DWithNormals()
    {
        QuadraticCurve2D test = new QuadraticCurve2D(new Vector2(-10, 10), new Vector2(10, 10), Vector2.Zero, .5f);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 51,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestQuadraticCurve2DWithNormalsAndTangents()
    {
        QuadraticCurve2D test = new QuadraticCurve2D(new Vector2(-10, 10), new Vector2(10, 10), Vector2.Zero, .5f);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 51,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowTangents = true,
            ShowNormals = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestLinearCurve2D()
    {
        LinearCurve2D test = new LinearCurve2D(Vector2.Zero, new Vector2(10, 10));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 11,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png"
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestLinearCurve2DWithNormals()
    {
        LinearCurve2D test = new LinearCurve2D(Vector2.Zero, new Vector2(10, 10));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 11,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCircularCurve2D()
    {
        CircularCurve2D test = new CircularCurve2D(Vector2.Zero, 5, 0, 360);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 50,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCircularCurve2DWithNormals()
    {
        CircularCurve2D test = new CircularCurve2D(Vector2.Zero, 5, 0, 360);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 50,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestEllipticalCurve2DWithNormals()
    {
        EllipticalCurve2D test = new EllipticalCurve2D(Vector2.Zero, new Vector2(5, 10), 0, 360);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 50,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestQuadraticBezierCurve2DWithNormals()
    {
        QuadraticBezierCurve2D test = new QuadraticBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 20,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCubicBezierCurve2DWithNormals()
    {
        CubicBezierCurve2D test = new CubicBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8), new Vector2(-6, 0));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 30,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestGenericBezierCurve2DWithNormals()
    {
        GenericBezierCurve2D test = new GenericBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8),
            new Vector2(-6, 0), new Vector2(0, 4), new Vector2(12, -5), new Vector2(-9, -6));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 100,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            UseEvenlySpacedPoints = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestGenericBezierCurve2D()
    {
        GenericBezierCurve2D test = new GenericBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8),
            new Vector2(-6, 0), new Vector2(0, 4), new Vector2(12, -5), new Vector2(-9, -6));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 50,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png"
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestEvenlySpacedGenericBezierCurve2D()
    {
        GenericBezierCurve2D test = new GenericBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8),
            new Vector2(-6, 0), new Vector2(0, 4), new Vector2(12, -5), new Vector2(-9, -6));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 100,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            UseEvenlySpacedPoints = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestQuadraticBezierToQuadraticCurve2D()
    {
        QuadraticBezierCurve2D test = new QuadraticBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 20,
            FileName = $"pre_{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
        };

        Curve2DVisualizer.VisualizeCurve(settings);

        QuadraticCurve2D test2 = test.ToQuadraticCurve2D();

        Curve2DVisualizationSettings settings2 = new Curve2DVisualizationSettings()
        {
            Curve = test2,
            SamplePoints = 20,
            FileName = $"post_{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
        };

        Curve2DVisualizer.VisualizeCurve(settings2);
    }

    [TestMethod]
    public void TestCompoudCurve2DWithLineBezierCircle()
    {
        LinearCurve2D curve1 = new LinearCurve2D(1, 5, Vector2.Zero);
        QuadraticBezierCurve2D curve2 = new QuadraticBezierCurve2D(curve1.GetPoint(1), new Vector2(5, 10), new Vector2(10, -10));
        CircularCurve2D curve3 = new CircularCurve2D(curve2.GetPoint(1) - new Vector2(2, 0), 2, 0, 270);

        CompoundCurve2D test = new CompoundCurve2D(curve1, curve2, curve3);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 99,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCompoudCurve2DWithLineBezierCircleWithNormals()
    {
        LinearCurve2D curve1 = new LinearCurve2D(1, 5, Vector2.Zero);
        QuadraticBezierCurve2D curve2 = new QuadraticBezierCurve2D(curve1.GetPoint(1), new Vector2(5, 10), new Vector2(10, -10));
        CircularCurve2D curve3 = new CircularCurve2D(curve2.GetPoint(1) - new Vector2(2, 0), 2, 0, 270);

        CompoundCurve2D test = new CompoundCurve2D(curve1, curve2, curve3);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 99,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestSmoothCompoudCurve2DWithLineBezierCircle()
    {
        LinearCurve2D curve1 = new LinearCurve2D(0, 5, Vector2.Zero);
        CubicBezierCurve2D curve2 = new CubicBezierCurve2D(curve1.GetPoint(1), curve1.GetPoint(1) + new Vector2(10, 0), curve1.GetPoint(1) + new Vector2(10, -10), curve1.GetPoint(1) + new Vector2(0, -10));
        CircularCurve2D curve3 = new CircularCurve2D(curve2.GetPoint(1) - new Vector2(0, 2), 2, 90, 360);

        CompoundCurve2D test = new CompoundCurve2D(curve1, curve2, curve3);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 300,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            MarkerSize = 0,
        };

        Curve2DVisualizer.VisualizeCurve(settings);

        Assert.IsTrue(test.IsSmooth);
    }

    [TestMethod]
    public void TestEvenlySpacedTriangluarCurve2DWithNormals()
    {
        TriangularCurve2D test = new TriangularCurve2D(new Vector2(-5, 10), new Vector2(10, 0), new Vector2(-1, -10));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 66,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            UseEvenlySpacedPoints = true,
            ShowNormals = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestEvenlySpacedQuadrilateralCurve2DWithNormals()
    {
        QuadrilateralCurve2D test = new QuadrilateralCurve2D(new Vector2(-5, 10), new Vector2(10, 12), new Vector2(8, -7), new Vector2(-6, -10));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 66,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            UseEvenlySpacedPoints = true,
            ShowNormals = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestEvenlySpacedPolygonalCurve2D()
    {
        PolygonalCurve2D test = new PolygonalCurve2D(new Vector2(6, 0), new Vector2(-6, -6), new Vector2(5, -7), new Vector2(6, -10),
            new Vector2(5, -2), new Vector2(10, 4), new Vector2(-7, 6), new Vector2(12, 2));

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 200,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            UseEvenlySpacedPoints = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCircularOpenSimplexNoiseCurve2DWithNormals()
    {
        CircularCurve2D baseCurve = new CircularCurve2D(Vector2.Zero, 5, 0, 360);

        FractalNoise noise = new FractalNoise(new OpenSimplexNoise(294, 10), 4);

        NoiseCurve2D test = new NoiseCurve2D(baseCurve, noise, 3);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 200,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            ShowNormals = true,
            MarkerSize = 0
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCubicBezierOpenSimplexNoiseCurve2DWithNormals()
    {
        CubicBezierCurve2D baseCurve = new CubicBezierCurve2D(new Vector2(0, 0), new Vector2(10, 4), new Vector2(0, -8), new Vector2(-6, 0));

        FractalNoise noise = new FractalNoise(new OpenSimplexNoise(26, 10), 4);

        NoiseCurve2D test = new NoiseCurve2D(baseCurve, noise, 2);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 50,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            MarkerSize = 0,
            ShowNormals = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestCubicHermiteSplineCurve2DWithNormals()
    {
        Vector2[] points = [new(1, 2), new(3, 5), new(6, 7)];
        Vector2[] tangents = [new(2, 2), new(4, -4), new(2, 4)];

        CubicHermiteSplineCurve2D test = new CubicHermiteSplineCurve2D(points, tangents);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 30,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            MarkerSize = 0,
            ShowNormals = true,
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }

    [TestMethod]
    public void TestEvenlySpacedCubicHermiteSplineCurve2DWithNoTangents()
    {
        Vector2[] points = [new(1, 2), new(3, 5), new(6, 1), new(7, 9), new(5, 7), new(3, 9)];
        Vector2[] tangents = [];

        CubicHermiteSplineCurve2D test = new CubicHermiteSplineCurve2D(points, tangents);

        Curve2DVisualizationSettings settings = new Curve2DVisualizationSettings()
        {
            Curve = test,
            SamplePoints = 200,
            FileName = $"{MethodBase.GetCurrentMethod()?.Name ?? "ERROR"}.png",
            UseEvenlySpacedPoints = true
        };

        Curve2DVisualizer.VisualizeCurve(settings);
    }
}
