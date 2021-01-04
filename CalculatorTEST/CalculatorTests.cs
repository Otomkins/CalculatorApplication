using NUnit.Framework;
using Calculator;
using System;

namespace CalculatorTEST
{
    public class Tests
    {
        [TestCase(5, 10, "15")]
        [TestCase(-20, 11, "-9")]
        [TestCase(-100, -33, "-133")]
        public void AddTEST(double a, double b, string expected)
        {
            var result = CalculatorMethods.Add(a, b);
            Assert.AreEqual(expected, result);

        }


        [TestCase(40, 28, "12")]
        [TestCase(10, 20, "-10")]
        [TestCase(-30, -15, "-15")]
        public void SubtractTEST(double a, double b, string expected)
        {
            var result = CalculatorMethods.Subtract(a, b);
            Assert.AreEqual(expected, result);
        }


        [TestCase(6, 6, "36")]
        [TestCase(13, 0, "0")]
        [TestCase(-5, -5, "25")]
        public void MultiplyTEST(double a, double b, string expected)
        {
            var result = CalculatorMethods.Multiply(a, b);
            Assert.AreEqual(expected, result);
        }


        [TestCase(50, 5, "10")]
        [TestCase(-18, 3, "-6")]
        [TestCase(-9, -3, "3")]
        public void DivideTEST(double a, double b, string expected)
        {
            var result = CalculatorMethods.Divide(a, b);
            Assert.AreEqual(expected, result);
        }


        [TestCase(1, 0)]
        [TestCase(0, 0)]
        [Test]
        public void DivideByZeroTEST(double a, double b)
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                CalculatorMethods.Divide(a, b);
            });
        }

        //[TestCase(1, 0)]
        //[TestCase(0, 0)]
        //[Test]
        //public void DivideByZeroTEST2(double a, double b)
        //{
        //    var ex = Assert.Throws<DivideByZeroException>(() => CalculatorMethods.Divide(a, b));
        //    Assert.AreEqual("", ex.Message);
        //}
    }
}