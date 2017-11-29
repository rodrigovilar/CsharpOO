namespace Aeroporto
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

    public class Agente : Pessoa
    {
        public double Salario { get; set; }
    }

    public class Tripulante : Pessoa
    {
        public double HorasVoo { get; set; }
    }

    public class Passageiro : Pessoa
    {
        public double Milhagem { get; set; }
    }

}