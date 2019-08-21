using System;
using NUnit.Framework;

namespace StringCalculatorAugust7.Tests
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void Add_GivenInputWithEmptyString_ShouldReturnZero()
        {
            var sut = new Calculator();
            var input = "";
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(0));
        }

        [TestCase("1", 1)]
        [TestCase("3", 3)]
        [TestCase("6", 6)]
        [TestCase("10", 10)]
        public void Add_GivenInputWithOneDigit_ShouldReturnThatDigit(string input, int expectedResult)
        {
            //Arrange
            var sut = new Calculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1,3", 4)]
        [TestCase("3,2", 5)]
        [TestCase("6,6", 12)]
        [TestCase("6,6,10,15", 37)]
        public void Add_GivenInputWithComma_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = new Calculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("3\n4,5", 12)]
        [TestCase("5\n5,4", 14)]
        public void Add_GivenInputWithNewLines_ShouldHandleNewlinesAndReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = new Calculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//;\n1;8", 9)]
        [TestCase("//;\n4;3", 7)]
        [TestCase("//;\n7;5", 12)]
        public void Add_GivenInputWithDelimiters_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = new Calculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        public void Play_GivenInputWithDifferentDelimitersWithAnyLength_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = new Calculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//*%\n1*2%3", 6)]
        [TestCase("//*%\n4*1%5", 10)]
        public void Play_GivenInputWithDifferentDelimitersWithStarSigns_ShouldReturnTheirSum(string input, int expectedResult)
        {
            //Arrange
            var sut = new Calculator();
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
            //
            var sut = new Calculator();
            //
            var ex = Assert.Throws<Exception>(() => sut.Add(input));
            //
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }
    }
}
