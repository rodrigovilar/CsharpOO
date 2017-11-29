using System.Collections.Generic;
using Xunit;
using Aeroporto;
using Support;
using System;

namespace Business.Test
{
    public class AeroportoTest
    {
        // [Fact]
        // public void Agente()
        // {
        //     Agente agente = new Agente() 
        //     {
        //         Nome = "Jose",
        //         Cpf = "00001",
        //         Salario = 5000
        //     };
        // }

        // [Fact]
        // public void Tripulante()
        // {
        //     Tripulante tripulante = new Tripulante() 
        //     {
        //         Nome = "Maria",
        //         Cpf = "00002",
        //         HorasVoo = 500
        //     };
        // }

        // [Fact]
        // public void Passageiro()
        // {
        //     Passageiro passageiro = new Passageiro() 
        //     {
        //         Nome = "Joao",
        //         Cpf = "00003",
        //         Milhagem = 50000
        //     };
        // }

        [Fact]
        public void Mutacao()
        {
            Agente agente = new Agente() 
            {
                Pessoa = new Pessoa()
                {
                    Nome = "Jose",
                    Cpf = "00001"
                },
                Salario = 5000
            };

            Passageiro passageiro = new Passageiro() 
            {
                Pessoa = agente.Pessoa,
                Milhagem = 50000
            };

            passageiro.Nome = "Antonio";
            Assert.Equal("Antonio", agente.Nome);
        }

        [Fact]
        public void Teclado()
        {
            Teclado teclado = new Teclado();
            Agente agente = new Agente();
            teclado.Handlers += plugado =>
            {
                if (plugado) {
                    Console.WriteLine("Prestando atenção no teclado");
                } else {
                    Console.WriteLine("Descansando");
                }
            };
            teclado.AvisarTecladoPlugado();
        }
    }
}