using System;
using System.IO;

namespace TobogganTrajectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("./data.txt");

            var totalCollisions = TrajectoryCalculator.CalculateCollisionsForMultipleRoutes(input, 
               new[] {
                    (1, 1),
                    (3, 1),
                    (5, 1),
                    (7, 1),
                    (1, 2)
                });

            Console.WriteLine($"Total lines: {input.Length}");
            Console.WriteLine($"Total Collisions: {totalCollisions}");
        }
    }

    public static class TrajectoryCalculator
    {
        public static long CalculateCollisionsForMultipleRoutes(string[] input, (int right, int down)[] routes)
        {
            // This will break if any of the routes miss all the trees
            long totalCollisions = 1;

            foreach(var route in routes)
            {
                var collisions = CalculateCollisions(input, route.right, route.down);

                Console.WriteLine($"{route} => {collisions} collisions");

                totalCollisions *= collisions;
            }

            return totalCollisions;
        }

        public static int CalculateCollisions(string[] input, int right, int down)
        {
            var grid = ConvertStringArrayToJaggedCharArray(input);
            var width = grid[0].Length; // assuming consistent size
            var height = input.Length;
            var x = 1;
            var treesHit = 0;

            for (int y = down; y < height; y += down)
            {
                x += right;

                if (x > width)
                {
                    x -= width;
                }

                var position = grid[y][x - 1];

                if (position == '#')
                {
                    treesHit++;
                }
            }

            return treesHit;
        }

        public static char[][] ConvertStringArrayToJaggedCharArray(string[] input)
        {
            // jagged arrays are better optimised by the JIT than mutlidimensional
            var jCharArray = new char[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                jCharArray[i] = input[i].ToCharArray();
            }

            return jCharArray;
        }
    }
}
