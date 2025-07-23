using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;

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
            try
            {
                var numpadDictionary = NumpadDictionary();
                var stringLength = input.Length;

                if (stringLength == 1)
                {
                    return GetNumpadCharacter(input);
                }

                var left = 0;
                var right = 1;
                int offsetFromEnd = stringLength - right;
                var result = string.Empty;

                while (left <= stringLength)
                {
                    var token = new System.Text.StringBuilder();

                    char currentChar = input[left];

                    // if both pointers in same place and right is last index, just append it
                    if (stringLength - 1 == right && left == right)
                    {
                        result += GetNumpadCharacter(currentChar.ToString());
                        break;
                    }
            
                    char nextChar = input[right];

                    token.Append(currentChar.ToString());

                    while (currentChar.Equals(nextChar))
                    {
                        token.Append(nextChar.ToString());

                        if (right < stringLength && !offsetFromEnd.Equals(1)) // ok for 22 = B, not for 223 BD
                        {
                            nextChar = input[right++];
                        }
                        else
                        {
                            break; //problem is 23... then it needs to process.
                        }
                    }

                    var word = token.ToString();

                    Console.WriteLine($"Processing token: {word}");
                    if (numpadDictionary.TryGetValue(word, out string value))
                    {
                        result += value;

                        // move left pointer next the right pointer
                        // this is to avoid processing the same character again
                        left = right;

                        // avoid out of range exception. reset right pointer to the next character after the current token
                        right = (right + 1).Equals(stringLength) ? left : left + 1; //buggy
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

