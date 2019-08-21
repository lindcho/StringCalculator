using System;
using System.Collections.Generic;
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

            var numbers = input.Replace('\n', delimiter).Split(delimiter);

            GetCustomDelimiter(ref numbers);

            ValidateNegativeNumbers(numbers);

            var numberArray = numbers.Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();

            return GetNumbersLessThan1000(numberArray).Sum(x => x);
        }

        private static IEnumerable<int> GetNumbersLessThan1000(int[] numberArray)
        {
            return numberArray.Where(x => x < 1000);
        }

        private static void ValidateNegativeNumbers(string[] numberArray)
        {
            var negativeNumbers = numberArray.Where(x => int.Parse(x) < 0).ToList();
            if (!negativeNumbers.Any()) return;
            throw new Exception($"negatives not allowed {string.Join(" ", negativeNumbers)}");
        }

        private static void GetCustomDelimiter(ref string[] numberStringArray)
        {
            if (StartsWith(numberStringArray))
            {
                var customDelimiters = DelimiterPart(numberStringArray).Remove(0, 2);

                foreach (var delimiter in customDelimiters)
                {
                    numberStringArray[1] = NumbersPart(numberStringArray).Replace(delimiter, ',');
                }
                numberStringArray = NumbersPart(numberStringArray).Split(',');
            }
        }

        private static string NumbersPart(string[] numberStringArray)
        {
            return numberStringArray.Last();
        }

        private static string DelimiterPart(string[] numberStringArray)
        {
            return numberStringArray.First();
        }

        private static bool StartsWith(string[] numberStringArray)
        {
            return numberStringArray[0].StartsWith("//");
        }
    }
}
