using System.Collections.Generic;

namespace Aeroporto
{
    public interface IPapelPessoa
    {
    }
    
    public class Pessoa
    {
        public List<IPapelPessoa> Papeis { get; set; } = new List<IPapelPessoa>();
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }


    public class Agente : IPapelPessoa
    {
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

    public class Tripulante : IPapelPessoa
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

    public class Passageiro : IPapelPessoa
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