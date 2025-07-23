using System;
using System.Runtime.InteropServices;
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
                int rightOffsetFromEnd = stringLength - 1 - right;
 
                var result = string.Empty;

                while (left <= stringLength)
                {
                    var token = new System.Text.StringBuilder();
                    var maxIndex = stringLength - 1;
                    
                    // if iteration is at the last character
                    if (right == left && rightOffsetFromEnd == 0)
                    {
                        char lastChar = input[stringLength - 1];
                        result += GetNumpadCharacter(lastChar.ToString());
                        return result;
                    }


                    char currentChar = input[left];
                    char nextChar = input[right];

                    token.Append(currentChar.ToString());

                    if (nextChar.Equals(currentChar))
                    {
                        while (currentChar.Equals(nextChar))
                        {
                            // if right pointer next the last char, we need to break
                            if (right < stringLength && !rightOffsetFromEnd.Equals(1))
                            {
                                token.Append(nextChar.ToString());
                                nextChar = input[right++];
                            }
                            else
                            {

                                var substringToken = token.ToString();
                                var substringLetter = GetNumpadCharacter(substringToken);
                                result += substringLetter;

                                if (rightOffsetFromEnd == 0)
                                {
                                    return result; // if right pointer is at the end, return the result
                                }
                                else
                                {
                                    left = right;
                                    right = left + 1;
                                }
                                token.Clear();
                                break;
                            }
                        }
                    }
                    else
                    {
                        var word = token.ToString();
                        var letter = GetNumpadCharacter(word);

                        result += letter;

                        // avoid out of range exception. set left pointer to the right pointer
                        if (rightOffsetFromEnd == 0 && left != right) 
                        {
                            left = right;
                            continue;
                        }
                        // if not, move both pointers to next new sequence
                        else
                        {
                            left = right;
                            right = left + 1;
                        }
                        token.Clear();
                        continue;
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

