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
            Tabuleiro tab = new Tabuleiro(8, 8);
            Tela.imprimeTabuleiro(tab);

        }
    }
}
