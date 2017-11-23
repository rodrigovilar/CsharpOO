﻿using System;
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

    public class Agencia
    {
        public Banco Banco { get; set; }
        public int Numero { get; set; } = 0;
        public int DigitoVerificador { get; set; } = 0;

        public override string ToString()
        {
            return $"{Banco} {Numero}-{DigitoVerificador}";
        }

        public override bool Equals(object obj)
        {
            // Elimina nulos e tipos diferentes
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Agencia agencia = obj as Agencia; // Cast

            // Comparacao de atributos
            if (agencia.Banco == null) 
            {
                if (this.Banco != null) return false;
            } else {
                if (!this.Banco.Equals(agencia.Banco)) return false;
            }

            if (this.Numero != agencia.Numero) return false;

            if (this.DigitoVerificador != agencia.DigitoVerificador) return false;

            return true;
        }
        
        public override int GetHashCode()
        {
            return (this.Numero + (this.DigitoVerificador * 1024)) * 8 + this.Banco.GetHashCode();
        }
    }

    public enum TipoConta { Poupanca, Corrente, Investimento };

    public class ContaBancaria
    {
        private Agencia agencia = null;

        public Agencia Agencia 
        { 
            get { return this.agencia; }
            set 
            { 
                this.agencia = value;
                this.Logger.Debug($"Agencia definida para {agencia}"); 
            }
        }

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
            return $"{this.Tipo} {this.Agencia}/{this.Numero}";
        }
    }
}
