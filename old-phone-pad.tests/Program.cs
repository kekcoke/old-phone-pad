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
        public static IEnumerable<object[]> GetSplitInputData()
        {
            yield return new object[] { "1", new List<string> { "1" } };
            yield return new object[] { "123", new List<string> { "123" } };
            yield return new object[] { "11", new List<string> { "11" } };
            yield return new object[] { "111", new List<string> { "111" } };
            yield return new object[] { "1111", new List<string> { "1111" } };
            yield return new object[] { "11111", new List<string> { "11111" } };
            yield return new object[] { "1234567890", new List<string> { "1234567890" } };
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