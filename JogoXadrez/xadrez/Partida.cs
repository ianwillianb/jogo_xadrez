using System;
using System.Collections.Generic;
using JogoXadrez.tabuleiro;
namespace JogoXadrez.xadrez
{
    class Partida
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor currentPlayer;
        public bool finalizada { get; private set; }

        public Partida()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            currentPlayer = Cor.Branca;
            InserirPecasTab();
            finalizada = false;
            
           
        }

        public void ExecMov(Posicao org, Posicao dest )
        {
            Peca p = tab.RetirarPeca(org);
            p.IncMovimentos();
            Peca capturada = tab.RetirarPeca(dest);
            tab.insertPeca(p, dest);
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
