using Mmc.MonoGame.Curves._2D.Polynomial;
using Mmc.MonoGame.Curves.Tests.Extensions;
using ScottPlot;

namespace Mmc.MonoGame.Curves.Tests
{
    public static class Curve2DVisualizer
    {
        public static void VisualizeCurve(Curve2DVisualizationSettings settings)
        {
            var curve = settings.Curve;
            var samplePoints = settings.SamplePoints;

            var points = settings.UseEvenlySpacedPoints ? curve.GetEvenlySpacedPoints(samplePoints) : curve.GetPoints(samplePoints);

            double[] x = points.Select(p => (double)p.X).ToArray();
            double[] y = points.Select(p => (double)p.Y).ToArray();

            var plt = new Plot();
            var scatter = plt.Add.Scatter(x, y);
            scatter.MarkerSize = settings.MarkerSize;
            plt.Title(settings.Title);
            plt.XLabel(settings.XAxis);
            plt.YLabel(settings.YAxis);

            if (settings.ShowTangents)
            {
                for (int i = 0; i < samplePoints; i++)
                {
                    float t = (float)i / (samplePoints - 1);
                    LinearCurve2D tangentLine = curve.GetTangentLine(t, settings.TangentLineLength);

                    var tangentPoints = tangentLine.GetPoints(2);

                    double[] tx = tangentPoints.Select(p => (double)p.X).ToArray();
                    double[] ty = tangentPoints.Select(p => (double)p.Y).ToArray();

                    var lineScatter = plt.Add.Scatter(tx, ty);
                    lineScatter.MarkerSize = settings.MarkerSize;
                }
            }

            if (settings.ShowNormals)
            {
                for (int i = 0; i < samplePoints; i++)
                {
                    float t = (float)i / (samplePoints - 1);
                    LinearCurve2D normalLine = curve.GetNormalLine(t, settings.NormalLineLength);

                    var normalPoints = normalLine.GetPoints(2);

                    double[] nx = normalPoints.Select(p => (double)p.X).ToArray();
                    double[] ny = normalPoints.Select(p => (double)p.Y).ToArray();

                    var lineScatter = plt.Add.Scatter(nx, ny);

                    lineScatter.MarkerSize = settings.MarkerSize;
                }
            }

            if (settings.ExportPngs)
            {
                var outputFolder = settings.OutputFolder;
                Directory.CreateDirectory(outputFolder);
                string path = Path.Combine(outputFolder, settings.FileName);
                plt.SavePng(path, 500, 500);
                Console.WriteLine($"Saved plot to {path}");
            }
        }
    }
}
