using Xunit;

namespace OldPhonePad.Tests.Helpers;

public class HelperTests
{
    [Theory]
    [InlineData("3333", true)]
    [InlineData("3", true)]
    [InlineData("-99", true)]
    [InlineData("-998", false)]
    public void IsAllRepeatedDigits_EvaluateCorrectDigits(string val, bool expected)
    {

        Assert.Equal(OldPhonePad.Helpers.Helpers.IsAllRepeatedDigits(val), expected);
    }
}