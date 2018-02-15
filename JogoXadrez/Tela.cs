using System;
using JogoXadrez.tabuleiro;


namespace JogoXadrez
{
    class Tela
    {
        public static void imprimeTabuleiro(Tabuleiro tab)
        {
            for(int i=0;i<tab.linhas;i++)
            {
                for(int j=0;j<tab.colunas;j++)
                {
                    if (tab.GetPeca(i, j) == null)
                    {   //Imprime o traço caso não existam peças na posição
                        Console.Write("- ");
                    }
                    else
                    {
                        //Imprime a Peça
                        Console.Write(tab.GetPeca(i, j) + " ");
                    }
                }

                //Parte para a proxíma linha quando todas as colunas da linha forem percorridas
                Console.WriteLine(); 
            }
        }

    }
}
