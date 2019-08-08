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
            return inputStringArray.Sum(x => int.Parse(x));
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
    }
}
