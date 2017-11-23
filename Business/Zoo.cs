namespace Zoo
{
    public class Animal
    {
        public int Idade { get; set; }
        public string Nome { get; set; }

        public string Respirar()
        {
            return $"{Nome}: respirando...";
        }

    }

    public class Gato : Animal
    {
        public string Miar() {
            return $"{Nome}: Miau! Eu tenho {Idade} anos!";
        }
    }

    public class Cachorro : Animal
    {
        public string Raca { get; set; }

        public string Latir() {
            return $"{Nome}: Au! Eu sou um {Raca} e tenho {Idade} anos!";
        }
    }

}