namespace BinaryBoarding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        static async Task Main(string[] args)
        {
            var allAssignedSeatIds = await CalculateAllSeatIds();
            var allPossibleSeats = Enumerable.Range(0, allAssignedSeatIds.Max());
            var missingSeats = allPossibleSeats.Except(allAssignedSeatIds);

            foreach (var seat in missingSeats)
            {
                if (allAssignedSeatIds.Contains(seat + 1) && allAssignedSeatIds.Contains(seat - 1))
                {
                    Console.WriteLine($"Seat number {seat} is this way sir.");
                }
            }

            return;
        }

        private static async Task<int[]> CalculateAllSeatIds()
        {
            var boardingPasses = File.ReadAllLines("./data.txt");
            var seatIdCalculationTasks = new List<Task<int>>();

            foreach (var pass in boardingPasses)
            {
                seatIdCalculationTasks.Add(CalculateSeatId(pass));
            }

            var seatIds = await Task.WhenAll(seatIdCalculationTasks);

            return seatIds;
        }

        public static async Task<int> CalculateSeatId(string boardingPass)
        {
            var row = FindPosition(boardingPass);
            var column = FindPosition(boardingPass, false);
            var results = await Task.WhenAll(new[] { row, column });

            return (results[0] * 8) + results[1];
        }

        public static async Task<int> FindPosition(string boardingPass, bool row = true)
        {
            var a = "";

            foreach (var c in row ? boardingPass.Take(7) : boardingPass.Skip(7))
            {
                a += (c == 'B' || c == 'R' ? 1 : 0);
            }

            return Convert.ToInt32(a, 2);
        }
    }
}
