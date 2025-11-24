using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils
{
    public class PseudoRandom
    {
        public int Seed { get; set; }

        public PseudoRandom(int seed)
        {
            Seed = seed;
        }

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random float in the range [0, 1]</returns>
        public float GetRandomValue(params int[] values)
        {
            unchecked
            {
                // initial hash to mix in the seed
                uint hash = (uint)Seed;

                // mix in each param value
                for (int i = 0; i < values.Length; i++)
                {
                    uint k = (uint)values[i];
                    k ^= k >> 16;
                    k *= 0x85EBCA6B;
                    k ^= k >> 13;
                    k *= 0xC2B2AE35;
                    k ^= k >> 16;
                    hash ^= k;
                    hash *= 0x27D4EB2D;
                }

                // final fix
                hash ^= hash >> 15;
                hash *= 0x85EBCA6B;
                hash ^= hash >> 13;
                hash *= 0xC2B2AE35;
                hash ^= hash >> 16;

                return (hash & 0x7fffffff) / 2147483647f; // [0,1]
            }
        }

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <returns>A pseudo-random float in the range [0, 1]</returns>
        public float GetRandomValue() => GetRandomValue(Array.Empty<int>());

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random float in the range [0, 1]</returns>
        public float GetRandomValue(params float[] values) => GetRandomValue(values.Select(v => BitConverter.SingleToInt32Bits(v)).ToArray());

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random float in the range [0, 1]</returns>
        public float GetRandomValue(params double[] values)
        {
            int[] converted = new int[2 * values.Length];
            int index = 0;

            foreach (var v in values)
            {
                long bits = BitConverter.DoubleToInt64Bits(v);
                converted[index] = (int)bits;
                converted[index + 1] = (int)(bits >> 32);
                index += 2;
            }

            return GetRandomValue(converted);
        }

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random float in the range [0, 1]</returns>
        public float GetRandomValue(params Vector2[] values)
        {
            return GetRandomValue(values.SelectMany(v => new float[] {
                BitConverter.SingleToInt32Bits(v.X), BitConverter.SingleToInt32Bits(v.Y)
            })
            .ToArray());
        }

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random float in the range [0, 1]</returns>
        public float GetRandomValue(params string[] values)
        {
            return GetRandomValue(values.Select(v => StringHash(v)).ToArray());
        }

        /// <summary>
        /// Returns a deterministic pseudo-random boolean value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random boolean value</returns>
        public bool GetRandomBoolean(params int[] values)
        {
            return GetRandomValue(values) > .5f;
        }

        /// <summary>
        /// Returns a deterministic pseudo-random color value based on the input parameters.
        /// </summary>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random color value, with alpha = 1</returns>
        public Color GetRandomColor(params int[] values)
        {
            var alteredValues = values.Append(1).ToArray();
            float r = GetRandomValue(alteredValues);
            alteredValues[^1]++;
            float g = GetRandomValue(alteredValues);
            alteredValues[^1]++;
            float b = GetRandomValue(alteredValues);

            return new Color(r, g, b);
        }

        /// <summary>
        /// Returns a deterministic pseudo-random value based on the input parameters.
        /// </summary>
        /// <param name="min">Parameter affecting the minimum returned value</param>
        /// <param name="max">Parameter affecting the maximum returned value</param>
        /// <param name="values">Input parameters affecting the psuedo random output</param>
        /// <returns>A pseudo-random float in the range [<paramref name="min"/>, <paramref name="max"/>]</returns>
        public float GetRandomValueInRange(float min = -1, float max = 1, params int[] values)
        {
            float val = GetRandomValue(values); // [0, 1]
            val *= (max - min); // [0, max-min]
            val += min; // [min, max]

            return val;
        }

        /// <summary>
        /// Shuffles the contents of <paramref name="list"/> in place.
        /// </summary>
        /// <param name="list">list to be shuffled</param>
        /// <param name="values">Input parameters affecting the shuffle</param>
        public void Shuffle<T>(IList<T> list, params int[] values)
        {
            var appendedValues = values.Append(0).ToArray();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                appendedValues[^1] = i;
                int swapIndex = (int)GetRandomValueInRange(0, i, appendedValues);
                (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
            }
        }

        private static int StringHash(string text)
        {
            unchecked
            {
                const uint RPrime = 16777619;
                uint hash = 2166136261;

                foreach (char c in text)
                {
                    hash ^= c;
                    hash *= RPrime;
                }

                return (int)hash;
            }
        }
    }
}
