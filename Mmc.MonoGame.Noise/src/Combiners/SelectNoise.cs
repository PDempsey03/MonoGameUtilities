namespace Mmc.MonoGame.Noise.Combiners
{
    public class SelectNoise : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        private INoise ControlNoise { get; init; }

        private double Threshold { get; init; }

        public SelectNoise(INoise noiseA, INoise noiseB, INoise controlNoise, double threshold)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
            ControlNoise = controlNoise;
            Threshold = threshold;
        }

        public double GetValue(double x, double y)
        {
            return ControlNoise.GetValue(x, y) < Threshold ? NoiseA.GetValue(x, y) : NoiseB.GetValue(x, y);
        }
    }
}
