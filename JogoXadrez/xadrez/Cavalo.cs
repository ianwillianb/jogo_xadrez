using JogoXadrez.tabuleiro;

namespace JogoXadrez.xadrez
{

    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.GetPeca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] MovPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}
