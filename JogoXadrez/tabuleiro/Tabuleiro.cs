using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez.tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca GetPeca(int linha, int coluna)
        {
            VerificaPosicaoException(new Posicao(linha, coluna));
            return pecas[linha, coluna];
            
        }

        public Peca GetPeca(Posicao pos)
        {
            VerificaPosicaoException(pos);
            return pecas[pos.linha, pos.coluna];                
        }

        public void insertPeca(Peca p, Posicao pos)
        {   
            if(ExistePeca(pos))
            {
                throw new TabuleiroException("Já Existe Uma Peça No Local!");
            }

            pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (!ExistePeca(pos)) return null;

            Peca aux = GetPeca(pos);
            aux.posicao = null;
            pecas[pos.linha, pos.coluna] = null;
            return aux;
            
        }

        public bool VerificaPosicao(Posicao pos)
        {
            if(pos.linha<0 || pos.linha>=linhas || pos.coluna<0 || pos.coluna>=colunas)
            {
                return false;
            }

            return true;
        }

        public void VerificaPosicaoException(Posicao pos)
        {
            if(VerificaPosicao(pos)==false)
            {

                throw new TabuleiroException("Posição Inválida.");
            }
        }

        public bool ExistePeca(Posicao pos)
        {
            VerificaPosicaoException(pos);

            if(GetPeca(pos) == null)
            {
                return false;
            }

            return true;
        }
    }
}
