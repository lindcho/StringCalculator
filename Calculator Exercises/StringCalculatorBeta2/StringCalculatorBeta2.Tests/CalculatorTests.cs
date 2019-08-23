using System;
using NUnit.Framework;

namespace StringCalculatorBeta2.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private static Calculator CreateCalculator()
        {
            return new Calculator();
        }

        [Test]
        public void Add_GivenInputWithEmptyString_ShouldReturnZero()
        {
            // Arrange
            var sut = CreateCalculator();
            var input = "";
            var expected = 0;
            // Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual,Is.EqualTo(expected));

        }

        [Test]
        public void Add_GivenInputWithOneNumber_ShouldReturnThatNumber()
        {
            // Arrange
            var sut = CreateCalculator();
            var input = "10";
            var expected = 10;
            // Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual,Is.EqualTo(expected));

        }

        [Test]
        public void Add_GivenInputWithTwoNumbers_ShouldReturnSum()
        {
            // Arrange
            var sut = CreateCalculator();
            var input = "6,7";
            var expected = 13;
            // Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual,Is.EqualTo(expected));

        }

        //[Test]
        //public void Add_GivenInputWithEmptyString_ShouldReturnZero()
        //{
        //    var sut = CreateCalculator();
        //    var input = "";
        //    //Act
        //    var actual = sut.Add(input);
        //    //Assert
        //    Assert.That(actual, Is.EqualTo(0));
        //}

        [Test]
        public void Add_GivenInputWithNull_ShouldReturnZero()
        {
            var sut = CreateCalculator();
            string input = null;
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
            var sut = CreateCalculator();
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
            var sut = CreateCalculator();
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
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("//;\n1;8", 9)]
        [TestCase("//;\n4;3", 7)]
        [TestCase("//;\n7;5", 12)]
        public void Add_GivenInputWithNewLineDelimitersAndComma_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        //TODO Re-implement the second testCase to accomodate that logic 
        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[****][%%%%@]\n2****1%%%%@8", 11)]
        public void Add_GivenInputWithDifferentDelimitersWithAnyLength_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("1,12,-3,-4", "Negatives not allowed. (-3,-4)")]
        [TestCase("8,-5,-12,-4", "Negatives not allowed. (-5,-12,-4)")]
        [TestCase("1,9,-20,10", "Negatives not allowed. (-20)")]
        public void Add_GivenInputWithNegativeNumbers_ShouldReturnNegativesNotAllowed(string input, string expectedMessage)
        {
            //Arrange
            var sut = CreateCalculator();
            //
            var ex = Assert.Throws<Exception>(() => sut.Add(input));
            //
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase("//;\n2;1001", 2)]
        [TestCase("//;\n3;1020;5", 8)]
        [TestCase("//;\n2;1000;9", 1011)]
        public void Add_GivenInputWithNumberGreaterThan1000_ShouldIgnoreThoseNumber(string input, int expectedResult)
        {
            //Arrange
            var sut = CreateCalculator();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

    }
}
