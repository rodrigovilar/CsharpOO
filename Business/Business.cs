using System;
using System.Collections.Generic;

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

    public class Logger
    {
        private List<Log> logs = new List<Log>();

        public void Debug(string message)
        {
            logs.Add(new Log("Debug", message));
        }

        public string Show()
        {
            string result = "";
            foreach (Log log in logs)
            {
                result = result + log.Type + ": " + log.Message + "\n";                
            }
            return result;
        }
    }

    public class Log
    {
        public string Type { get; }
        public string Message { get; }

        public Log (string type, string message) 
        {
            this.Type = type;
            this.Message = message;
        }
    }
}
