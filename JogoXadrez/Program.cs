using System;
using System.Collections.Generic;
using System.Text;
using JogoXadrez.tabuleiro;




namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao pos = new Posicao(50, 25);
            Console.WriteLine("Posição: " + pos);
            Console.ReadLine();

        }
    }
}
