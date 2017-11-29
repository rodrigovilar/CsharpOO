using System.Collections.Generic;
using System.Linq;

namespace Support
{

    public enum LogType { Debug, Error };

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

    public delegate void TecladoPlugado(bool plugado);

    public class Teclado
    {

        public TecladoPlugado Handlers { get; set; }

        public void AvisarTecladoPlugado() {
            Handlers(true);
        }
    }
}