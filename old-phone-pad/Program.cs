using System;
using System.Runtime.InteropServices;
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
                string input = Console.ReadLine();

                if (input.ToLower() == exitCommand)
                {
                    Console.WriteLine("Exiting the program.");
                    break;
                }

                else if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid numpad number.");
                    continue;
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
                var numpadDictionary = NumpadDictionary();
                var stringLength = input.Length;

                if (stringLength == 1)
                {
                    return GetNumpadCharacter(input);
                }

                var list = SplitInput(input);
                var listahan = new LinkedList<string>(list);

                var result = string.Empty;
                foreach (var item in listahan)
                {
                    if (numpadDictionary.ContainsKey(item))
                    {
                        result = numpadDictionary[item];
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Unknown substring '{item}'");
                    }
                }


                return result;
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return $"An error occurred while processing the input {input}. Please ensure it is valid.";
            }
        }

        public static List<string> SplitInput(string input)
        {
            var pattern = @"\d+(?:\*#|\*|#)?";

            return Regex.Matches(input, pattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList();
        }
        
        public static string GetNumpadCharacter(string input)
        {
            var numpadDictionary = NumpadDictionary();
            if (numpadDictionary.TryGetValue(input, out string value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException($"The input '{input}' is not a valid numpad input.");
            }
        }

        private static Dictionary<string, string> NumpadDictionary()
        {
            return new Dictionary<string, string>
            {
                { "1", "1" },
                { "2", "A" },
                { "22", "B" },
                { "222", "C" },
                { "3", "D" },
                { "33", "E" },
                { "333", "F" },
                { "4", "G" },
                { "44", "H" },
                { "444", "I" },
                { "5", "J" },
                { "55", "K" },
                { "555", "L" },
                { "6", "M" },
                { "66", "N" },
                { "666", "O" },
                { "7", "P" },
                { "77", "Q" },
                { "777", "R" },
                { "7777", "S" },
                { "8", "T" },
                { "88", "U" },
                { "888", "V" },
                { "9", "W" },
                { "99", "X" },
                { "999", "Y" },
                { "9999", "Z" },
                { "*", "" },
                { "#", " " }, // this & below are space delimiters
                { " ", " " }, // but these aren't allowed at start & end.
                { "0", " " }  // or a filtered out.
            };
        }
    }
}

