using System.Text.RegularExpressions;

namespace OldPhonePad.Helpers;

public class Helpers
{
    public static bool IsAllRepeatedDigits(string val)
    {
        char first = val[0];

        foreach (char digit in val)
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