using System;
using JogoXadrez.tabuleiro;
using JogoXadrez.xadrez;


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
                            Console.BackgroundColor = fundOrginal;
                        }

                    }

                    
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

    }
}
