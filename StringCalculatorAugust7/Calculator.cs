using System;
using System.Linq;

namespace StringCalculatorAugust7
{
    public class Calculator
    {
        public int Add(string input)
        {
            const char delimiter = ',';
            if (string.IsNullOrEmpty(input))
                return 0;

            var numberStringArray = input.Replace('\n', delimiter).Split(delimiter);
            GetCustomDelimiter(ref numberStringArray);
            ValidateNegatives(numberStringArray);
            var numberArray = numberStringArray.Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();
            return numberArray.Where(x => x > 0 && x <= 1000).Sum(x => x);
        }

        public static void ValidateNegatives(string[] numberArray)
        {
            if (!numberArray.Any(x => int.Parse(x) < 0)) return;
            throw new Exception($"negatives not allowed {string.Join(" ", numberArray.Where(x => int.Parse(x) < 0))}");
        }

        private static void GetCustomDelimiter(ref string[] numberStringArray)
        {
            if (numberStringArray[0].StartsWith("//"))
            {
                var customDelimiters = numberStringArray[0].Remove(0, 2);

                foreach (var delimiter in customDelimiters)
                {
                    numberStringArray[1] = numberStringArray[1].Replace(delimiter, ',');
                }
                numberStringArray = numberStringArray[1].Split(',');
            }
        }
    }
}
