using System;
using Xunit;
using Business;
using Support;

namespace Business.Test
{
    public class BancoTest
    {
        private Banco _banco;
        private Banco _banco2;

        public BancoTest()
        {
            _banco = new Banco();
            _banco2 = new Banco()
            {
                Nome = "Caixa",
                Numero = 104,
                Faturamento = 1000000000.0
            };
        }

        [Fact]
        public void ToStringBanco()
        {
            Assert.Equal("(0)", _banco.ToString());
            Assert.Equal("Caixa(104)", _banco2.ToString());
        }
    }

    public class ContaBancariaTest
    {

        private Banco _banco;
        private Banco _banco2;

        private ContaBancaria _contaBancaria;
        private ContaBancaria _contaBancaria2;
        
        public ContaBancariaTest()
        {
            _banco = new Banco()
            {
                Nome = "BB",
                Numero = 1,
                Faturamento = 5000000000.0
            };
            _banco2 = new Banco()
            {
                Nome = "Caixa",
                Numero = 104,
                Faturamento = 1000000000.0
            };

            _contaBancaria = new ContaBancaria();
            _contaBancaria2 = new ContaBancaria()
            {
                Banco = _banco2,
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
            _contaBancaria.Banco = _banco;
            _contaBancaria.Agencia = 1000;
            _contaBancaria.Numero = 123456;
            _contaBancaria.Tipo = TipoConta.Poupanca;

            verificarContaBancaria(_banco, 1000, 123456, TipoConta.Poupanca, _contaBancaria);

            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Banco definido para BB(1)\n", _contaBancaria.Logger.Show());
        }

        [Fact]
        public void ConstrutorComArgumentos() 
        {
            verificarContaBancaria(_banco2, 2000, 1111111, TipoConta.Corrente, _contaBancaria2);
            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Banco definido para Caixa(104)\n", _contaBancaria2.Logger.Show());
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
            Assert.Throws<BusinessException>(() => _contaBancaria2.Sacar(50.00));
            // TODO formatar moeda
            Assert.Equal("Error: Saldo insuficiente (R$ 0) para sacar R$ 50\n", 
                _contaBancaria2.Logger.Show(LogType.Error));
        }

        [Fact]
        public void SaqueSaldoInsuficiente() 
        {
            _contaBancaria2.Depositar(100.00);
            Assert.Throws<BusinessException>(() => _contaBancaria2.Sacar(200.00));
            Assert.Equal("Error: Saldo insuficiente (R$ 100) para sacar R$ 200\n", 
                _contaBancaria2.Logger.Show(LogType.Error));
        }
        
        [Fact]
        public void ToStringConta() 
        {
            Assert.Equal("  0/0", _contaBancaria.ToString());
            Assert.Equal("Corrente Caixa(104) 2000/1111111", _contaBancaria2.ToString());
        }

        private void verificarContaBancaria(Banco bancoEsperado, int agenciaEsperada, int numeroEsperado, 
            TipoConta? tipoEsperado, ContaBancaria contaBancariaTestada)
        {
            Assert.Equal(bancoEsperado, contaBancariaTestada.Banco);
            Assert.Equal(agenciaEsperada, contaBancariaTestada.Agencia);
            Assert.Equal(numeroEsperado, contaBancariaTestada.Numero);
            Assert.Equal(tipoEsperado, contaBancariaTestada.Tipo);
        }
    }

}
