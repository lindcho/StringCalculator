using System;
using System.Linq;
using NUnit.Framework;

namespace StringCalculatorAugust8.Tests
{
    [TestFixture]
    public class CalculatorTestsV2
    {
        [Test]
        public void Add_GivenEmptyInputAsString_ShouldReturnZero()
        {
            var sut = new CalculatorModel();
            var input = "";

            var actual = sut.Add(input);
            Assert.That(actual, Is.EqualTo(0));
        }

        [TestCase("4", 4)]
        [TestCase("7", 7)]
        [TestCase("10", 10)]
        public void Add_GivenInputWithDigits_ShouldReturnThatDigit(string input, int expectedResult)
        {
            var sut = new CalculatorModel();

            var actual = sut.Add(input);
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("3,5", 8)]
        [TestCase("1,3", 4)]
        [TestCase("11,5", 16)]
        [TestCase("11,9,6", 26)]
        public void Add_GivenInputWithCommaSeparatedDigits_ShouldReturnSum(string input, int expectedResult)
        {
            var sut = new CalculatorModel();

            var actual = sut.Add(input);
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1\n5,2", 8)]
        [TestCase("2\n3,7", 12)]
        [TestCase("4\n2,2", 8)]
        public void Add_GivenInputWithNewLinesBetweenDigits_ShouldReturnSum(string input, int expectedResult)
        {
            var sut = new CalculatorModel();

            var actual = sut.Add(input);
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//;\n3;3;3", 9)]
        [TestCase("//;\n10;6;0", 16)]
        public void Add_GivenInputWithDifferentDelimitersInTheBeginning_ShouldHandleDelimitersAndReturnSum(string input, int expectedResult)
        {
            var sut = new CalculatorModel();

            var actual = sut.Add(input);
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Add_GivenInputWithNegativeNumbers_ShouldThrowException()
        {
            var sut = new CalculatorModel();
            var input = "2,3,-3,-2,1";

            var actual = Assert.Throws<Exception>(() => sut.Add(input));
            Assert.That(actual.Message, Is.EqualTo("negatives not allowed -3 -2"));
        }
    }

    public class CalculatorModel
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            var numberStringArray = input.Replace('\n', ',').Split(',');

            numberStringArray = GetDelimiter(numberStringArray);
            ValidateNegatives(numberStringArray);
            return numberStringArray.Sum(x => int.Parse(x));
        }

        public static void ValidateNegatives(string[] numberArray)
        {
            if (!numberArray.Any(x => int.Parse(x) < 0)) return;
            throw new Exception($"negatives not allowed {string.Join(" ", numberArray.Where(x => int.Parse(x) < 0))}");
        }

        private static string[] GetDelimiter(string[] numberStringArray)
        {
            if (numberStringArray[0].StartsWith("//"))
            {
                var customDelimiter = numberStringArray[0].Remove(0, 2);
                foreach (var delimiter in customDelimiter)
                {
                    numberStringArray[1] = numberStringArray[1].Replace(delimiter, ',');
                }

                numberStringArray = numberStringArray[1].Split(',');
            }

            return numberStringArray;
        }
    }
}
