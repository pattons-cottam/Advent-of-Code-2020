using System;
using System.IO;
using Xunit;

namespace SeatingSystem.Tests
{
    public class WaitingAreaTests
    {
        [Fact]
        public void Single_update_test()
        {
            var initialLayout = CreateJaggedArray(new []
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
            });

            var subject = new WaitingArea(initialLayout);

            subject.UpdateLayout();

            var result = subject.CurrentLayout;

            Assert.True(true);
        }

        private char[][] CreateJaggedArray(string[] input)
        {
            var jaggedArray = new char[input.Length][];

            for (var i = 0; i < input.Length; i++)
                jaggedArray[i] = input[i].ToCharArray();

            return jaggedArray;
        }
    }
}
