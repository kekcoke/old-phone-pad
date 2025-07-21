using System;
using System.Text.RegularExpressions;

namespace OldPhonePad {
    class Program
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
            char[] delimiters = { ' ', '\t', '#' };
            string[] numbersArray = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;

            try
            {
                var numpadDictionary = NumpadDictionary();

                foreach (string number in numbersArray)
                {
                    if (numpadDictionary.TryGetValue(number, out string character))
                    {
                        result += character;
                    }
                    else
                    {
                        return $"Invalid input: '{number}' is not a valid numpad number.";
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return $"An error occurred while processing the input.";
            }

            return result;
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
                { "*", "*" },
                { "#", "#" } 
            };
        }
    }
}

