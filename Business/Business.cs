using System;
using System.Collections.Generic;

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
