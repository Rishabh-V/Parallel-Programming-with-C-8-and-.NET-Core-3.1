namespace CalculatorUnitTest
{
    using Calculator;
    using System;
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

        [Fact]
        public void TestDivideAsyncUsingResult()
        {
            var mathClass = new MathClass();
            var result = mathClass.DivideAsync(6, 2).GetAwaiter().GetResult();
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task TestDivideByZeroException()
        {
            var mathClass = new MathClass();
            var result = mathClass.DivideAsync(6, 0);
            await Assert.ThrowsAsync<DivideByZeroException>(async () => await result);
        }

        [Fact]
        public async Task TestDivideByGenericException()
        {
            var mathClass = new MathClass();
            var result = mathClass.DivideAsync(6, null);
            await Assert.ThrowsAnyAsync<Exception>(async () => await result);
        }

    }
}
