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
            // char[] delimiters = { ' ', '\t', '#' };
            // string[] numbersArray = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var numpadDictionary = NumpadDictionary();

                if (input.Length == 1)
                {
                    return GetNumpadCharacter(input);
                }

                var left = 0;
                var right = 1;
                var result = string.Empty;

                while (left < input.Length)
                {
                    // take char values of left and right
                    // append token value until the next char is different
                    // if the next char is the same, increment right
                    // if the next char is different, check if the token exists in the dictionary
                    // if it exists, append the value to the result
                    // if it doesn't exist, throw an exception

                    char currentChar = input[left];
                
                    // Check if we are at the end of the input
                    bool isLastChar = left == input.Length; // 2-length input is a special case
                    bool isLastCharNext = input.Length - 1 - right == 1;
                    var token = new System.Text.StringBuilder();

                    if (isLastChar)
                    {
                        // if we are at the last character, we can break the loop
                        Console.WriteLine($"Processing last character: {currentChar}");
                        result += GetNumpadCharacter(currentChar.ToString());
                        break;
                    }
                    else
                    {
                        char nextChar = input[right];

                        token.Append(currentChar.ToString());

                        while (currentChar.Equals(nextChar))
                        {
                            token.Append(nextChar.ToString());
                            isLastCharNext = input.Length - 1 - right == 1;

                            if (isLastCharNext)
                            {
                                break;
                            }

                            nextChar = input[++right];
                        }
                    }

                    // we get here it means contiguous characters are complete
                    var word = token.ToString();

                    Console.WriteLine($"Processing token: {word}");
                    if (numpadDictionary.TryGetValue(word, out string value))
                    {
                        result += value;

                        // move left pointer next the right pointer
                        // this is to avoid processing the same character again
                        left = right;

                        // avoid out of range exception. reset right pointer to the next character after the current token
                        right = isLastCharNext ? left : left + 1;
                        token.Clear(); // clear the token for the next iteration
                        continue;
                    }
                    else
                    {
                        throw new ArgumentException($"The input '{word}' is not a valid numpad input.");
                    }
                }

                return result;
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return $"An error occurred while processing the input.";
            }
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
                { "#", " " },
                { " ", " " }
            };
        }
    }
}

