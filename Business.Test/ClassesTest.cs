using Xunit;
using Business;

namespace Business.Test
{
    public class ContaBancariaTest
    {

        [Fact]
        public void Construtor() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria();
            Assert.NotNull(_contaBancaria);

            Assert.Equal(null, _contaBancaria.Banco);
            Assert.Equal(0, _contaBancaria.Agencia);
            Assert.Equal(0, _contaBancaria.Numero);
            Assert.Equal(null, _contaBancaria.Tipo);

            Assert.Equal("Debug: Conta bancária criada\n", _contaBancaria.Logger.Show());
        }

        [Fact]
        public void Propriedades() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria();

            _contaBancaria.Banco = "BB";
            _contaBancaria.Agencia = 1000;
            _contaBancaria.Numero = 123456;
            _contaBancaria.Tipo = TipoConta.Poupanca;

            Assert.Equal("BB", _contaBancaria.Banco);
            Assert.Equal(1000, _contaBancaria.Agencia);
            Assert.Equal(123456, _contaBancaria.Numero);
            Assert.Equal(TipoConta.Poupanca, _contaBancaria.Tipo);

            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Banco definido para BB\n", _contaBancaria.Logger.Show());
        }

        [Fact]
        public void ConstrutorComArgumentos() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria()
            {
                Banco = "Caixa",
                Agencia = 2000,
                Numero = 1111111,
                Tipo = TipoConta.Corrente
            };

            Assert.Equal("Caixa", _contaBancaria.Banco);
            Assert.Equal(2000, _contaBancaria.Agencia);
            Assert.Equal(1111111, _contaBancaria.Numero);
            Assert.Equal(TipoConta.Corrente, _contaBancaria.Tipo);

            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Banco definido para Caixa\n", _contaBancaria.Logger.Show());
        }
    
        [Fact]
        public void MetodosPublicos() 
        {
            ContaBancaria _contaBancaria = new ContaBancaria()
            {
                Banco = "Caixa",
                Agencia = 2000,
                Numero = 1111111,
                Tipo = TipoConta.Corrente
            };

            double saldo = _contaBancaria.Depositar(100.00);
            Assert.Equal(100.00, saldo);

            saldo = _contaBancaria.Depositar(500.00);
            Assert.Equal(600.00, saldo);

            saldo = _contaBancaria.Sacar(50.00);
            Assert.Equal(550.00, saldo);
        }

    }

    public class LoggerTest
    {

        [Fact]
        public void WithoutLogs() 
        {
            Logger _logger = new Logger();
            Assert.Equal("", _logger.Show());
        }

        [Fact]
        public void OneLog() 
        {
            Logger _logger = new Logger();
            _logger.Debug("Message 1");
            Assert.Equal("Debug: Message 1\n", _logger.Show());
        }

        [Fact]
        public void SeveralLogs() 
        {
            Logger _logger = new Logger();
            _logger.Debug("Message 1");
            _logger.Debug("Message 2");
            _logger.Debug("Message 3");
            Assert.Equal("Debug: Message 1\nDebug: Message 2\nDebug: Message 3\n", _logger.Show());
        }
    }
}
