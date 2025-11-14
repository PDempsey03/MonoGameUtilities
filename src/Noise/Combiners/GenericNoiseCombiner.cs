namespace Mmc.MonoGame.Utils.Noise.Combiners
{
    public class GenericNoiseCombiner : INoise
    {
        private INoise[] SourceNoises { get; init; }

        private Func<INoise[], float> NoiseFunc { get; init; }

        public GenericNoiseCombiner(Func<INoise[], float> noiseFunc, params INoise[] sourceNoises)
        {
            SourceNoises = sourceNoises;
            NoiseFunc = noiseFunc;
        }

        public float GetValue(float x, float y)
        {
            return NoiseFunc?.Invoke(SourceNoises) ?? (SourceNoises.Length > 0 ? SourceNoises[0].GetValue(x, y) : 0);
        }
    }
}
