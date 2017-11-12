using Xunit;
using Business;

namespace Business.Test
{
    public class ClassesTest
    {
        private readonly ContaBancaria _contaBancaria;

        public ClassesTest()
        {
            _contaBancaria = new ContaBancaria();
        }

        [Fact]
        public void Constructor() 
        {
            Assert.NotNull(_contaBancaria);
        }

    }
}
