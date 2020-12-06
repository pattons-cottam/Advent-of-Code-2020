namespace CustomCustoms.Tests
{
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

            Assert.Equal(expected, Program.SanitiseGroupAnswers(input.ToArray(), 1));
        }

        [Fact]
        public void Calculate_sum_of_group_answers_test()
        {
            var input = File.ReadAllLines("../../../test.txt").Append("");

            Assert.Equal(11, Program.CalculateSumOfGroupAnswers(input.ToArray(), 1));
        }

        [Fact]
        public void PartOne_test()
        {
            Assert.Equal("defghiknux", Program.PartOne("dxgiekudihnxkgf"));
        }

        [Fact]
        public void PartTwo_test()
        {
            Assert.Equal("dgikx", Program.PartTwo("dxgiekudihnxkgf", 2));
        }

        [Fact]
        public void Sanitise_group_answers_test_part_two()
        {
            var input = File.ReadAllLines("../../../test.txt").Append("");

            var expected = new[]
            {
                "abc",
                "a",
                "a",
                "b"
            };

            Assert.Equal(expected, Program.SanitiseGroupAnswers(input.ToArray(), 2));
        }

        [Fact]
        public void Calculate_sum_of_group_answers_test_part_two()
        {
            var input = File.ReadAllLines("../../../test.txt").Append("");

            Assert.Equal(6, Program.CalculateSumOfGroupAnswers(input.ToArray(), 2));
        }
    }
}
