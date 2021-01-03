using Xunit;

namespace SeatingSystem.Tests
{
    public class WaitingAreaTests
    {
        [Fact]
        public void Single_update_test()
        {
            var initialLayout = GetInitialLayout();

            var expected = new[]
            {
                new[] {'#','.','#','#','.','#','#','.','#','#'},
                new[] {'#','#','#','#','#','#','#','.','#','#'},
                new[] {'#','.','#','.','#','.','.','#','.','.'},
                new[] {'#','#','#','#','.','#','#','.','#','#'},
                new[] {'#','.','#','#','.','#','#','.','#','#'},
                new[] {'#','.','#','#','#','#','#','.','#','#'},
                new[] {'.','.','#','.','#','.','.','.','.','.'},
                new[] {'#','#','#','#','#','#','#','#','#','#'},
                new[] {'#','.','#','#','#','#','#','#','.','#'},
                new[] {'#','.','#','#','#','#','#','.','#','#'}
            };

            var subject = new WaitingArea(initialLayout);

            subject.UpdateLayout();

            var result = subject.CurrentLayout;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RunSimulation_test()
        {
            var initialLayout = GetInitialLayout();

            var expected = new[]
            {
                new[] {'#','.','#','L','.','L','#','.','#','#',},
                new[] {'#','L','L','L','#','L','L','.','L','#',},
                new[] {'L','.','#','.','L','.','.','#','.','.',},
                new[] {'#','L','#','#','.','#','#','.','L','#',},
                new[] {'#','.','#','L','.','L','L','.','L','L',},
                new[] {'#','.','#','L','#','L','#','.','#','#',},
                new[] {'.','.','L','.','L','.','.','.','.','.',},
                new[] {'#','L','#','L','#','#','L','#','L','#',},
                new[] {'#','.','L','L','L','L','L','L','.','L',},
                new[] {'#','.','#','L','#','L','#','.','#','#',}
            };

            var subject = new WaitingArea(initialLayout);

            subject.RunSimulation();

            var result = subject.newLayout;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Occupied_seats_calculated_correctly()
        {
            var initialLayout = GetInitialLayout();
            var subject = new WaitingArea(initialLayout);

            subject.RunSimulation();

            var result = subject.OccupiedSeats;

            Assert.Equal(37, result);
        }

        private char[][] GetInitialLayout()
        {
            var input = new[]
            {
                "L.LL.LL.LL",
                "LLLLLLL.LL",
                "L.L.L..L..",
                "LLLL.LL.LL",
                "L.LL.LL.LL",
                "L.LLLLL.LL",
                "..L.L.....",
                "LLLLLLLLLL",
                "L.LLLLLL.L",
                "L.LLLLL.LL"
            };

            var jaggedArray = new char[input.Length][];

            for (var i = 0; i < input.Length; i++)
                jaggedArray[i] = input[i].ToCharArray();

            return jaggedArray;
        }
    }
}
