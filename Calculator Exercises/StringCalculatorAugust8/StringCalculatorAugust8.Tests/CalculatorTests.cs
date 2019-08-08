using System;
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

        [TestCase("//;\n1;2", 3)]
        [TestCase("//;\n5;6;4", 15)]
        [TestCase("//;\n1;2;7;2", 12)]
        public void Add_GivenNumberInputStringWithSeparateLine_ShouldHandleSeparateLinesAndReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1,12,-3,-4", "negatives not allowed -3 -4")]
        [TestCase("8,-5,-12,-4", "negatives not allowed -5 -12 -4")]
        [TestCase("1,9,-20,10", "negatives not allowed -20")]
        public void Add_GivenInputWithNegativeNumbers_ShouldReturnNegativesNotAllowed(string input, string expectedMessage)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var ex = Assert.Throws<Exception>(() => sut.Add(input));
            //Assert
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase("//;\n1001;9", 9)]
        public void Add_GivenInputGreaterThan1000_ShouldLessThan100inputOnly(string input, int expectedResult)
        {
            //
            var sut = CreateCalculator();
            //
            var actual = sut.Add(input);
            //
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//;\n1;2", 3)]
        public void Play_GivenInputWithDifferentDelimitersWithAnyLength_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//***\n1***2***3", 6)]
        [TestCase("//*%\n1*2%3", 6)]
        [TestCase("//*%\n4*1%5", 10)]
        public void Play_GivenInputWithDifferentDelimitersWithStarSigns_ShouldReturnTheirSum(string input, int expectedResult)
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
}
