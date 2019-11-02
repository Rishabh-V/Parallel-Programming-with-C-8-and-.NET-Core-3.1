namespace CalculatorUnitTest
{
    using Calculator;
    using System.Threading.Tasks;
    using Xunit;
    public class MathClassUnitTest
    {
        [Fact]
        public void TestDivide()
        {
            var mathClass = new MathClass();
            int output = 3; //mock data
            var result = mathClass.Divide(6, 2);
            Assert.Equal(output, result);
        }

        [Fact]
        public async Task TestDivideAsync()
        {
            var mathClass = new MathClass();
            var result = await mathClass.DivideAsync(6, 2);
            Assert.Equal(3, result);
        }

    }
}
