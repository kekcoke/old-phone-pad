using System;
using Xunit;
using OldPhonePad;
using System.Collections;

namespace OldPhonePad.Tests
{
    public class ProgramTests
    {
        [Theory]
        [MemberData(nameof(TestData.GetSplitInputData), MemberType = typeof(TestData))]
        public void SplitInput_ReturnsExpectedResults(string input, List<string> expected)
        {
            List<string> result = Program.SplitInput(input);
            Assert.Equal(expected, result);
        }
    }

    public class TestData : IEnumerable<object[]>
    {
        const string GIVEN_INPUT_1 = "33#";
        const string GIVEN_INPUT_2 = "227*#";
        const string GIVEN_INPUT_3 = "4433555 555666#";
        const string GIVEN_INPUT_4 = "8 88777444666*664#";

        public static IEnumerable<object[]> GetSplitInputData()
        {
            yield return new object[] { "1", new List<string> { "1" } };
            yield return new object[] { "123", new List<string> { "123" } };
            yield return new object[] { "123 456", new List<string> { "123", "456" } };
            yield return new object[] { "122*#11* *", new List<string> { "122*#", "11*" } };
            yield return new object[] { " 4 2 3  88* 999*# ", new List<string> { "4", "2", "3", "88*", "999*#" } };
            yield return new object[] { " **34*#  8867*", new List<string> { "34*#", "8867*" } };
            yield return new object[] { " **34*#  8867*", new List<string> { "34*#", "8867*" } };
            yield return new object[] { GIVEN_INPUT_1, new List<string> { "33#" } };
            yield return new object[] { GIVEN_INPUT_2, new List<string> { "227*#" } };
            yield return new object[] { GIVEN_INPUT_3, new List<string> { "4433555", "555666#" } };
            yield return new object[] { GIVEN_INPUT_4, new List<string> { "8", "88777444666*", "664#" } };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}