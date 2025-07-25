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
                var numpadDictionary = Constants.Constants.NumpadDictionary();
                var stringLength = input.Length;

                if (stringLength == 1)
                {
                    return numpadDictionary[input];
                }

                var list = SplitInput(input);
                var listahan = new LinkedList<string>(list);

                var result = string.Empty;
                foreach (var item in listahan)
                {
                    // loop each item and append to token
                    // if prev != next then get char
                    // if none can be found, iteratively cut last
                    // char until a match is found
                    // for item length no more than 3-4 chars,
                    // depending on the numpad value
                    // if none found, throw exception

                    // for # elements
                    if (item.Length == 1 && numpadDictionary.ContainsKey(item))
                    {
                        result += numpadDictionary[item];
                        continue;
                    }

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
            var pattern = @"\d+|[#0]";

            return Regex.Matches(input, pattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList();
        }
        
    }
}

