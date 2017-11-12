using System;

namespace Business
{
    public enum TipoConta { Poupanca, Corrente, Investimento };

    public class ContaBancaria
    {
        public string banco { get; set; }
        public int agencia { get; set; }
        public int numero { get; set; }
        public TipoConta tipo { get; set; }
    }
}
