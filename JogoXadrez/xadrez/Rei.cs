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
        private Partida partida;

        public Rei(Tabuleiro tab, Cor cor, Partida partida) : base(tab,cor)
        {
            this.partida = partida;
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.GetPeca(pos);
            return p == null || p.cor != cor;

        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.GetPeca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
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




            // #jogadaespecial roque
            if (qteMovimentos == 0 && !partida.xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.GetPeca(p1) == null && tab.GetPeca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.GetPeca(p1) == null && tab.GetPeca(p2) == null && tab.GetPeca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }


            return mat;
        }

        public override string ToString()
        {
            return "R";
        }


    }
}
