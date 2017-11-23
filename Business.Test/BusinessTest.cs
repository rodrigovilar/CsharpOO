using System;
using Xunit;
using Business;
using Support;

namespace Business.Test
{
    public class BancoTest
    {

        [Fact]
        public void ToStringBanco()
        {
            Assert.Equal("(0)", BancoTestHelper.BancoVazio.ToString());
            Assert.Equal("Caixa(104)", BancoTestHelper.BancoCaixa.ToString());
        }
    }

    public class AgenciaTest
    {
        [Fact]
        public void ToStringAgencia()
        {
            Assert.Equal(" 0-0", AgenciaTestHelper.AgenciaVazia.ToString());
            Assert.Equal("BB(1) 1234-5", AgenciaTestHelper.Agencia1.ToString());
            Assert.Equal("Caixa(104) 1000-9", AgenciaTestHelper.Agencia2.ToString());
        }
    }

    public class ContaBancariaTest
    {
        [Fact]
        public void Construtor() 
        {
            Assert.NotNull(ContaBancariaTestHelper.ContaBancariaVazia1);
            ContaBancariaTestHelper.verificarContaBancaria(null, 0, null, ContaBancariaTestHelper.ContaBancariaVazia1);
            Assert.Equal("Debug: Conta bancária criada\n", ContaBancariaTestHelper.ContaBancariaVazia1.Logger.Show());
        }

        [Fact]
        public void Propriedades() 
        {
            ContaBancaria _contaBancaria = ContaBancariaTestHelper.ContaBancariaVazia2;
            _contaBancaria.Agencia = AgenciaTestHelper.Agencia1;
            _contaBancaria.Numero = 123456;
            _contaBancaria.Tipo = TipoConta.Poupanca;

            ContaBancariaTestHelper.verificarContaBancaria(AgenciaTestHelper.Agencia1, 123456, TipoConta.Poupanca, 
                _contaBancaria);

            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Agencia definida para BB(1) 1234-5\n", _contaBancaria.Logger.Show());
        }

        [Fact]
        public void ConstrutorComArgumentos() 
        {
            ContaBancariaTestHelper.verificarContaBancaria(AgenciaTestHelper.Agencia2, 1111111, 
                TipoConta.Poupanca, ContaBancariaTestHelper.ContaBancariaCorrente2);
            Assert.Equal("Debug: Conta bancária criada\n" +
                         "Debug: Agencia definida para Caixa(104) 1000-9\n", 
                         ContaBancariaTestHelper.ContaBancariaCorrente2.Logger.Show());
        }
    
        [Fact]
        public void MetodosPublicos() 
        {
            ContaBancaria _contaBancaria = ContaBancariaTestHelper.ContaBancariaCorrente2;
            double saldo = _contaBancaria.Depositar(100.00);
            Assert.Equal(100.00, saldo);

            saldo = _contaBancaria.Depositar(500.00);
            Assert.Equal(600.00, saldo);

            saldo = _contaBancaria.Sacar(50.00);
            Assert.Equal(550.00, saldo);
        }

        [Fact]
        public void SaqueSemSaldo() 
        {
            ContaBancaria _contaBancaria = ContaBancariaTestHelper.ContaBancariaVazia2;
            Assert.Throws<BusinessException>(() => _contaBancaria.Sacar(50.00));
            // TODO formatar moeda
            Assert.Equal("Error: Saldo insuficiente (R$ 0) para sacar R$ 50\n", 
                _contaBancaria.Logger.Show(LogType.Error));
        }

        [Fact]
        public void SaqueSaldoInsuficiente() 
        {
            ContaBancaria _contaBancaria = ContaBancariaTestHelper.ContaBancariaCorrente1;
            _contaBancaria.Depositar(100.00);
            Assert.Throws<BusinessException>(() => _contaBancaria.Sacar(200.00));
            Assert.Equal("Error: Saldo insuficiente (R$ 100) para sacar R$ 200\n", 
                _contaBancaria.Logger.Show(LogType.Error));
        }
        
        [Fact]
        public void ToStringConta() 
        {
            Assert.Equal(" /0", ContaBancariaTestHelper.ContaBancariaVazia1.ToString());
            Assert.Equal("Corrente BB(1) 1234-5/123456", ContaBancariaTestHelper.ContaBancariaCorrente1.ToString());
        }

        [Fact]
        public void RelatorioFinanceiroBanco() 
        {
            ContaBancariaTestHelper.ContaBancariaCorrente_21.Depositar(100); // 100
            ContaBancariaTestHelper.ContaBancariaCorrente_32.Depositar(500);
            ContaBancariaTestHelper.ContaBancariaCorrente_32.Depositar(450); // 950
            ContaBancariaTestHelper.ContaBancariaCorrente_33.Depositar(50);
            ContaBancariaTestHelper.ContaBancariaCorrente_33.Depositar(1000);
            ContaBancariaTestHelper.ContaBancariaCorrente_33.Sacar(300);
            ContaBancariaTestHelper.ContaBancariaCorrente_33.Depositar(500); //1250

            Assert.Equal(
                "Banco Itau(341) - Saldo total: R$ 2300\n" +
                "  Agencia 1 - Saldo parcial: R$ 0\n" +
                "  Agencia 2 - Saldo parcial: R$ 100\n" +
                "    Conta Corrente 21 - Saldo: R$ 100\n" +
                "  Agencia 3 - Saldo parcial: R$ 2200\n" +
                "    Conta Corrente 31 - Saldo: R$ 0\n" +
                "    Conta Poupanca 32 - Saldo: R$ 950\n" +
                "    Conta Corrente 33 - Saldo: R$ 1250\n",
                BancoTestHelper.BancoItau.RelatorioFinanceiro()
            );
        }

