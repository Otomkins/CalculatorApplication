using NUnit.Framework;
using Calculator_Code;
using System;

namespace Calculator_Tests
{
    public class Tests
    {
        readonly CalculatorService _cs = new CalculatorService(); // Used to test methods within 'CalculatorService'

        // ADDITION TESTS
        [Category("Addition Tests")]
        [Test]
        public void WhenAddingTwoNumbers_ReturnsResult()
        {
            Assert.That(_cs.AddNumbers(5, 10), Is.EqualTo(15));
        }

        [Category("Addition Tests")]
        [Test]
        public void WhenAddingTwoNegativeNumbers_ReturnsResult()
        {
            Assert.That(_cs.AddNumbers(-20, -13), Is.EqualTo(-33));
        }

        [Category("Addition Tests")]
        [Test]
        public void WhenAddingPositiveAndNegativeNumber_ReturnsResult()
        {
            Assert.That(_cs.AddNumbers(11, -20), Is.EqualTo(-9));
        }

        // SUBTRACTION TESTS
        [Category("Subtraction Tests")]
        [Test]
        public void WhenSubtractingTwoNumbers_ReturnsResult()
        {
            Assert.That(_cs.SubtractNumbers(40, 28), Is.EqualTo(12));
        }

        [Category("Subtraction Tests")]
        [Test]
        public void WhenSubtractingTwoNegativeNumbers_ReturnsResult()
        {
            Assert.That(_cs.SubtractNumbers(-40, -17), Is.EqualTo(-23));
        }

        [Category("Subtraction Tests")]
        [Test]
        public void WhenSubtractingPositiveAndNegativeNumber_ReturnsResult()
        {
            Assert.That(_cs.SubtractNumbers(22, -9), Is.EqualTo(31));
        }

        // MULTIPLICATION TESTS
        [Category("Multiplication Tests")]
        [Test]
        public void WhenMultiplyingTwoNumbers_ReturnsResult()
        {
            Assert.That(_cs.MultiplyNumbers(6, 7), Is.EqualTo(42));
        }

        [Category("Multiplication Tests")]
        [Test]
        public void WhenMultiplyingTwoNegativeNumbers_ReturnsResult()
        {
            Assert.That(_cs.MultiplyNumbers(-4, -17), Is.EqualTo(68));
        }

        [Category("Multiplication Tests")]
        [Test]
        public void WhenMultiplyingPositiveAndNegativeNumber_ReturnsResult()
        {
            Assert.That(_cs.MultiplyNumbers(16, -5), Is.EqualTo(-80));
        }

        // DIVISION TESTS
        [Category("Division Tests")]
        [Test]
        public void WhenDividingTwoNumbers_ReturnsResult()
        {
            Assert.That(_cs.DivideNumbers(60, 4), Is.EqualTo(15));
        }

        [Category("Division Tests")]
        [Test]
        public void WhenDividingTwoNegativeNumbers_ReturnsResult()
        {
            Assert.That(_cs.DivideNumbers(-39, -3), Is.EqualTo(13));
        }

        [Category("Division Tests")]
        [Test]
        public void WhenDividingPositiveAndNegativeNumber_ReturnsResult()
        {
            Assert.That(_cs.DivideNumbers(18, -9), Is.EqualTo(-2));
        }

        [Category("Division Tests")]
        [Test]
        public void WhenDividingZeroByNumber_ReturnsZero()
        {
            Assert.That(_cs.DivideNumbers(0, 45), Is.Zero);
        }

        [Category("Division Tests")]
        [Test]
        public void WhenDividingNumberByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => { double? v = _cs.DivideNumbers(10, 0); });
        }

        [Category("Division Tests")]
        [Test]
        public void WhenDividingNumberByZero_ThrowsExceptionWithMessage()
        {
            Assert.That(() => _cs.DivideNumbers(10, 0), Throws.Exception.With.Message.EqualTo("You cannot divide by zero"));
        }
    }
}