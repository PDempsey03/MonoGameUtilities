namespace Mmc.MonoGame.Utils.Noise.Combiners
{
    public class SelectNoise : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        private INoise ControlNoise { get; init; }

        private float Threshold { get; init; }

        public SelectNoise(INoise noiseA, INoise noiseB, INoise controlNoise, float threshold)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
            ControlNoise = controlNoise;
            Threshold = threshold;
        }

        public float GetValue(float x, float y)
        {
            return ControlNoise.GetValue(x, y) < Threshold ? NoiseA.GetValue(x, y) : NoiseB.GetValue(x, y);
        }
    }
}
