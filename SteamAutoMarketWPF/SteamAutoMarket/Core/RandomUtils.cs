namespace Core
{
    using System;
    using System.Linq;

    public static class RandomUtils
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static readonly Random Random = new Random();

        public static double RandomDouble(double min, double max) => (Random.NextDouble() * (max - min)) + min;

        public static int RandomInt(int min, int max) => Random.Next(min, max + 1);

        public static string RandomString(int length) =>
            new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}