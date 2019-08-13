using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

namespace CalculatorKata.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void Add_GivenEmptyString_ShouldReturnZero(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1", 1)]
        [TestCase("20", 20)]
        [TestCase("143", 143)]
        public void Add_GivenValidNumber_ShouldReturnSameNumber(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("6,9", 15)]
        [TestCase("120,56", 176)]
        [TestCase("30,76", 106)]
        [TestCase("30,76,2,65,2,5", 180)]
        public void Add_GivenTwoCommaSeparatedInput_ShouldReturnInputSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1\n2", 3)]
        [TestCase("1\n14", 15)]
        [TestCase("1\n1,4", 6)]
        [TestCase("1\n5,4", 10)]
        public void Add_GivenNewLineDelimiterBetweenInput_ShouldReturnInputSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//;\n43;2,3", 45)]
        [TestCase("//;\n7;2;6;5", 20)]
        [TestCase("//$\n1", 1)]
        public void Add_GivenCustomDelimiterBetweenInput_ShouldReturnInputSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("-1,-2,-4,56,9", "negatives not allowed -1 -2 -4")]
        [TestCase("1,-5,20", "negatives not allowed -5")]
        public void Add_GivenInputWithNegativeValues_ShouldThrowError(string input, string expectedResult)
        { 
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = Assert.Throws<ArgumentException>(() => sut.Add(input));
            //Assert
            Assert.That(actual.Message, Is.EqualTo(expectedResult));
        }

        [TestCase("//;\n4;2", 6)]
        [TestCase("//[*][%]\n5*8%23", 36)]
        public void Play_GivenInputWithDifferentDelimitersWithAnyLength_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1002,100", 100)]
        [TestCase("1000,21", 1021)]
        [TestCase("999,1", 1000)]
        public void Play_GivenInputWithNumbersGreaterThan1000_ShouldOnlyNumbersLessThan1000(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        private static Calculator CreateCalculator()
        {
            return new Calculator();
        }
    }

    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            var inputArray = input.Replace('\n', ',').Split(',');
            GetDelimiters(ref inputArray);

            ValidateNumbersArePositive(inputArray);

            return inputArray.Where(x=>int.Parse(x)<=1000).Sum(x => int.Parse(x));
        }

        private static void ValidateNumbersArePositive(string[] inputArray)
        {
            if (inputArray.Any(x => int.Parse(x) < 0))
            {
                throw new ArgumentException(
                    $"negatives not allowed {string.Join(" ", inputArray.Where(x => int.Parse(x) < 0))}");
            }
        }

        private static void GetDelimiters(ref string[] inputArray)
        {
            if (!inputArray[0].StartsWith("//")) return;
            var customDelimiter = inputArray[0].Remove(0, 2);
            foreach (var delimiter in customDelimiter)
            {
                inputArray[1] = inputArray[1].Replace(delimiter, ',');
            }

            inputArray = inputArray[1].Split(',');
        }
    }
}
