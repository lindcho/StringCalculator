﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StringCalculatorAugust8.Tests
{
    [TestFixture]
    public class CalculatorTestsV2
    {

        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void Add_GivenEmptyInputAsString_ShouldReturnZero(string input, int expectedResult)
        {
            var sut = new CalculatorModel();

            var actual = sut.Add(input);
            Assert.That(actual, Is.EqualTo(expectedResult));
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

        [TestCase("2,3,-3,-2,1", "negatives not allowed -3 -2")]
        [TestCase("-1,-3,-2,-7", "negatives not allowed -1 -3 -2 -7")]
        public void Add_GivenInputWithNegativeNumbers_ShouldThrowException(string input, string expectedResult)
        {
            var sut = new CalculatorModel();

            var actual = Assert.Throws<ArgumentException>(() => sut.Add(input));
            Assert.That(actual.Message, Is.EqualTo(expectedResult));
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[*][%]\n1*2%3", 6)]
        public void Play_GivenInputWithDifferentDelimitersWithAnyLength_ShouldReturnSum(string input, int expectedResult)
        {
            //Arrange
            var sut = new CalculatorModel();
            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }
    }

    public class CalculatorModel
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            var stringNumberArray = input.Replace('\n', ',').Split(',');

            stringNumberArray = GetCustomDelimiter(stringNumberArray);
            ValidateNumbersArePositive(stringNumberArray);
            return stringNumberArray.Sum(x => int.Parse(x));
        }

        public static void ValidateNumbersArePositive(string[] numberArray)
        {
            if (!numberArray.Any(x => int.Parse(x) < 0)) return;
            throw new ArgumentException($"negatives not allowed {string.Join(" ", numberArray.Where(x => int.Parse(x) < 0))}");
        }

        private static string[] GetCustomDelimiter(string[] numberArray)
        {
            if (!numberArray[0].StartsWith("//")) return numberArray;

            numberArray = numberArray[1].Replace(';', ',').Split(',');

            return numberArray;
        }
    }
}
