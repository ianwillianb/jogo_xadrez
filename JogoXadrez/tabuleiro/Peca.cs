using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez.tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos {get;set;}
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            posicao = null; //posição definida nas subclasses
            this.cor = cor;
            qteMovimentos = 0;
            this.tab = tab;
        }

        public void IncMovimentos()
        {
            qteMovimentos++;
        }

        public abstract bool[,] MovPossiveis();
        
        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = MovPossiveis();
            for(int i = 0; i<tab.linhas;i++)
            {
                for(int j = 0;j<tab.colunas;j++)
                {
                    if (mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool PodeMoverPos(Posicao pos)
        {

            return MovPossiveis()[pos.linha, pos.coluna];
        }


        
    }
}
