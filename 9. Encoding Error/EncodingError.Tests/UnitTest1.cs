using Xunit;

namespace EncodingError.Tests
{
    public class EncodingErrorTests
    {
        [Fact]
        public void FindFirstInvalidNumber_test()
        {
            var input = new long[]
            {
                35, 20, 15, 25, 47, 40, 62, 55, 65, 95,
                102, 117, 150, 182, 127, 219, 299, 277, 309, 576
            };

            var result = XMASDataProcessor.FindFirstInvalidNumber(input, 5);

            Assert.Equal(127, result);
        }

        [Fact]
        public void FindEncryptionWeakness_test()
        {
            var input = new long[]
            {
                35, 20, 15, 25, 47, 40, 62, 55, 65, 95,
                102, 117, 150, 182, 127, 219, 299, 277, 309, 576
            };

            var result = XMASDataProcessor.FindEncryptionWeakness(input, 127);

            Assert.Equal(62, result);
        }
    }
}
