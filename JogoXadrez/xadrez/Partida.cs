using System;
using System.Collections.Generic;
using JogoXadrez.tabuleiro;
using JogoXadrez.xadrez;

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
        public Peca vulneravelEnPassant { get; private set; }


        public Partida()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            currentPlayer = Cor.Branca;
            finalizada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
            vulneravelEnPassant = null;
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


            // #jogadaespecial roque pequeno
            if (p is Rei && dest.coluna == org.coluna + 2)
            {
                Posicao origemT = new Posicao(org.linha, org.coluna + 3);
                Posicao destinoT = new Posicao(org.linha, org.coluna + 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncMovimentos();
                tab.insertPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && dest.coluna == org.coluna - 2)
            {
                Posicao origemT = new Posicao(org.linha, org.coluna - 4);
                Posicao destinoT = new Posicao(org.linha, org.coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncMovimentos();
                tab.insertPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (org.coluna != dest.coluna && capturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(dest.linha + 1, dest.coluna);
                    }
                    else
                    {
                        posP = new Posicao(dest.linha - 1, dest.coluna);
                    }
                    capturada = tab.RetirarPeca(posP);
                    capturadas.Add(capturada);
                }
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

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.insertPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.insertPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && capturada == vulneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.insertPeca(peao, posP);
                }
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

            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

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
                            Peca capturada = ExecMov(origem, destino);
                            bool testeXeque = VericaXeque(c);
                            desfazMov(origem, destino, capturada);
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
