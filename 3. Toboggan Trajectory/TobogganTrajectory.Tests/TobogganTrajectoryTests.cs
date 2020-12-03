using Xunit;

namespace TobogganTrajectory.Tests
{
    public class TobogganTrajectoryTests
    {
        [Fact]
        public void Convert_string_to_array_test()
        {
            var result = TrajectoryCalculator.ConvertStringArrayToJaggedCharArray(new[] {
                "..##.......",
                "#...#...#..",
                ".#....#..#."
            });

            var expected = new char[][] {
                new [] {'.', '.', '#', '#', '.', '.', '.', '.', '.', '.', '.' },
                new [] {'#','.','.','.','#','.','.','.','#','.','.'},
                new [] {'.','#','.','.','.','.','#','.','.','#','.'}
            };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Simple_collision_test()
        {
            var result = TrajectoryCalculator.CalculateCollisions(new[] {
                "..##.......",
                "#...#...#..",
                ".#....#..#."
            }, 3, 1);

            Assert.Equal(1, result);
        }

        private string[] TestData = new[] {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#"
        };

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(3, 1, 7)]
        [InlineData(5, 1, 3)]
        [InlineData(7, 1, 4)]
        [InlineData(1, 2, 2)]
        public void Longer_collision_tests(int right, int down, int expected)
        {
            var result = TrajectoryCalculator.CalculateCollisions(this.TestData, right, down);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiplication_test()
        {
            // experience has taught me that this is necessary
            var result = TrajectoryCalculator.CalculateCollisionsForMultipleRoutes(this.TestData,
                new[] {
                    (1, 1),
                    (3, 1),
                    (5, 1),
                    (7, 1),
                    (1, 2)
                });

            Assert.Equal(336, result);
        }
    }
}
