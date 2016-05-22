using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public sealed class AI
    {
        #region variable

        public Board _board;
        Game _g;
        public long Count;
        #endregion

        #region public methods
        public AI(Board board)
        {
	        _board = board;
	        _g = new Game(board);
            Count = 0;
        }

        //выбирает ход для выбранного игрока с глубиной не более maxDeep
        public Step SelectMove(Player p, int maxDeep)
        {
            var moves = _g.getAllLegalMoves(p);
            var vMoves = new int[moves.Count];
            if (p == Player.White)
            {
                var i = 0;
                foreach (var x in moves)
                {
                    DoMove(x);
                    var res = AlphaBetaBlackWS(int.MinValue, Int32.MaxValue, maxDeep - 1);
                    BackMove();
                    vMoves[i++] = res;
                }
            }
            else
            {
                var i = 0;
                foreach (var x in moves)
                {
                    DoMove(x);
                    int res = AlphaBetaWhiteBS(Int32.MinValue, Int32.MaxValue, maxDeep - 1);
                    BackMove();
                    vMoves[i++] = res;
                }
            }
            var max = vMoves.Max();
            var arrMoves = moves.ToArray();
            var bestSteps = new List<Step>();
            for (var i = 0; i < vMoves.Length; i++)
            {
                if (vMoves[i] == max)
                    bestSteps.Add(arrMoves[i]);
            }
            var rand = new Random();
            return bestSteps[rand.Next() % bestSteps.Count];
        }
        #endregion

        #region private methods

        #region White Step First

        private int AlphaBetaWhiteWS(int alpha, int beta, int depth)
        {
            Count++;
            var max = Int32.MinValue;
            if (depth <= 0) return calculateScoreWhite() - calculateScoreBlack();
            var moves = _g.getAllLegalMoves(Player.White);
            if (moves.Count() != 0)
                foreach (var x in moves)
                {
                    DoMove(x);
                    var tmp = AlphaBetaBlackWS(alpha, beta, depth - 1);
                    BackMove();
                    if (tmp > max) max = tmp;
                    if (tmp > alpha) alpha = tmp;
                    if (max >= beta) return max;
                }
            else
            {
                var state = _g.calcState();
                if (state == State.Checkmate)
                    return 0;
                else return -10000;
            }
            return max;
        }

        private int AlphaBetaBlackWS(int alpha, int beta, int depth)
        {
            Count++;
            var min = int.MaxValue;
            if (depth <= 0) return calculateScoreBlack() - calculateScoreWhite();
            var moves = _g.getAllLegalMoves(Player.Black);
            if (moves.Count() != 0)
                foreach (var x in moves)
                {
                    DoMove(x);
                    var tmp = AlphaBetaWhiteWS(alpha, beta, depth - 1);
                    BackMove();
                    if (tmp < min) min = tmp;
                    if (tmp < beta) beta = tmp;
                    if (min <= alpha) return min;
                }
            else
            {
                var state = _g.calcState();
                if (state == State.Checkmate)
                    return 0;
                else return 10000;
            }
            return min;
        }

        #endregion

        #region Black Step First

        private int AlphaBetaWhiteBS(int alpha, int beta, int depth)
        {
            Count++;
            var min = Int32.MaxValue;
            if (depth <= 0)
            {
                var state = _g.calcState(Player.White);
                switch (state)
                {
                    case State.Calm:
                        return calculateScoreWhite() - calculateScoreBlack();
                        break;
                    case State.Check:
                        return calculateScoreWhite() - calculateScoreBlack() - 300 ;
                        break;
                    case State.Checkmate:
                        return Int32.MinValue;
                        break;
                    case State.Draw:
                        return 0;
                        break;
                }
            }
            var moves = _g.getAllLegalMoves(Player.White);
            foreach (var x in moves)
            {
                DoMove(x);
                var tmp = AlphaBetaBlackBS(alpha, beta, depth - 1);
                BackMove();
                if (tmp < min) min = tmp;
                if (tmp < beta) beta = tmp;
                if (min <= alpha) return min;
            }
            return min;
        }

        private int AlphaBetaBlackBS(int alpha, int beta, int depth)
        {
            Count++;
            var max = Int32.MinValue;
            if (depth <= 0)
            {
                var state = _g.calcState(Player.White);
                switch (state)
                {
                    case State.Calm:
                        return calculateScoreBlack() - calculateScoreWhite();
                        break;
                    case State.Check:
                        return calculateScoreBlack()- 300-calculateScoreWhite()   ;
                        break;
                    case State.Checkmate:
                        return Int32.MaxValue;
                        break;
                    case State.Draw:
                        return 0;
                        break;
                }
            }
            var moves = _g.getAllLegalMoves(Player.Black);
            foreach (var x in moves)
            {
                DoMove(x);
                var tmp = AlphaBetaWhiteBS(alpha, beta, depth - 1);
                BackMove();
                if (tmp > max) max = tmp;
                if (tmp > alpha) alpha = tmp;
                if (max >= beta) return max;
            }
            return max;
        }

        #endregion


        //посчитать счет белых (пока не самым оптимальный способ)
        int calculateScoreWhite()
        {
            var sum = 0;
            foreach (var figure in _board.Figures)
            {
	            if (figure != null && figure.Player == Player.White)
		            sum += figure.Cost;
            }
	        return sum;
        }

        //посчитать счет черных (пока не самым оптимальный способ)
        int calculateScoreBlack()
        {
            var sum = 0;
            foreach (var figure in _board.Figures)
            {
	            if (figure != null && figure.Player == Player.Black)
		            sum += figure.Cost;
            }
	        return sum;
        }

        //делает ход и заносит в стек информацию
        //о том, что необходимо для его отмены 
        void DoMove(Step step)
        {
           _board.Move(step);
        }

        //отменяет последний ход в стеке
        void BackMove()
        {
            _board.CanselMove();
        }

        #endregion
    }
}
