using System.Collections.Generic;
using Xunit;
using Zoo;

namespace Business.Test
{
    public class ZooTest
    {
        [Fact]
        public void AnimaisRespirandoFalando()
        {
            Assert.Equal("Gato de Botas: respirando...", ZooTestHelper.gato.Respirar());
            Assert.Equal("Gato de Botas: Miau! Eu tenho 3 anos!", ZooTestHelper.gato.FazerBarulho());

            Assert.Equal("Rex: respirando...", ZooTestHelper.cachorro.Respirar());
            Assert.Equal("Rex: Au! Eu sou um Labrador e tenho 5 anos!", ZooTestHelper.cachorro.FazerBarulho());
        }

        [Fact]
        public void CastAnimais()
        {
            Animal a1 = ZooTestHelper.gato;
            Gato g1 = a1 as Gato;
            Assert.NotNull(g1);

            Cachorro c1 = a1 as Cachorro;
            Assert.Null(c1);

            List<Animal> animais = new List<Animal>();
            animais.Add(ZooTestHelper.gato);
            animais.Add(ZooTestHelper.cachorro);

            Assert.Equal("Gato de Botas: respirando...", animais[0].Respirar());
            Assert.Equal("Gato de Botas: Miau! Eu tenho 3 anos!", animais[0].FazerBarulho());

            Assert.Equal("Rex: respirando...", animais[1].Respirar());
            Assert.Equal("Rex: Au! Eu sou um Labrador e tenho 5 anos!", animais[1].FazerBarulho());

            foreach (Animal animal in animais)
            {
                animal.Respirar();
                animal.FazerBarulho(); // Chamada polimorfica
            }
        }
    }

    public class ZooTestHelper
    {
        public static Gato gato = new Gato()
        {
            Nome = "Gato de Botas",
            Idade = 3
        };
        public static Cachorro cachorro = new Cachorro()
        {
            Raca = "Labrador",
            Nome = "Rex",
            Idade = 5
        };
    }
}