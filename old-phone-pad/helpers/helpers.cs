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
}