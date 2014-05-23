using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public sealed class AI
    {
        #region variable
        Stack<Move> _moves;
        public static Figure[] _board;
        Game _g;
        public long Count;
        #endregion

        #region public methods
        public AI()
        {
            _moves = new Stack<Move>();
            _g = new Game();
            Count = 0;
        }

        //выбирает ход для выбранного игрока с глубиной не более maxDeep
        public Step SelectMove(Player p, int maxDeep)
        {
            var moves = _g.getAllLegalMoves(p);
            int[] vMoves = new Int32[moves.Count()];
            if (p == Player.White)
            {
                int i = 0;
                foreach (var x in moves)
                {
                    int res;
                    DoMove(x);
                    res = AlphaBetaBlack(Int32.MinValue, Int32.MaxValue, maxDeep - 1);
                    BackMove();
                    vMoves[i++] = res;
                }
            }
            else
            {
                int i = 0;
                foreach (var x in moves)
                {
                    int res;
                    DoMove(x);
                    res = AlphaBetaWhite(Int32.MinValue, Int32.MaxValue, maxDeep - 1);
                    BackMove();
                    vMoves[i++] = res;
                }
            }
            int max = vMoves[0], maxIdx = 0;
            for (int i = 1; i < vMoves.Length; i++)
            {
                if (vMoves[i] > max)
                {

                    max = vMoves[i];
                    maxIdx = i;
                }
            }
            return moves.ToArray()[maxIdx];
        }
        #endregion

        #region private methods
        int AlphaBetaWhite(int alpha, int beta, int depth)
        {
            Count++;
            int max = Int32.MinValue;
            if (depth <= 0) return calculateScoreWhite() - calculateScoreBlack();
            var moves = _g.getAllLegalMoves(Player.White);
            if (moves.Count() != 0)
                foreach (var x in moves)
                {
                    DoMove(x);
                    int tmp = AlphaBetaBlack(alpha, beta, depth - 1);
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

        private int AlphaBetaBlack(int alpha, int beta, int depth)
        {
            Count++;
            int min = Int32.MaxValue;
            if (depth <= 0) return calculateScoreBlack() - calculateScoreWhite();
            var moves = _g.getAllLegalMoves(Player.Black);
            if (moves.Count() != 0)
                foreach (var x in moves)
                {
                    DoMove(x);
                    int tmp = AlphaBetaWhite(alpha, beta, depth - 1);
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

        //посчитать счет белых (пока не самым оптимальный способ)
        int calculateScoreWhite()
        {
            int sum = 0;
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] != null && _board[i].Player == Player.White)
                    sum += _board[i].Cost;
            }
            return sum;
        }

        //посчитать счет черных (пока не самым оптимальный способ)
        int calculateScoreBlack()
        {
            int sum = 0;
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] != null && _board[i].Player == Player.Black)
                    sum += _board[i].Cost;
            }
            return sum;
        }

        //делает ход и заносит в стек информацию
        //о том, что необходимо для его отмены 
        void DoMove(Step step)
        {
            Move m = new Move(step);
            _moves.Push(m);
            _board[(step.tx << 3) + step.ty] = m.fFrom;
            _board[(step.fx << 3) + step.fy] = null;
            if (m.fFrom is Pawn)
            {
                if (m.fFrom.Player == Player.White && step.tx == 7)
                    new Queen(Player.White, step.tx, step.ty);
                else if (m.fFrom.Player == Player.Black && step.tx == 0)
                    new Queen(Player.Black, step.tx, step.ty);
                else
                {
                    m.fFrom.X = step.tx;
                    m.fFrom.Y = step.ty;
                }
            }
            else
            {
                m.fFrom.X = step.tx;
                m.fFrom.Y = step.ty;
            }
        }

        //отменяет последний ход в стеке
        void BackMove()
        {
            Move m = _moves.Pop();
            _board[(m.step.fx << 3) + m.step.fy] = m.fFrom;
            _board[(m.step.tx << 3) + m.step.ty] = m.fOut;
            m.fFrom.X = m.step.fx;
            m.fFrom.Y = m.step.fy;
        }

        #endregion
    }
}