        [Fact]
        public void QuantidadeSaquesNegados() 
        {
            Assert.Throws<BusinessException>(() => ContaBancariaTestHelper.ContaBancariaCorrente_21.Sacar(1000));
            Assert.Throws<BusinessException>(() => ContaBancariaTestHelper.ContaBancariaCorrente_32.Sacar(1000));
            Assert.Throws<BusinessException>(() => ContaBancariaTestHelper.ContaBancariaCorrente_33.Sacar(3000));
            Assert.Throws<BusinessException>(() => ContaBancariaTestHelper.ContaBancariaCorrente_33.Sacar(3000));
            Assert.Throws<BusinessException>(() => ContaBancariaTestHelper.ContaBancariaCorrente1.Sacar(5000));
            Assert.Equal(5, ContaBancaria.QuantidadeSaquesNegados());
        }
    }

    public class BancoTestHelper
    {
        public static Banco BancoVazio { get; } = new Banco();
        public static Banco BancoBB { get; } = new Banco()
        {
            Nome = "BB",
            Numero = 1,
            Faturamento = 5000000000.0
        };
        public static Banco BancoCaixa { get; } = new Banco()
        {
            Nome = "Caixa",
            Numero = 104,
            Faturamento = 1000000000.0
        };
        public static Banco BancoItau = new Banco()
            {
                Nome = "Itau",
                Numero = 341
            };

    }

    public class AgenciaTestHelper
    {
        public static Agencia AgenciaVazia { get; } = new Agencia();
        public static Agencia Agencia1 { get; } = new Agencia()
        {
            Banco = BancoTestHelper.BancoBB,
            Numero = 1234,
            DigitoVerificador = 5
        };
        public static Agencia Agencia2 { get; } = new Agencia()
        {
            Banco = BancoTestHelper.BancoCaixa,
            Numero = 1000,
            DigitoVerificador = 9
        };
        public static Agencia Agencia_1 = new Agencia()
        {
            Banco = BancoTestHelper.BancoItau,
            Numero = 1,
            DigitoVerificador = 1
        };
        public static Agencia Agencia_2 = new Agencia()
        {
            Banco = BancoTestHelper.BancoItau,
            Numero = 2,
            DigitoVerificador = 2
        };
        public static Agencia Agencia_3 = new Agencia()
        {
            Banco = BancoTestHelper.BancoItau,
            Numero = 3,
            DigitoVerificador = 3
        };
    }

    public class ContaBancariaTestHelper
    {
        public static ContaBancaria ContaBancariaVazia1 { get; } = new ContaBancaria();
        public static ContaBancaria ContaBancariaVazia2 { get; } = new ContaBancaria();
        public static ContaBancaria ContaBancariaCorrente1 { get; } = new ContaBancaria()
        {
            Agencia = AgenciaTestHelper.Agencia1,
            Numero = 123456,
            Tipo = TipoConta.Corrente
        };
        public static ContaBancaria ContaBancariaCorrente2 { get; } = new ContaBancaria()
        {
            Agencia = AgenciaTestHelper.Agencia2,
            Numero = 1111111,
            Tipo = TipoConta.Poupanca
        };
        public static ContaBancaria ContaBancariaCorrente_21 = new ContaBancaria()
        {
            Agencia = AgenciaTestHelper.Agencia_2,
            Numero = 21,
            Tipo = TipoConta.Corrente
        };
        public static ContaBancaria ContaBancariaCorrente_31 = new ContaBancaria()
        {
            Agencia = AgenciaTestHelper.Agencia_3,
            Numero = 31,
            Tipo = TipoConta.Corrente
        };
        public static ContaBancaria ContaBancariaCorrente_32 = new ContaBancaria()
        {
            Agencia = AgenciaTestHelper.Agencia_3,
            Numero = 32,
            Tipo = TipoConta.Poupanca
        };
        public static ContaBancaria ContaBancariaCorrente_33 = new ContaBancaria()
        {
            Agencia = AgenciaTestHelper.Agencia_3,
            Numero = 33,
            Tipo = TipoConta.Corrente
        };

        public static void verificarContaBancaria(Agencia agenciaEsperada, int numeroEsperado, 
            TipoConta? tipoEsperado, ContaBancaria contaBancariaTestada)
        {
            Assert.Equal(agenciaEsperada, contaBancariaTestada.Agencia);
            Assert.Equal(numeroEsperado, contaBancariaTestada.Numero);
            Assert.Equal(tipoEsperado, contaBancariaTestada.Tipo);
        }
    }
}
