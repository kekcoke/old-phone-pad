using System.Text.RegularExpressions;

namespace OldPhonePad.Helpers;

public class Helpers
{
    public static bool IsAllRepeatedDigits(string val)
    {
        var sample = val.StartsWith("-") ? val.Remove(0) : val;

        if (string.IsNullOrEmpty(sample)) return false;

        char first = sample[0];

        foreach (char digit in sample)
        {
            if (digit != first) return false;
        }

        return true;
    }
}