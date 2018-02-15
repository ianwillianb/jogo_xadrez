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

        public Partida()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            currentPlayer = Cor.Branca;
            InserirPecasTab();
            finalizada = false;
            
           
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

        public void ExecMov(Posicao org, Posicao dest )
        {
            Peca p = tab.RetirarPeca(org);
            p.IncMovimentos();
            Peca capturada = tab.RetirarPeca(dest);
            tab.insertPeca(p, dest);
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

        private void InserirPecasTab()
        {
            
            tab.insertPeca(new Torre(tab, Cor.Preta), new PositionToName('a', 1).toPosicao());
            tab.insertPeca(new Torre(tab, Cor.Preta), new PositionToName('b', 2).toPosicao());
            tab.insertPeca(new Rei(tab, Cor.Branca), new PositionToName('c', 3).toPosicao());
            tab.insertPeca(new Rei(tab, Cor.Branca), new PositionToName('d', 4).toPosicao());
        }
    }
}
