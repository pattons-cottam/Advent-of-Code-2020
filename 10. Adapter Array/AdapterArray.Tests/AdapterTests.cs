using Xunit;

namespace AdapterArray.Tests
{
    public class AdapterTests
    {
        [Fact]
        public void Find_jolt_differential_from_small_sample()
        {
            var adapters = new[] {
                16, 10, 15, 5, 1, 11,
                7, 19, 6, 12, 4
            };

            var diff = Adapter.FindJoltDifferential(adapters);

            Assert.Equal(35, diff);
        }

        [Fact]
        public void Find_jolt_differential_from_large_sample()
        {
            var adapters = new[] {
                28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23,
                49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7,
                9, 4, 2, 34, 10, 3
            };

            var diff = Adapter.FindJoltDifferential(adapters);

            Assert.Equal(220, diff);

        }

        [Fact]
        public void Tuple_creation_test()
        {
            var expected = new (int, int[], long)[]
            {
                (0, new[] { 1 }, 0),
                (1, new[] { 4 }, 0),
                (4, new[] { 5, 6, 7 }, 0),
                (5, new[] { 6, 7 }, 0),
                (6, new[] { 7 }, 0),
                (7, new[] { 10 }, 0),
                (10, new[] { 11, 12 }, 0),
                (11, new[] { 12 }, 0),
                (12, new[] { 15 }, 0),
                (15, new[] { 16 }, 0),
                (16, new[] { 19 }, 0),
                // (19, null, 1),
                (19, new[] { 22 }, 0),
                (22, null, 1),
            };

            var adapters = new[] {
                16, 10, 15, 5, 1, 11,
                7, 19, 6, 12, 4
            };

            var result = Adapter.CreateTreeTuples(adapters);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateTotalArrangments_small_test()
        {
            var adapters = new[] {
                16, 10, 15, 5, 1, 11,
                7, 19, 6, 12, 4
            };

            var totalArrangements = Adapter.CalculateTotalArrangements(adapters);

            Assert.Equal(8, totalArrangements);
        }
        
        [Fact]
        public void CalculateTotalArrangments_large_test()
        {
            var adapters = new[] {
                28, 33, 18, 42, 31, 14, 46, 20, 48, 47,
                24, 23, 49, 45, 19, 38, 39, 11,  1, 32,
                25, 35,  8, 17,  7,  9,  4,  2, 34, 10,
                 3
            };

            var totalArrangements = Adapter.CalculateTotalArrangements(adapters);

            Assert.Equal(19208, totalArrangements);
        }
    }
}
