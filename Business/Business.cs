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
    
    public class Banco
    {
        public string Nome { get; set; } = "";
        public int Numero { get; set; } = 0;
        public double Faturamento { get; set; } = 0.0;

        public override string ToString()
        {
            return $"{Nome}({Numero})";
        }

        public override bool Equals(object obj)
        {
            // Elimina nulos e tipos diferentes
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Banco banco = obj as Banco; // Cast

            // Comparacao de atributos
            if (banco.Nome == null) 
            {
                if (this.Nome != null) return false;
            } else {
                if (this.Nome != banco.Nome) return false;
            }

            if (this.Numero != banco.Numero) return false;

            return true;
        }
        
        public override int GetHashCode()
        {
            string nome = (this.Nome == null) ? "" : this.Nome;
            return this.Nome.GetHashCode() * (this.Numero + 1024);
        }
    }

    public enum TipoConta { Poupanca, Corrente, Investimento };

    public class ContaBancaria
    {
        private Banco banco = null;

        public Banco Banco 
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

        public override string ToString() 
        {
            return $"{this.Tipo} {this.banco} {this.Agencia}/{this.Numero}";
        }
    }
}
