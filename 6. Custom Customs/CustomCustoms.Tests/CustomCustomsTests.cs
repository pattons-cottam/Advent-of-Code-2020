namespace CustomCustoms.Tests
{
    using System;
    using Xunit;
    using System.IO;
    using System.Linq;

    public class CustomCustomsTests
    {
        [Fact]
        public void Sanitise_group_answers_test()
        {
            var input = File.ReadAllLines("../../../test.txt").Append("");

            var expected = new[] 
            {
                "abc",
                "abc",
                "abc",
                "a",
                "b"
            };

            Assert.Equal(expected, Program.SanitiseGroupAnswers(input.ToArray()));
        }

        [Fact]
        public void Calculate_sum_of_group_answers_test()
        {
            var input = File.ReadAllLines("../../../test.txt").Append("");

            Assert.Equal(11, Program.CalculateSumOfGroupAnswers(input.ToArray()));
        }
    }
}
