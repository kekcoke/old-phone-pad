using System;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using System.Linq;

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
                var listahan = SplitInput(input);
                var result = new StringBuilder();
                var currentIndex = 0;

                while (currentIndex < listahan.Count)
                {
                    int nextDelimiterIndex = FindNextDelimter(listahan, currentIndex);
                    var segmentResult = ProcessSegment(listahan, currentIndex, nextDelimiterIndex);
                    currentIndex = GetNextStartingIndex(listahan, nextDelimiterIndex);
                    result = result.Append(segmentResult);
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

        private static string ProcessSegment(List<string> listahan, int startIndex, int nextDelimiterIndex)
        {
            if (startIndex >= listahan.Count) return string.Empty;

            // if it ends in *# then eval whole segment
            var numPadDict = Constants.Constants.NumpadDictionary();

            // process repeat digits. patch fix.
            if (listahan.Count == 1 && listahan[0].Length < 5)
            {
                return numPadDict[listahan[0]];
            }

            // determine type of delimiter
            string nextDelimiter = listahan[nextDelimiterIndex];

            if (nextDelimiter == "*" && listahan[nextDelimiterIndex + 1] == "#")
            {
                var dict = Constants.Constants.NumpadDictionary;
                // You can add logic here to process the segment using 'dict'
                var fullSegment = new StringBuilder();

                for (int i = startIndex; i < nextDelimiterIndex; i++)
                {
                    // skip *  acting as in-between letter delimiters
                    if (listahan[i] == "*") continue;

                    fullSegment.Append(listahan[i]);
                }

                var fullSegmentStr = fullSegment.ToString();
                if (numPadDict.ContainsKey(fullSegmentStr))
                {
                    return numPadDict[fullSegmentStr];
                }
                else
                {
                    var truncated = fullSegmentStr.Substring(0, fullSegmentStr.Length - 1);

                    if (numPadDict.ContainsKey(truncated))
                        return numPadDict[truncated];
    
                    return Constants.Constants.KeyWords.UNKNOWN;
                }

            }

            var bitSegment = new StringBuilder();
        
            for (int i = startIndex; i < nextDelimiterIndex; i++)
            {
                // skip *  acting as in-between letter delimiters
                if (listahan[i] == "*") continue;

                bitSegment.Append(listahan[i]);
            }

            var bitSegmentStr = bitSegment.ToString();
            if (numPadDict.ContainsKey(bitSegmentStr))
            {
                return numPadDict[bitSegmentStr];
            }
            else
            {
                var truncated = bitSegmentStr.Substring(0, bitSegmentStr.Length - 1);

                if (numPadDict.ContainsKey(truncated))
                    return numPadDict[truncated];
    
                return Constants.Constants.KeyWords.UNKNOWN;
            }

        }

        public static int FindNextDelimter(List<string> listahan, int startIndex)
        {
            string[] delimeters = { "*", "*#", "0", " " };

            if (listahan.Count == 1)
            {
                for (int i = 0; i < listahan[0].Length; i++)
                {
                    var letter = listahan[0][i].ToString();
                    if (delimeters.Contains(letter))
                        return i;
                }

                return listahan[0].Length;
            }

            for (int i = startIndex; i < listahan.Count; i++)
            {
                if (delimeters.Contains(listahan[i]))
                    return i;
            }

            return listahan.Count;
        }

        private static int GetNextStartingIndex(List<string> listahan, int nextIndex)
        {
            if (nextIndex >= listahan.Count)
            {
                return listahan.Count;
            }

            // Check for "*#" pattern
            if (nextIndex + 1 < listahan.Count && listahan[nextIndex + 1] == "#")
            {
                return nextIndex + 2; // Skip both
            }
            
            return nextIndex + 1; // Skip "*"
        }
        
    }
}

