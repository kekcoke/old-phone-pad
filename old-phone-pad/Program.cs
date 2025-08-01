using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OldPhonePad 
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Console NumPad Converter. \n. Enter numpad numbers to convert it to a character. \n Type 'exit' to quit.");
            const string exitCommand = "exit";

            while (true)
            {
                Console.Write("Enter numpad number: ");
                string validPattern = @"^[0-9#* ]+$";

                var input = Console.ReadLine()!;

                if (input == null || string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid numpad number.");
                    continue;
                }

                else if (input.ToLower() == exitCommand)
                {
                    Console.WriteLine("Exiting the program.");
                    break;
                }

                else if (!Regex.IsMatch(input, validPattern))
                {
                    Console.WriteLine("Invalid input. Please enter only numbers, '#' or '*'.");
                    continue;
                }

                string convertedCharacter = ConvertNumpadInput(input);
                Console.WriteLine(convertedCharacter);
            }
        }

        public static string ConvertNumpadInput(string input)
        {
            try
            {
                var numpadDictionary = Constants.Constants.NumpadDictionary();
                var stringLength = input.Length;

                if (stringLength == 1)
                {
                    return numpadDictionary[input];
                }

                var endsWithHash = input.EndsWith("#", StringComparison.Ordinal);
                var endsWithHashterisk = input.EndsWith("*#", StringComparison.Ordinal);
                var listahan = SplitInput(input);

                var result = new StringBuilder();
                var len = listahan.Count() - 1;
                var currentIndex = 0;

                while (currentIndex < listahan.Count)
                {
                    int nextDelimiterIndex = FindNextDelimter(listahan, currentIndex);

                    var segmentResult = ProcessSegments(listahan, currentIndex, nextDelimiterIndex);

                    currentIndex = GetNextStartingIndex(listahan, nextDelimiterIndex);
                }


                return result.ToString();
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Constants.Constants.KeyWords.UNKNOWN;
            }
        }

        public static List<string> SplitInput(string input)
        {
            var pattern = @"(\d)\1*|([*#0])";

            return Regex.Matches(input, pattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList();
        }

        private static string ProcessSegments(List<string> listahan, int startIndex, int nextDelimiterIndex)
        {
            if (startIndex >= listahan.Count) return string.Empty;

            return string.Empty;
        }

        private static int FindNextDelimter(List<string> listahan, int startIndex)
        {
            return Math.Abs(10);
        }

        private static int GetNextStartingIndex(List<string> listahan, int nextDelimiterIndex)
        {
            return Math.Abs(10);
        }
        
    }
}

