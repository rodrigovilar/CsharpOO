using System;

namespace Business
{
    public enum TipoConta { Poupanca, Corrente, Investimento };

    public class ContaBancaria
    {
        public string banco { get; set; }
        public int agencia { get; set; }
        public int numero { get; set; }
        public TipoConta? tipo { get; set; }

        public ContaBancaria(string banco, int agencia, int numero, TipoConta? tipo)
        {
            this.banco = banco;
            this.agencia = agencia;
            this.numero = numero;
            this.tipo = tipo;
        }

        public ContaBancaria() : this(null, 0, 0, null)
        {
        }
    }
}
