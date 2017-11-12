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

        [Fact]
        public void Properties() 
        {
            _contaBancaria.banco = "BB";
            _contaBancaria.agencia = 1000;
            _contaBancaria.numero = 123456;
            _contaBancaria.tipo = TipoConta.Poupanca;

            Assert.Equal(_contaBancaria.banco, "BB");
            Assert.Equal(_contaBancaria.agencia, 1000);
            Assert.Equal(_contaBancaria.numero, 123456);
            Assert.Equal(_contaBancaria.tipo, TipoConta.Poupanca);
        }
    
    }
}
