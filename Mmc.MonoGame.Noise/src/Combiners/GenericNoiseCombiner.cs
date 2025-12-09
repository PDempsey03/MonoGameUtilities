namespace Mmc.MonoGame.Noise.Combiners
{
    public class GenericNoiseCombiner : INoise
    {
        private INoise[] SourceNoises { get; init; }

        private Func<INoise[], double> NoiseFunc { get; init; }

        public GenericNoiseCombiner(Func<INoise[], double> noiseFunc, params INoise[] sourceNoises)
        {
            SourceNoises = sourceNoises;
            NoiseFunc = noiseFunc;
        }

        public double GetValue(double x, double y)
        {
            return NoiseFunc?.Invoke(SourceNoises) ?? (SourceNoises.Length > 0 ? SourceNoises[0].GetValue(x, y) : 0);
        }
    }
}
