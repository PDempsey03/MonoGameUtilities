
using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Tests;

[TestClass]
public class PseudoRandomTests
{
    [TestMethod]
    public void TestEqualRandomNoParameters()
    {
        const int Seed = 34;
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue();
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue());
    }

    [TestMethod]
    public void TestEqualRandomOneParameter()
    {
        const int Seed = 34;
        const int Param = 485673;
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue(Param);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue(Param));
    }

    [TestMethod]
    public void TestEqualRandomTwoParameters()
    {
        const int Seed = 34;
        const int Param = 485673;
        const int Param2 = 34897;
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue(Param, Param2);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue(Param, Param2));
    }

    [TestMethod]
    public void TestEqualRandomManyParameters()
    {
        const int Seed = 34;
        int[] Params = [13487, 321987235, 23987, 1937376, 289324, 148734];
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue(Params);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue(Params));
    }

    [TestMethod]
    public void TestDifferentRandomManyParameters()
    {
        const int Seed = 34;
        int[] Params = [13487, 321987235, 23987, 1937376, 289324, 148734];
        PseudoRandom random = new PseudoRandom(Seed);

        HashSet<float> results = [];

        for (int i = 0; i < 1000; i++)
        {
            var newVal = random.GetRandomValue(Params.Append(i).ToArray());
            Assert.IsFalse(results.Contains(newVal));
            results.Add(newVal);
        }
    }

    [TestMethod]
    public void TestEqualRandomManyDoubleParameters()
    {
        const int Seed = 34;
        double[] Params = [127984.33, 23987.458793, 2398723780.374763, 99.99, 100d, 186463d];
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue(Params);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue(Params));
    }

    [TestMethod]
    public void TestEqualRandomManyVector2Parameters()
    {
        const int Seed = 34;
        Vector2[] Params = [Vector2.One, Vector2.Zero, new Vector2(128, 1947), new Vector2(183.49274f, 2789.1947f), new Vector2(19, 18)];
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue(Params);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue(Params));
    }

    [TestMethod]
    public void TestEqualRandomManyStringParameters()
    {
        const int Seed = 34;
        string[] Params = ["arg1", "arg2", "arg3", "arg4", "arg5", "arg6", "hello", "world"];
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomValue(Params);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomValue(Params));
    }

    [TestMethod]
    public void TestEqualRandomBooleanManyParameters()
    {
        const int Seed = 34;
        int[] Params = [1974, 12, 245, 11, 138764, 23948, 1];
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomBoolean(Params);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomBoolean(Params));
    }

    [TestMethod]
    public void TestEqualRandomColorManyParameters()
    {
        const int Seed = 34;
        int[] Params = [19374, 132, 2435, 131, 1387364, 233948, 31];
        PseudoRandom random = new PseudoRandom(Seed);

        var val = random.GetRandomColor(Params);
        for (int i = 0; i < 10; i++) Assert.IsTrue(val == random.GetRandomColor(Params));
    }

    [TestMethod]
    public void TestMinMaxRandomManyParameters()
    {
        const int Iterations = 1000000;
        const int Seed = 2457896;
        const float Min = -10;
        const float Max = 10;
        int[] Params = [234587, 23, 593, 1048382, 384, 2];
        PseudoRandom random = new PseudoRandom(Seed);

        for (int i = 0; i < Iterations; i++)
        {
            var val = random.GetRandomValueInRange(Min, Max, Params.Append(i).ToArray());
            Assert.IsTrue(val >= Min && val <= Max);
        }
    }

    [TestMethod]
    public void TestShuffleCorrectlyMixes()
    {
        const int Seed = 2457896;
        int[] Params = [29867, 73, 9374, 4846, 283, 9];
        int[] arr = [234587, 23, 593, 1048382, 384, 2, 8, 123, 4, 1493944, 48384, 38957, 144, 10548, 48384, 8274];
        int[] shuffledArray = new int[arr.Length];
        arr.CopyTo(shuffledArray, 0);

        PseudoRandom random = new PseudoRandom(Seed);

        random.Shuffle(shuffledArray, Params);

        int matchingCount = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == shuffledArray[i]) matchingCount++;
        }

        Console.WriteLine($"{matchingCount}/{arr.Length} remained at the same index");
        Assert.AreNotEqual(matchingCount, arr.Length);
    }

    [TestMethod]
    public void TestConsistentShuffle()
    {
        const int Seed = 2457896;
        int[] Params = [29867, 73, 9374, 4846, 283, 976];
        int[] arr = [234587, 23, 593, 1048382, 384, 2, 123, 4, 1493944, 48384, 38957, 144, 10548, 48384, 8274];
        int[] shuffledArray = new int[arr.Length];
        int[] secondShuffledArray = new int[arr.Length];
        arr.CopyTo(shuffledArray, 0);
        arr.CopyTo(secondShuffledArray, 0);

        PseudoRandom random = new PseudoRandom(Seed);

        random.Shuffle(shuffledArray, Params);
        random.Shuffle(secondShuffledArray, Params);

        for (int i = 0; i < arr.Length; i++)
        {
            Assert.IsTrue(shuffledArray[i] == secondShuffledArray[i]);
        }
    }
}
