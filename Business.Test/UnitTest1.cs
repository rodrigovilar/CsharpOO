using Xunit;
using Business;

namespace Business.Test
{
    public class ClassesTest
    {

        [Fact]
        public void Construtor() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria();
            Assert.NotNull(_contaBancaria);

            Assert.Equal(_contaBancaria.banco, null);
            Assert.Equal(_contaBancaria.agencia, 0);
            Assert.Equal(_contaBancaria.numero, 0);
            Assert.Equal(_contaBancaria.tipo, null);
        }

        [Fact]
        public void Propriedades() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria();

            _contaBancaria.banco = "BB";
            _contaBancaria.agencia = 1000;
            _contaBancaria.numero = 123456;
            _contaBancaria.tipo = TipoConta.Poupanca;

            Assert.Equal(_contaBancaria.banco, "BB");
            Assert.Equal(_contaBancaria.agencia, 1000);
            Assert.Equal(_contaBancaria.numero, 123456);
            Assert.Equal(_contaBancaria.tipo, TipoConta.Poupanca);
        }

        [Fact]
        public void ConstrutorComArgumentos() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria("Caixa", 2000, 1111111, TipoConta.Corrente);

            Assert.Equal(_contaBancaria.banco, "Caixa");
            Assert.Equal(_contaBancaria.agencia, 2000);
            Assert.Equal(_contaBancaria.numero, 1111111);
            Assert.Equal(_contaBancaria.tipo, TipoConta.Corrente);
        }
    
    }
}
