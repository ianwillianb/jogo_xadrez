using JogoXadrez.tabuleiro;

namespace JogoXadrez.xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
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

            // acima
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            while (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
            }

            // abaixo
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

            // direita
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

            // esquerda
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            while (tab.VerificaPosicao(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }
    }
}
