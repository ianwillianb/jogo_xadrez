using System;
using System.Collections.Generic;
using JogoXadrez.tabuleiro;
namespace JogoXadrez.xadrez
{
    class Partida
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor currentPlayer { get; private set; }
        public bool finalizada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public Partida()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            currentPlayer = Cor.Branca;
            finalizada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            InserirPecasTab();
            
            
           
        }

        public void ValidarPosOrigem(Posicao pos)
        {
            if(tab.GetPeca(pos)==null)
            {
                throw new TabuleiroException("Não Existe Peça Na Posição de Origem!");
            }

            if(tab.GetPeca(pos).cor != currentPlayer)
            {
                throw new TabuleiroException("Peça Escolhida Pertence ao Jogador com As Peças " +tab.GetPeca(pos).cor + "!");
            }

            if(!tab.GetPeca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não Existem Movimentos Disponíveis!");
            }
        }

        public void ValidarPosDestino(Posicao origem, Posicao destino)
        {

            if(!tab.GetPeca(origem).PodeMoverPos(destino))
            {
                throw new TabuleiroException("Posição de Destino Inválida!");
            }
        }

        public void ExecMov(Posicao org, Posicao dest )
        {
            Peca p = tab.RetirarPeca(org);
            p.IncMovimentos();
            Peca capturada = tab.RetirarPeca(dest);
            tab.insertPeca(p, dest);
            if(capturada != null)
            {
                capturadas.Add(capturada);
            }
        }

        public HashSet<Peca> pecasCapturadasList(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }

            }

            return aux;

        }

        public HashSet<Peca> pecasEmJogoList(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }

            }

            aux.ExceptWith(pecasCapturadasList(cor));

            return aux;

        }

        public void RealizaJogada(Posicao org, Posicao dest)
        {
            ExecMov(org, dest);
            turno++;
            mudaJogador();

        }

        public void mudaJogador()
        {
            if (currentPlayer == Cor.Branca)
            {
                currentPlayer = Cor.Preta;
            }
            else currentPlayer = Cor.Branca;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.insertPeca(peca, new PositionToName(coluna, linha).toPosicao());
            pecas.Add(peca); 
        }

        private void InserirPecasTab()
        {
            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Torre(tab, Cor.Preta));



        }
    }
}
