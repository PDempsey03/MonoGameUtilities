namespace Mmc.MonoGame.Utils.Tests.Extensions
{
    public static class DoubleExtensions
    {
        public static bool AreNearlyEqual(this double a, double b, double epsilon)
        {
            double diff = Math.Abs(a - b);
            return diff <= epsilon;
        }
    }
}
