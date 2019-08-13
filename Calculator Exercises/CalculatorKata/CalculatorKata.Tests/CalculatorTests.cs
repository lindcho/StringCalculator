﻿using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

namespace CalculatorKata.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("",0)]
        [TestCase(null,0)]
        public void Add_GivenEmptyString_ShouldReturnZero(string input,int expectedResult)
        {
            var sut=new Calculator();

            var actual = sut.Add(input);
            Assert.That(actual,Is.EqualTo(0));
        }

        [TestCase("1",1)]
        [TestCase("20",20)]
        [TestCase("143",143)]
        public void Add_GivenValidNumber_ShouldReturnSameNumber(string input,int expectedResult)
        {
            var sut=new Calculator();

            var actual = sut.Add(input);
            Assert.That(actual,Is.EqualTo(expectedResult));
        }

        [TestCase("6,9",15)]
        [TestCase("120,56",176)]
        [TestCase("30,76",106)]
        [TestCase("30,76,2,65,2,5",180)]
        public void Add_GivenTwoCommaSeparatedInput_ShouldReturnInputSum(string input,int expectedResult)
        {
            var sut=new Calculator();

            var actual = sut.Add(input);
            Assert.That(actual,Is.EqualTo(expectedResult));
        }

        [TestCase("1\n2", 3)]
        [TestCase("1\n14", 15)]
        [TestCase("1\n1,4", 6)]
        [TestCase("1\n5,4", 10)]
        public void Add_GivenNewLineDelimiterBetweenInput_ShouldReturnInputSum(string input,int expectedResult)
        {
            var sut=new Calculator();

            var actual = sut.Add(input);
            Assert.That(actual,Is.EqualTo(expectedResult));
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
            
            return inputArray.Sum(x=>int.Parse(x));
        }
    }
}
