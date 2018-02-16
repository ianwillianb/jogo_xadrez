using JogoXadrez.tabuleiro;

namespace JogoXadrez.xadrez
{

    class Dama : Peca
    {

        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "D";
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

            // esquerda
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha, pos.coluna - 1);
            }

            // direita
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha, pos.coluna + 1);
            }

            // acima
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha - 1, pos.coluna);
            }

            // abaixo
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha + 1, pos.coluna);
            }

            // NO
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {  
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha - 1, pos.coluna - 1);
            }

            // NE
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha - 1, pos.coluna + 1);
            }

            // SE
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha + 1, pos.coluna + 1);
            }

            // SO
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.VerificaPosicao(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.GetPeca(pos) != null && tab.GetPeca(pos).cor != cor)
                {
                    break;
                }
                pos.DefinirValores(pos.linha + 1, pos.coluna - 1);
            }

            return mat;
        }
    }
}
