using NUnit.Framework;

namespace Testing.Tools
{
    public class ExampleTest : GivenA<Calculator, int>
    {
        private int _number1;
        private int _number2;

        protected override void Given()
        {
            base.Given();

            _number1 = 10;
            _number2 = 20;

            GetMock<ITheCalculatorThatLikesToCalculate>().Setup(m => m.Add(_number1, _number2)).Returns(30);
        }

        protected override void When()
        {
            base.When();
            Result = Target.Add(_number1, _number2);
        }

        [Then]
        public void TheResultIs30()
        {
            Assert.AreEqual(30, Result);
        }

        [Then]
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