using System.Linq;
using NUnit.Framework;

namespace StringCalculatorAugust8.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_GivenEmptyInputString_ShouldReturnZero()
        {
            //Arrange
            var sut = CreateCalculator();
            var input = "";
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(0));
        }

        [TestCase("1", 1)]
        [TestCase("4", 4)]
        [TestCase("10", 10)]
        [TestCase("16", 16)]
        public void Add_GivenNumberInputString_ShouldReturnThatNumber(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("4,5", 9)]
        [TestCase("10,1", 11)]
        [TestCase("16,6", 22)]
        [TestCase("12,4,9", 25)]
        public void Add_GivenNumberInputStringWithComma_ShouldReturnSumOfNumbers(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("4\n2,4", 10)]
        [TestCase("10\n5,12", 27)]
        [TestCase("10\n2", 12)]
        public void Add_GivenNumberInputStringWithNewLineBetweenNumbers_ShouldHandleNewLinesAndReturnSum(string input, int expectedResult)
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
            const char delimiter = ',';
            if (string.IsNullOrEmpty(input)) return 0;
            var inputStringArray = input.Replace('\n', delimiter).Split(delimiter);

            return inputStringArray.Sum(x=>int.Parse(x));
        }
    }
}
