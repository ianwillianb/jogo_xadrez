using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JogoXadrez.tabuleiro;

namespace JogoXadrez.xadrez
{
    class PositionToName
    {
        public int linha { get; set; }
        public char coluna { get; set; }

        public PositionToName(char coluna, int linha)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public Posicao toPosicao()
        {

            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
