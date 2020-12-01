using Xunit;

namespace ReportRepair.Tests
{
    public class ReportRepairTests
    {
        private int[] SimpleData = new[] {
            1721,
            979,
            366,
            299,
            675,
            1456
        };

        [Fact]
        public void Simple_test()
        {
            var results = ReportRepairer.FindPair(this.SimpleData);
            var expected = new[] { 1721, 299 };

            Assert.Equal(expected, results);
        }

        [Fact]
        public void Simple_triplet_test()
        {
            var results = ReportRepairer.FindTriplet(this.SimpleData);
            var expected = new[] { 979, 366, 675 };

            Assert.Equal(expected, results);
        }
    }
}
