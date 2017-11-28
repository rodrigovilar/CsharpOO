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

}