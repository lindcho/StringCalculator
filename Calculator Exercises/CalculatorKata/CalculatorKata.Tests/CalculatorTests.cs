using NUnit.Framework;
using NUnit.Framework.Constraints;

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
    }

    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            return int.Parse(input);
        }
    }
}
