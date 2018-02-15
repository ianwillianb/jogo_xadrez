using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JogoXadrez.tabuleiro;

namespace JogoXadrez.xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
            
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.GetPeca(pos);
            return p == null || p.cor != cor;

        }

        public override bool[,] MovPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            //verificando acima
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);

            while(tab.VerificaPosicao(pos)&&PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }

                pos.linha = pos.linha - 1;
            }


            //verificando abaixo
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);

            while (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }

                pos.linha = pos.linha + 1;
            }


            //verificando direita
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);

            while (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }

                pos.coluna = pos.coluna + 1;
            }

            //verificando esquerda
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);

            while (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }

                pos.coluna = pos.coluna + 1;
            }

            return mat;

        }

        public override string ToString()
        {
            return "T";
        }


    }
}
