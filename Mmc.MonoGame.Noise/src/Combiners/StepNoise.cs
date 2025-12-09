namespace Mmc.MonoGame.Noise.Combiners
{
    public class StepNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private int Steps { get; init; }

        public StepNoise(INoise sourceNoise, int steps)
        {
            SourceNoise = sourceNoise;
            Steps = steps;
        }

        public double GetValue(double x, double y)
        {
            // convert to [0,1] range for even stepping
            double scaled = (SourceNoise.GetValue(x, y) + 1) / 2;

            // forces value to be a value on a step interval
            double stepped = Math.Floor(scaled * Steps) / Steps;

            // convert bak to [-1, 1] range
            return stepped * 2 - 1;
        }
    }
}
