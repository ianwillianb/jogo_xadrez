using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JogoXadrez.tabuleiro;

namespace JogoXadrez.xadrez
{
    class Rei : Peca 
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab,cor)
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
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //verificando nordeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //verificando direita
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //verificando sudeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //abaixo
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //sudoeste
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //esquerda
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //noroeste
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;

        }

        public override string ToString()
        {
            return "R";
        }


    }
}
