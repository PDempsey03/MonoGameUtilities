using Mmc.MonoGame.Curves._2D;

namespace Mmc.MonoGame.Curves.Tests
{
    public class Curve2DVisualizationSettings
    {
        public required Curve2D Curve { get; set; }

        public required int SamplePoints { get; set; }

        public required string FileName { get; set; }

        public bool UseEvenlySpacedPoints { get; set; } = false;

        public float MarkerSize { get; set; } = 5;

        public string Title { get; set; } = "Plot";

        public string XAxis { get; set; } = "X";

        public string YAxis { get; set; } = "Y";

        public bool ShowTangents { get; set; } = false;

        public float TangentLineLength { get; set; } = 2;

        public bool ShowNormals { get; set; } = false;

        public float NormalLineLength { get; set; } = 4;

        public bool ExportPngs { get; set; } = true;

        public string OutputFolder { get; set; } = "TestPlots";
    }
}
