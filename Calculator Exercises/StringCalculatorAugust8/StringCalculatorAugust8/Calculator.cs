using System;
using System.Linq;

namespace StringCalculatorAugust8
{
    public class Calculator
    {
        public int Add(string input)
        {
            const char commaDelimiter = ',';
            if (string.IsNullOrEmpty(input)) return 0;
            var inputStringArray = input.Replace('\n', commaDelimiter).Split(commaDelimiter);
            inputStringArray = GetCustomDelimiter(inputStringArray, commaDelimiter);
            var numberArray = inputStringArray.Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();
            ValidateNegatives(numberArray);
            return numberArray.Where(x => x < 1000).Sum(x => x);
        }

        private static string[] GetCustomDelimiter(string[] inputStringArray, char commaDelimiter)
        {
            if (inputStringArray[0].StartsWith("//"))
            {
                var customDelimiter = inputStringArray[0].Remove(0, 2);
                foreach (var delimiter in customDelimiter)
                {
                    inputStringArray[1] = inputStringArray[1].Replace(delimiter, commaDelimiter);
                }

                inputStringArray = inputStringArray[1].Split(commaDelimiter);
            }

            return inputStringArray;
        }

        public static void ValidateNegatives(int[] numberArray)
        {
            if (!numberArray.Any(x => x < 0)) return;
            throw new Exception($"negatives not allowed {string.Join(" ", numberArray.Where(x => x < 0))}");
        }
    }
}
