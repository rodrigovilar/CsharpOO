using System;
using Support;

namespace Business
{
    public enum TipoConta { Poupanca, Corrente, Investimento };

    public class ContaBancaria
    {
        private string banco = null;

        public string Banco 
        { 
            get { return this.banco; }
            set 
            { 
                this.banco = value;
                this.Logger.Debug($"Banco definido para {value}"); 
            }
        }

        public int Agencia { get; set; } = 0;
        public int Numero { get; set; } = 0;
        public TipoConta? Tipo { get; set; } = null;
        public Logger Logger { get; } = new Logger();

        private double saldo = 0.0;

        public ContaBancaria() 
        {
            this.Logger.Debug($"Conta bancária criada");
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
