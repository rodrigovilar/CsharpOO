using System;
using Xunit;
using Business;
using Support;

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

        [Fact]
        public void SaqueSemSaldo() 
        {
            Assert.Throws<Exception>(() => _contaBancaria2.Sacar(50.00));
            // TODO formatar moeda
            Assert.Equal("Error: Saldo insuficiente (R$ 0) para sacar R$ 50\n", 
                _contaBancaria2.Logger.Show(LogType.Error));
        }

        [Fact]
        public void SaqueSaldoInsuficiente() 
        {
            _contaBancaria2.Depositar(100.00);
            Assert.Throws<Exception>(() => _contaBancaria2.Sacar(200.00));
            Assert.Equal("Error: Saldo insuficiente (R$ 100) para sacar R$ 200\n", 
                _contaBancaria2.Logger.Show(LogType.Error));
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

}
