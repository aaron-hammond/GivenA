using NUnit.Framework;

namespace Testing.Tools
{
    public class ExampleTest : With<Calculator, int>
    {
        private int _number1;
        private int _number2;

        protected override void Arrange()
        {
            base.Arrange();

            _number1 = 10;
            _number2 = 20;

            GetMock<ITheCalculatorThatLikesToCalculate>().Setup(m => m.Add(_number1, _number2)).Returns(30);
        }

        protected override void Act()
        {
            base.Act();
            Result = Target.Add(_number1, _number2);
        }

        [Assertion]
        public void TheResultIs30()
        {
            Assert.AreEqual(30, Result);
        }

        [Assertion]
        public void TheCalculatorThatLikesToCalculateIsCalled()
        {
            Verify<ITheCalculatorThatLikesToCalculate>(m => m.Add(_number1, _number2));
        }
    }

    public interface ITheCalculatorThatLikesToCalculate
    {
        int Add(int number1, int number2);
    }

    public class Calculator
    {
        private readonly ITheCalculatorThatLikesToCalculate _happyCalc;

        public Calculator(ITheCalculatorThatLikesToCalculate happyCalc)
        {
            _happyCalc = happyCalc;
        }

        public int Add(int number1, int number2)
        {
            return _happyCalc.Add(number1, number2);
        }
    }
}