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
            try
            {

                tab.insertPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.insertPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.insertPeca(new Rei(tab, Cor.Branca), new Posicao(2, 4));
                tab.insertPeca(new Rei(tab, Cor.Branca), new Posicao(0, 5));

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Tela.imprimeTabuleiro(tab);

            //PositionToName name = new PositionToName('a', 1);
            //Console.WriteLine(name.toPosicao().ToString());
            //Console.WriteLine(name.ToString().ToUpper());



        }
    }
}
