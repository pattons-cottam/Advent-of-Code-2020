namespace BinaryBoarding.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class BinaryBoardingTests
    {
        [Theory]
        [InlineData("FBFBBFFRLR", 44)]
        [InlineData("BFFFBBFRRR", 70)]
        [InlineData("FFFBBBFRRR", 14)]
        [InlineData("BBFFBBFRLL", 102)]
        public async Task Row_test(string boardingPass, int expectedRow)
        {
            var result = await Program.FindPosition(boardingPass);
            Assert.Equal(expectedRow, result);
        }

        [Theory]
        [InlineData("FBFBBFFRLR", 5)]
        [InlineData("BFFFBBFRRR", 7)]
        [InlineData("FFFBBBFRRR", 7)]
        [InlineData("BBFFBBFRLL", 4)]
        public async Task Column_test(string boardingPass, int expectedColumn)
        {
            var result = await Program.FindPosition(boardingPass, false);
            Assert.Equal(expectedColumn, result);
        }

        [Theory]
        [InlineData("FBFBBFFRLR", 357)]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public async Task Calculate_seat_id_test(string boardingPass, int expectedSeatId)
        {
            var result = await Program.CalculateSeatId(boardingPass);
            Assert.Equal(expectedSeatId, result);
        }
    }
}