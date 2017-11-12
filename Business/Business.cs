using System;

namespace Business
{
    public enum TipoConta { Poupanca, Corrente, Investimento };

    public class ContaBancaria
    {
        public string Banco { get; set; }
        public int Agencia { get; set; }
        public int Numero { get; set; }
        public TipoConta? Tipo { get; set; }

        private double saldo = 0.0;

        public ContaBancaria(string banco, int agencia, int numero, TipoConta? tipo)
        {
            this.Banco = banco;
            this.Agencia = agencia;
            this.Numero = numero;
            this.Tipo = tipo;
        }

        public ContaBancaria() : this(null, 0, 0, null)
        {
        }

        public double Depositar(double valor)
        {
            saldo += valor;
            return saldo;
        }

        public double Sacar(double valor)
        {
            saldo -= valor;
            return saldo;
        }

    }
}
