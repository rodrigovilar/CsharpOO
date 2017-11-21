using Xunit;
using Business;

namespace Business.Test
{
    public class ContaBancariaTest
    {

        private ContaBancaria _contaBancaria;
        private ContaBancaria _contaBancaria2;

        public ContaBancariaTest() 
        {
            _contaBancaria = new ContaBancaria();
            _contaBancaria2 = new ContaBancaria()
            {
                Banco = "Caixa",
                Agencia = 2000,
                Numero = 1111111,
                Tipo = TipoConta.Corrente
            };
        }

        [Fact]
        public void Construtor() 
        {
            Assert.NotNull(_contaBancaria);
            verificarContaBancaria(null, 0, 0, null, _contaBancaria);
            Assert.Equal("Debug: Conta bancária criada\n", _contaBancaria.Logger.Show());
        }

        [Fact]
        public void Propriedades() 
        {
            _contaBancaria.Banco = "BB";
            _contaBancaria.Agencia = 1000;
            _contaBancaria.Numero = 123456;
            _contaBancaria.Tipo = TipoConta.Poupanca;

            verificarContaBancaria("BB", 1000, 123456, TipoConta.Poupanca, _contaBancaria);

            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Banco definido para BB\n", _contaBancaria.Logger.Show());
        }

        [Fact]
        public void ConstrutorComArgumentos() 
        {
            verificarContaBancaria("Caixa", 2000, 1111111, TipoConta.Corrente, _contaBancaria2);
            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Banco definido para Caixa\n", _contaBancaria2.Logger.Show());
        }
    
        [Fact]
        public void MetodosPublicos() 
        {
            double saldo = _contaBancaria2.Depositar(100.00);
            Assert.Equal(100.00, saldo);

            saldo = _contaBancaria2.Depositar(500.00);
            Assert.Equal(600.00, saldo);

            saldo = _contaBancaria2.Sacar(50.00);
            Assert.Equal(550.00, saldo);
        }

        private void verificarContaBancaria(string bancoEsperado, int agenciaEsperada, int numeroEsperado, 
            TipoConta? tipoEsperado, ContaBancaria contaBancariaTestada)
        {
            Assert.Equal(bancoEsperado, contaBancariaTestada.Banco);
            Assert.Equal(agenciaEsperada, contaBancariaTestada.Agencia);
            Assert.Equal(numeroEsperado, contaBancariaTestada.Numero);
            Assert.Equal(tipoEsperado, contaBancariaTestada.Tipo);
        }
    }

    public class LoggerTest
    {
        private Logger _logger;

        public LoggerTest() 
        {
            _logger = new Logger();
        }

        [Fact]
        public void WithoutLogs() 
        {
            Assert.Equal("", _logger.Show());
        }

        [Fact]
        public void OneLog() 
        {
            _logger.Debug("Message 1");
            Assert.Equal("Debug: Message 1\n", _logger.Show());
        }

        [Fact]
        public void SeveralLogs() 
        {
            _logger.Debug("Message 1");
            _logger.Debug("Message 2");
            _logger.Debug("Message 3");
            Assert.Equal("Debug: Message 1\nDebug: Message 2\nDebug: Message 3\n", _logger.Show());
        }

        [Fact]
        public void SeveralLogTypes() 
        {
            _logger.Debug("Message 1");
            _logger.Error("Message 2");
            _logger.Debug("Message 3");
            Assert.Equal("Debug: Message 1\nError: Message 2\nDebug: Message 3\n", _logger.Show());
            Assert.Equal("Error: Message 2\n", _logger.Show(LogType.Error));
            Assert.Equal("Debug: Message 1\nDebug: Message 3\n", _logger.Show(LogType.Debug));
        }
    }
}
