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
        public bool xeque { get; private set; }


        public Partida()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            currentPlayer = Cor.Branca;
            finalizada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
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

        public Peca ExecMov(Posicao org, Posicao dest )
        {
            Peca p = tab.RetirarPeca(org);
            p.IncMovimentos();
            Peca capturada = tab.RetirarPeca(dest);
            tab.insertPeca(p, dest);
            if(capturada != null)
            {
                capturadas.Add(capturada);
            }

            return capturada;
        }

        public void desfazMov(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.decrementarQteMovimentos();
            if(capturada != null)
            {
                tab.insertPeca(capturada,destino);
                capturadas.Remove(capturada);
            }

            tab.insertPeca(p, origem);
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
            Peca peca_capturada = ExecMov(org, dest);
            if(VericaXeque(currentPlayer))
            {
                desfazMov(org, dest, peca_capturada);
                throw new TabuleiroException("Proibido se Colocar em Xeque!");
            }

            if (VericaXeque(CorAdvers(currentPlayer)))
            {
                xeque = true;
            }

            else
            {
                xeque = false;

            }

            if (testeXequeMate(CorAdvers(currentPlayer)))
            {
                finalizada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

        }

        public void mudaJogador()
        {
            if (currentPlayer == Cor.Branca)
            {
                currentPlayer = Cor.Preta;
            }
            else { currentPlayer = Cor.Branca; }
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.insertPeca(peca, new PositionToName(coluna, linha).toPosicao());
            pecas.Add(peca); 
        }

        private void InserirPecasTab()
        {

            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('a', 8, new Rei(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Torre(tab, Cor.Preta));

            /*
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
            ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            
           */

        }

        private Cor CorAdvers(Cor c)
        {
            if (c == Cor.Branca) return Cor.Preta;
            else return Cor.Branca;
        }

        private Peca returnRei(Cor c)
        {
            foreach(Peca x in pecasEmJogoList(c))
            {
                if (x is Rei) return x;

            }

            return null;
        }

        public bool VericaXeque(Cor c)
        {
            Peca R = returnRei(c);
            if (R == null) throw new TabuleiroException("Um Rei Não Consta no Tabuleiro!");
            
            foreach(Peca x in pecasEmJogoList(CorAdvers(c)))
            {
                bool[,] mat = x.MovPossiveis();

                if(mat[R.posicao.linha,R.posicao.coluna])
                {

                    return true;
                }
            }

             return false;

        }

        public bool testeXequeMate(Cor c)
        {
            if(!VericaXeque(c)) { return false; }

            foreach(Peca x in pecasEmJogoList(c))
            {
                bool[,] mat = x.MovPossiveis();
                for(int i = 0;i<tab.linhas;i++)
                {
                    for(int j=0;j<tab.colunas;j++)
                    {
                        if(mat[i,j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca capturada = ExecMov(x.posicao, destino);
                            bool testeXeque = VericaXeque(c);
                            desfazMov(x.posicao, destino, capturada);
                            if(!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true; //esta em xeque mate
        }

    }
}
