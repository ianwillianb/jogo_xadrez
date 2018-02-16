using System;
using JogoXadrez.tabuleiro;
using JogoXadrez.xadrez;
using System.Collections.Generic;

namespace JogoXadrez
{
    class Tela
    {
        public static void imprimeTabuleiro(Tabuleiro tab)
        {
            for(int i=0;i<tab.linhas;i++)
            {
                Console.Write( ((tab.linhas)-i) + " " );
                for(int j=0;j<tab.colunas;j++)
                {
                    if (tab.GetPeca(i, j) == null)
                    {   //Imprime o traço caso não existam peças na posição
                        Console.Write("- ");
                    }
                    else
                    {
                        //Imprime a Peça de Acordo Se É Branca ou Preta
                        Peca aux = tab.GetPeca(i, j);
                        if (aux.cor == Cor.Branca) { Console.Write(aux + " "); }
                        else
                        {
                            ConsoleColor auxcolor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(aux + " ");
                            Console.ForegroundColor = auxcolor;
                        }   

                    }
                }


                //Parte para a proxíma linha quando todas as colunas da linha forem percorridas
                Console.WriteLine();
                
            }

            
            for (int j = 0;j<tab.colunas;j++)
            {
                if (j == 0) Console.Write(" ");
                Console.Write(" "+(char)('A'+j));
            }
            Console.WriteLine();
        }


        public static void imprimeTabuleiro(Tabuleiro tab, bool[,] movimentosPossiveis)
        {
            ConsoleColor fundOrginal = Console.BackgroundColor;
            ConsoleColor fundoMarcado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(((tab.linhas) - i) + " ");

                for (int j = 0; j < tab.colunas; j++)
                {
                    if (movimentosPossiveis[i, j] == true)
                    {
                        Console.BackgroundColor = fundoMarcado;
                    }

                    else Console.BackgroundColor = fundOrginal;

                    

                    if (tab.GetPeca(i, j) == null)
                    {   //Imprime o traço caso não existam peças na posição
                        Console.Write("- ");
                    }
                    else
                    {
                        //Imprime a Peça de Acordo Se É Branca ou Preta
                        Peca aux = tab.GetPeca(i, j);
                        if (aux.cor == Cor.Branca) { Console.Write(aux + " "); }
                        else
                        {
                            ConsoleColor auxcolor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(aux + " ");
                            Console.ForegroundColor = auxcolor;
                            
                        }
                        
                    }

                    Console.BackgroundColor = fundOrginal;
                }


                //Parte para a proxíma linha quando todas as colunas da linha forem percorridas
                Console.WriteLine();

            }

            Console.BackgroundColor = fundOrginal;

            for (int j = 0; j < tab.colunas; j++)
            {
                if (j == 0) Console.Write(" ");
                Console.Write(" " + (char)('A' + j));
            }

            Console.WriteLine();
            Console.BackgroundColor = fundOrginal;


        }

        
        public static PositionToName lerPosicao()
        {   
            string s = Console.ReadLine().ToLower();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PositionToName(coluna, linha);
;       }

        public static void imprimirPartida(Partida partida)
        {
            imprimeTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if (!partida.finalizada)
            {
                Console.WriteLine("Aguardando Jogada: " + partida.currentPlayer);
                if (partida.xeque)
                {
                    Console.WriteLine("Jogador com As Peças " + partida.currentPlayer + "s está em Xeque!");
                }
            }

            else
            {
                Console.WriteLine("Xeque Mate! \nCor das Peças Vencedoas: " + partida.currentPlayer + "s");
            }
        }

        public static void ImprimirPecasCapturadas(Partida partida)
        {
            Console.WriteLine("-------Peças Capturadas-------");
            Console.Write("Brancas: ");
            imprimeConjunto(partida.pecasCapturadasList(Cor.Branca));

            Console.Write("Pretas:  ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimeConjunto(partida.pecasCapturadasList(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine("------------------------------");


        }

        public static void imprimeConjunto(HashSet<Peca> conjutoPecas)
        {
            Console.Write("[");
            foreach(Peca x in conjutoPecas)
            {
                Console.Write(x + ",");
            }
            Console.WriteLine("]");
        }

    }
}
