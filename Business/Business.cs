using System;
using System.Collections.Generic;
using System.Linq;

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

    public enum LogType { Debug, Error };

    public class Logger
    {
        private List<Log> logs = new List<Log>();

        public void Debug(string message)
        {
            AddLog(LogType.Debug, message);
        }

        public void Error(string message)
        {
            AddLog(LogType.Error, message);
        }

        private void AddLog(LogType type, string message) 
        {
            logs.Add(new Log(type, message));
        }

        public string Show()
        {
            return MountShow(this.logs);
        }

        public string Show(LogType type)
        {
            var subset = from log in this.logs
                 where log.Type == type
                 select log;

            return MountShow(subset);
        }

        private string MountShow(IEnumerable<Log> logsToShow)
        {
            string result = "";
            foreach (Log log in logsToShow)
            {
                result = result + log.Type + ": " + log.Message + "\n";                
            }
            return result;
        }

    }

    public class Log
    {
        public LogType Type { get; }
        public string Message { get; }

        public Log (LogType type, string message) 
        {
            this.Type = type;
            this.Message = message;
        }
    }
}
