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

        private static string ConvertNumpadInput(string input)
        {

            return $"{input}";
        }

        private Dictionary<string, string> NumpadDictionary()
        {
            return new Dictionary<string, string>
            {
                { "1", "1" },
                { "2", "ABC" },
                { "22", "B" },
                { "222", "C" },
                { "3", "DEF" },
                { "33", "E" },
                { "333", "F" },
                { "4", "GHI" },
                { "44", "H" },
                { "444", "I" },
                { "5", "JKL" },
                { "55", "K" },
                { "555", "L" },
                { "6", "MNO" },
                { "66", "N" },
                { "666", "O" },
                { "7", "PQRS" },
                { "77", "Q" },
                { "777", "R" },
                { "7777", "S" },
                { "8", "TUV" },
                { "88", "U" },
                { "888", "V" },
                { "9", "WXYZ" },
                { "99", "X" },
                { "999", "Y" },
                { "9999", "Z" },
                { "*", "*" },
                { "#", "#" } 
            };
        }
    }
}

