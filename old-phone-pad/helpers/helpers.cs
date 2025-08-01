using System.Text.RegularExpressions;

namespace OldPhonePad.Helpers;

public class Helpers
{
    public static bool IsAllRepeatedDigits(int val)
    {
        var numString = Math.Abs(val).ToString();

        char first = numString[0];

        foreach (char digit in numString)
        {
            if (digit != first) return false;
        }

        return true;
    }

    public static bool IsValidT9Pattern(int val)
    {
        var regex = new Regex(@"");

        return regex.IsMatch(val.ToString());
    }
}