using System;

namespace Zoo
{
    public abstract class Animal
    {
        public int Idade { get; set; }
        public string Nome { get; set; }

        public virtual string Respirar()
        {
            return $"{Nome}: respirando...";
        }

        public abstract string FazerBarulho();

    }

    public class Gato : Animal
    {
        public override string FazerBarulho() {
            return $"{Nome}: Miau! Eu tenho {Idade} anos!";
        }

    }

    public class Cachorro : Animal
    {
        public string Raca { get; set; }

        public override string FazerBarulho() {
            return $"{Nome}: Au! Eu sou um {Raca} e tenho {Idade} anos!";
        }
    }

    public enum TipoPassaro { Europeu, Africano, NorueguesAzul }

    public class Passaro
    {
        public TipoPassaro TipoPassaro { get; set; }
        public double VelocidadeBase { get; set; }
        public double FatorDeCarga { get; set; }
        public int QuantidadeCocos { get; set; }
        public bool EstaPreso { get; set; }
        public int Voltagem { get; set; }

        public double Velocidade
        {
            get
            {
                switch (TipoPassaro)
                {
                    case TipoPassaro.Europeu:
                        return VelocidadeBase;
                    case TipoPassaro.Africano:
                        return VelocidadeBase - (FatorDeCarga * QuantidadeCocos);
                    case TipoPassaro.NorueguesAzul:
                        return (EstaPreso) ? 0 : (VelocidadeBase * Voltagem) / 110;
                    default:
                        throw new Exception("Não deve chegar aqui");
                }
            }
        }

    }
}