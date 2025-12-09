namespace Mmc.MonoGame.Utils.Extensions
{
    public static class DoubleExtensions
    {
        public static double Lerp(double value1, double value2, double interpolator)
        {
            return value1 + (value2 - value1) * interpolator;
        }
    }
}
