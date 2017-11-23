using Xunit;
using Zoo;

namespace Business.Test
{
    public class ZooTest
    {
        [Fact]
        public void AnimaisRespirandoFalando()
        {
            Gato gato = new Gato()
            {
                Nome = "Gato de Botas",
                Idade = 3
            };
            Cachorro cachorro = new Cachorro()
            {
                Raca = "Labrador",
                Nome = "Rex",
                Idade = 5
            };
            
            Assert.Equal("Gato de Botas: respirando...", gato.Respirar());
            Assert.Equal("Gato de Botas: Miau! Eu tenho 3 anos!", gato.Miar());

            Assert.Equal("Rex: respirando...", cachorro.Respirar());
            Assert.Equal("Rex: Au! Eu sou um Labrador e tenho 5 anos!", cachorro.Latir());
        }
    }
}