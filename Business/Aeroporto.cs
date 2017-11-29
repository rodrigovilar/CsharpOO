using System;
using Support;

namespace Aeroporto
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

    public class Agente
    {
        public Agente()
        {
        }

        public Pessoa Pessoa { get; set; }
        public string Nome 
        { 
            get { return this.Pessoa.Nome; }
            set { this.Pessoa.Nome = value; } 
        }
        public string Cpf 
        { 
            get { return this.Pessoa.Cpf; }
            set { this.Pessoa.Cpf = value; } 
        }
        public double Salario { get; set; }

    }

    public class Tripulante
    {
        public double HorasVoo { get; set; }
        public Pessoa Pessoa { get; set; }
        public string Nome 
        { 
            get { return this.Pessoa.Nome; }
            set { this.Pessoa.Nome = value; } 
        }
        public string Cpf 
        { 
            get { return this.Pessoa.Cpf; }
            set { this.Pessoa.Cpf = value; } 
        }
    }

    public class Passageiro
    {
        public double Milhagem { get; set; }
                public Pessoa Pessoa { get; set; }
        public string Nome 
        { 
            get { return this.Pessoa.Nome; }
            set { this.Pessoa.Nome = value; } 
        }
        public string Cpf 
        { 
            get { return this.Pessoa.Cpf; }
            set { this.Pessoa.Cpf = value; } 
        }
    }

}