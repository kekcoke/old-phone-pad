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

        [Theory]
        [MemberData(nameof(TestData.GetSingleOutput_GivenInput), MemberType = typeof(TestData))]
        public void ConvertNumpadInput_ReturnsExpectedOneLengthCharacter(string input, string expected)
        {
            string result = Program.ConvertNumpadInput(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestData.GetMultipleOutput_GivenInput), MemberType = typeof(TestData))]
        public void ConvertNumpadInput_ReturnsMultiLengthCharacters(string input, string expected)
        {
            string result = Program.ConvertNumpadInput(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestData.GetSingleOutput_GivenList), MemberType = typeof(TestData))]
        public void FindNextDelimeter(List<string> list, int nextIndex)
        {
            var result = Program.FindNextDelimter(list, 0);
            Assert.Equal(result, nextIndex);
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
            yield return new object[] { "1#", new List<string> { "1", "#" } };
            yield return new object[] { "123#", new List<string> { "1", "2", "3", "#" } };
            yield return new object[] { "123 456#", new List<string> { "1", "2", "3", "4", "5", "6", "#" } };
            yield return new object[] { "122*11* *#", new List<string> { "1", "22","11", "#" } };
            yield return new object[] { " 4 2 3  88* 999*# ", new List<string> { "4", "2", "3", "88", "999", "#" } };
            yield return new object[] { " **34*  8867*#", new List<string> { "3", "4", "88", "6", "7", "#" } };
            yield return new object[] { GIVEN_INPUT_1, new List<string> { "33", "#" } };
            yield return new object[] { GIVEN_INPUT_2, new List<string> { "22", "7", "#" } };
            yield return new object[] { GIVEN_INPUT_3, new List<string> { "44", "33", "555", "555", "666", "#" } };
            yield return new object[] { GIVEN_INPUT_4, new List<string> { "8", "88", "777", "444", "666", "66", "4", "#" } };
            yield return new object[] { "844330336633#", new List<string> { "8", "44", "33", "0", "33", "66", "33", "#" } };
            yield return new object[] { "2*22*222*3*33*333*#", new List<string> { "2", "22", "222", "3", "33", "333", "#" } };
            yield return new object[] { "888888#", new List<string> { "888888", "#" } };
        }

        public static IEnumerable<object[]> GetSingleOutput_GivenInput()
        {
            yield return new object[] { "1", "&" };
            yield return new object[] { "11", "'" };
            yield return new object[] { "111", "(" };
            yield return new object[] { "2", "A" };
            yield return new object[] { "22", "B" };
            yield return new object[] { "222", "C" };
            yield return new object[] { "3", "D" };
            yield return new object[] { "33", "E" };
            yield return new object[] { "333", "F" };
            yield return new object[] { "4", "G" };
            yield return new object[] { "44", "H" };
            yield return new object[] { "444", "I" };
            yield return new object[] { "5", "J" };
            yield return new object[] { "55", "K" };
            yield return new object[] { "555", "L" };
            yield return new object[] { "6", "M" };
            yield return new object[] { "66", "N" };
            yield return new object[] { "666", "O" };
            yield return new object[] { "7", "P" };
            yield return new object[] { "77", "Q" };
            yield return new object[] { "777", "R" };
            yield return new object[] { "7777", "S" };
            yield return new object[] { "8", "T" };
            yield return new object[] { "88", "U" };
            yield return new object[] { "888", "V" };
            yield return new object[] { "9", "W" };
            yield return new object[] { "99", "X" };
            yield return new object[] { "999", "Y" };
            yield return new object[] { "9999", "Z" };
            yield return new object[] { " ", " " };
            yield return new object[] { "0", " " };
        }

        public static IEnumerable<object[]> GetSingleOutput_GivenList()
        {
            yield return new object[] { new List<string> { "33", "#" }, 1 };
            yield return new object[] { new List<string> { "22", "7", "#" }, 2 };
        }

        public static IEnumerable<object[]> GetMultipleOutput_GivenInput()
        {
            yield return new object[] { GIVEN_INPUT_1, "E" };
            yield return new object[] { GIVEN_INPUT_2, "B" };
            yield return new object[] { GIVEN_INPUT_3, "HELLO" };
            yield return new object[] { GIVEN_INPUT_4, "?????" };
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