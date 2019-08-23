using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorBeta2
{
    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            var delimiters = GetDelimiters(input);

            var numberList = GetNumberList(input, delimiters);

            var sum = numberList.Sum();

            return sum;
        }

        private List<int> GetNumberList(string input, string[] delimiters)
        {
            var numberString = input;

            if (HasCustomDelimiter(input))
                numberString = input.Split('\n')[1];

            var numberList = numberString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            ThrowExceptionIfAnyNegativeValues(numberList);

            numberList = RemoveValuesGreaterThanAThousand(numberList);
            return numberList;
        }

        private List<int> RemoveValuesGreaterThanAThousand(List<int> numberList)
        {
            return numberList.Where(x => x <= 1000).ToList();
        }

        private void ThrowExceptionIfAnyNegativeValues(List<int> numberList)
        {
            var negativeValues = numberList.Where(x => x < 0).ToList();

            if (negativeValues.Count > 0)
                throw new Exception($"Negatives not allowed. ({string.Join(",", negativeValues)})");
        }

        private string[] GetDelimiters(string input)
        {
            var delimiters = new[] { ",", "\n" };

            if (HasCustomDelimiter(input))
                delimiters = GetCustomDelimiters(input);

            return delimiters;
        }

        private string[] GetCustomDelimiters(string input)
        {
            var delimiterString = input.Split('\n')[0]
                .Remove(0, 2);
            var delimiters = delimiterString.Replace("[", "")
                .Split(']');

            return delimiters;
        }

        private bool HasCustomDelimiter(string input)
        {
            return input.StartsWith("//");
        }
    }
}
