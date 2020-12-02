using Xunit;

namespace PasswordPhilosophy.Tests
{
    public class PasswordPhilosophyTests
    {
        public static TheoryData<string, (int, int, char, string)> TestData = new TheoryData<string, (int, int, char, string)>
            {
                {"1-3 a: abcde", (1, 3, 'a', "abcde")},
                {"1-3 b: cdefg", (1, 3, 'b', "cdefg")},
                {"2-9 c: ccccccccc", (2, 9, 'c', "ccccccccc")}
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Password_and_policy_split_tests(string input, (int, int, char, string) expected)
        {
            var result = PasswordValidator.GetPasswordDetails(input);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        public void Validity_tests(string input, bool valid)
        {
            var result = PasswordValidator.ValidateOriginalRule(input);

            Assert.Equal(valid, result);
        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", false)]
        public void Positional_validity_tests(string input, bool valid)
        {
            var result = PasswordValidator.ValidatePositionalRule(input);

            Assert.Equal(valid, result);
        }
    }
}