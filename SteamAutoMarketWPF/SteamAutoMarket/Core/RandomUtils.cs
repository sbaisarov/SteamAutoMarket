namespace Core
{
    using System;
    using System.Linq;

    public class RandomUtils
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static readonly Random Random = new Random();

        public static string RandomString(int length) =>
            new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());

        public static int RandomInt(int min, int max) => Random.Next(min, max + 1);

        public static double RandomDobule(double min, double max) => Random.NextDouble() * (max - min) + min;
    }
}