using System;
using System.Collections.Generic;
using System.Text;
using JogoXadrez.tabuleiro;
using JogoXadrez.xadrez;



namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            Tabuleiro tab2 = new Tabuleiro(10, 10);
            tab.insertPeca(new Torre(tab,Cor.Preta),new Posicao(0,0));
            tab.insertPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.insertPeca(new Rei(tab2, Cor.Preta), new Posicao(2, 4));

            Tela.imprimeTabuleiro(tab);



        }
    }
}
