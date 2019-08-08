using System.Linq;

namespace StringCalculatorAugust8
{
    public class Calculator
    {
        public int Add(string input)
        {
            const char delimiter = ',';
            if (string.IsNullOrEmpty(input)) return 0;
            var inputStringArray = input.Replace('\n', delimiter).Split(delimiter);

            return inputStringArray.Sum(x => int.Parse(x));
        }
    }
}
