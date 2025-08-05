using System;
using System.Text;
using System.Linq;
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
                var listahan = SplitInput(input);

                if (!listahan[listahan.Count - 1].Contains("#"))
                {
                    listahan[listahan.Count - 1] = listahan[listahan.Count - 1] + "#";
                }

                var result = new StringBuilder();
                var segmentIndex = 0;

                var delimiters = new Dictionary<int, List<int>>();
                delimiters = GetDelimeters(listahan);

                while (segmentIndex < listahan.Count)
                {
                    List<int> delimiterSegment;
                    delimiters.TryGetValue(segmentIndex, out delimiterSegment);
                    var segmentResult = ProcessSegment(listahan, segmentIndex, delimiterSegment);
                    // segmentIndex = GetNextStartingIndex(listahan, segmentIndex);
                    result = result.Append(segmentResult);

                    delimiters.Remove(segmentIndex);
                    segmentIndex++;
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

        private static string ProcessSegment(List<string> listahan, int segmentIndex, List<int> delimiterIndices)
        {
            if (segmentIndex >= listahan.Count) return string.Empty;

            var numPadDict = Constants.Constants.NumpadDictionary();

            // given segment, scan for ending.
            var stringSegment = listahan[segmentIndex];

            // if segment contains
            if (stringSegment.EndsWith("#") || stringSegment.EndsWith("*#"))
            {
                var cleaned = CleanSegment(stringSegment);
                return TryMatch(cleaned);
            }
            
            // if * is detected and next one is # eval the whole list


            if (numPadDict.TryGetValue(stringSegment, out var directMatch))
                return directMatch;

            throw new ArgumentException();
        }

        public static Dictionary<int, List<int>> GetDelimeters(List<string> listahan)
        {
            var delimeterDict = new Dictionary<int, List<int>>();
            string[] delimeters = { "*", "#", "*#", "0", " " };

            for (int i = 0; i < listahan.Count; i++)
            {
                for (int j = 0; j < listahan[i].Length; j++)
                {
                    var letter = listahan[i][j].ToString();

                    if (delimeters.Contains(letter))
                    {
                        int segment = i;
                        int posIndex = j;

                        if (!delimeterDict.ContainsKey(segment))
                        {
                            var list = new List<int>();
                            list.Add(posIndex);
                            delimeterDict.Add(segment, list);
                        }
                        else
                        {
                            var segmentList = delimeterDict[segment];
                            segmentList.Append(posIndex);
                            delimeterDict[segment] = segmentList;
                        }
                    }
                }
            }

            return delimeterDict;
        }

        private static string TryMatch(string segment)
        {
            var numPadDict = Constants.Constants.NumpadDictionary();

            if (segment.EndsWith("#") || segment.EndsWith("*#"))
            {
                var cleaned = CleanSegment(segment);
                return TryMatch(cleaned);
            }

            if (numPadDict.TryGetValue(segment, out var directMatch))
                    return directMatch;

            throw new ArgumentException();
        }

        private static string CleanSegment(string segment)
        {
            char[] omit = { '*', '#', ' ' };
            var arr = segment.Where(s => !omit.Contains(s)).ToArray();

            if (string.IsNullOrEmpty(new string(arr)))
                return string.Empty;

            return new string(arr);
        }
        
    }
}

