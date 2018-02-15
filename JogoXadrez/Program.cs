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
            
            try
            {

                Partida partida = new Partida();
                Tela.imprimeTabuleiro(partida.tab);

                while (!partida.finalizada)
                {   
                    Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab);

                    
                    Console.Write("\nOrigem: ");
                    Posicao org = Tela.lerPosicao().toPosicao();
                    Console.Clear();
                    Tela.imprimeTabuleiro(partida.tab, partida.tab.GetPeca(org).MovPossiveis());

                    Console.Write("Destino: ");
                    Posicao dest = Tela.lerPosicao().toPosicao();

                    partida.ExecMov(org, dest);

                }
            
              


            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            

            //PositionToName name = new PositionToName('a', 1);
            //Console.WriteLine(name.toPosicao().ToString());
            //Console.WriteLine(name.ToString().ToUpper());



        }
    }
}
