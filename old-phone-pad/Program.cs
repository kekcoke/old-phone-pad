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
                var numpadDictionary = Constants.Constants.NumpadDictionary();
                var stringLength = input.Length;

                if (stringLength == 1)
                {
                    return numpadDictionary[input];
                }

                var list = SplitInput(input);
                var listahan = new LinkedList<string>(list);

                var result = new StringBuilder();

                // check substring. truncate one string like for 227 if not found. 
                foreach (var item in listahan)
                {
                    if (numpadDictionary.ContainsKey(item))
                    {
                        result.Append(numpadDictionary[item]);
                    }
                    else if (item.Length > 4)
                    {
                        var t = item.Substring(0, item.Length - 1);

                        if (numpadDictionary.ContainsKey(t))
                        {
                            result.Append(numpadDictionary[item]);
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown substring '{item}'");
                        }
                        
                    }
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
            var pattern = @"(\d)\1*|([#0])";

            return Regex.Matches(input, pattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList();
        }
        
    }
}

