using System;
using Support;

namespace Business
{
    [System.Serializable]
    public class BusinessException : System.Exception
    {
        public BusinessException() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, System.Exception inner) : base(message, inner) { }
        protected BusinessException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
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
            if (saldo < valor)
            {
                this.Logger.Error($"Saldo insuficiente (R$ {saldo}) para sacar R$ {valor}"); 
                throw new BusinessException("Saldo menor que valor a sacar");
            }

            saldo -= valor;
            return saldo;
        }

    }
}
