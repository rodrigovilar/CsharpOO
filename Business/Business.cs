using System;
using System.Collections.Generic;
using System.Text;
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
        private List<Agencia> agencias = new List<Agencia>();

        public string Nome { get; set; } = "";
        public int Numero { get; set; } = 0;
        public double Faturamento { get; set; } = 0.0;
        
        internal void AddAgencia(Agencia agencia)
        {
            agencias.Add(agencia);
        }

        public string RelatorioFinanceiro()
        {
            StringBuilder linhasAgencias = new StringBuilder();
            double saldoTotal = 0;
            foreach (Agencia agencia in agencias)
            {
                double saldoParcial = agencia.RelatorioFinanceiro(linhasAgencias);
                saldoTotal += saldoParcial;
            }

            string linhaBanco = $"Banco {ToString()} - Saldo total: R$ {saldoTotal}\n";

            return linhaBanco + linhasAgencias.ToString();
        }

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
        private List<ContaBancaria> contasBancarias = new List<ContaBancaria>();

        private Banco banco;
        
        public Banco Banco
        { 
            get { return this.banco; }
            set 
            { 
                this.banco = value;
                this.banco.AddAgencia(this);
            }
        }

        public int Numero { get; set; } = 0;
        public int DigitoVerificador { get; set; } = 0;

        internal void AddContaBancaria(ContaBancaria contaBancaria)
        {
            contasBancarias.Add(contaBancaria);
        }

        internal double RelatorioFinanceiro(StringBuilder linhasAgencias)
        {
            double saldoParcial = 0;

            StringBuilder linhasContas = new StringBuilder();
            foreach (ContaBancaria conta in contasBancarias)
            {
                double saldo = conta.RelatorioFinanceiro(linhasContas);
                saldoParcial += saldo;
            }

            string linhaAgencia = $"  Agencia {Numero} - Saldo parcial: R$ {saldoParcial}\n";
            linhasAgencias.Append(linhaAgencia + linhasContas.ToString());

            return saldoParcial;
        }

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
        private double saldo = 0.0;

        public Agencia Agencia 
        { 
            get { return this.agencia; }
            set 
            { 
                this.agencia = value;
                this.agencia.AddContaBancaria(this);
                this.Logger.Debug($"Agencia definida para {agencia}"); 
            }
        }

        public int Numero { get; set; } = 0;
        public TipoConta? Tipo { get; set; } = null;
        public Logger Logger { get; } = new Logger();

        public ContaBancaria() 
        {
            this.Logger.Debug($"Conta bancária criada");
        }

        public static int QuantidadeSaquesNegados() {
            return 0;
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

        internal double RelatorioFinanceiro(StringBuilder linhasContas)
        {
            linhasContas.Append($"    Conta {Tipo} {Numero} - Saldo: R$ {saldo}\n");
            return saldo;
        }

        public override string ToString() 
        {
            return $"{this.Tipo} {this.Agencia}/{this.Numero}";
        }
    }
}
