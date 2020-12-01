using System;
using System.Linq;

namespace ReportRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            Part_one();
            Part_two();
        }

        static void Part_one()
        {
            var data = DataSeed.GetData();
            var results = ReportRepairer.FindPair(data);

            Console.WriteLine($"Pair found: [{results[0]}, {results[1]}]");
            Console.WriteLine($"Result: {results[0] * results[1]}");
        }

        static void Part_two()
        {
            var data = DataSeed.GetData();
            var results = ReportRepairer.FindTriplet(data);

            Console.WriteLine($"Triplet found: [{results[0]}, {results[1]}, {results[2]}] ({results[0] + results[1] + results[2]})");
            Console.WriteLine($"Result: {results[0] * results[1] * results[2]}");
        }
    }

    public static class ReportRepairer
    {
        public static int[] FindPair(int[] data, int? target = null)
        {
            var minimum = data.Min();

            foreach (var d in data)
            {
                var newTarget = target != null
                ? 2020 - target - d
                : 2020 - d;

                if (d <= minimum) continue;

                if (data.Any(d => d == newTarget))
                {
                    return new[] { d, (int)newTarget };
                }
            }

            return null;
        }

        public static int[] FindTriplet(int[] data)
        {
            // start by removing values that aren't eligible
            var orderedData = data.OrderBy(d => d).ToArray();
            // anything bigger than the smallest two numbers is invalid
            var maximum = 2020 - orderedData[0] + orderedData[1];
            var filtered = orderedData.Where(d => d <= maximum).ToArray();

            // starting with the largest number
            foreach (var d in filtered.Reverse())
            {
                var pair = FindPair(filtered, d);

                if (pair != null)
                {
                    return new int[] { d, pair[0], pair[1] };
                }
            }

            throw new ArgumentException("no triplet found");
        }
    }
}