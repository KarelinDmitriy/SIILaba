using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class AI
    {
        #region variable
        Stack<Move> _moves;
        public static Figure[] _board;
        Game _g;
        #endregion

        #region public methods
        public AI()
        {
            _moves = new Stack<Move>();
            _g = new Game();
        }

        //выбирает ход для выбранного игрока с глубиной не более maxDeep
        public Step selectMove(Player p, int maxDeep)
        {
            var moves = _g.getAllLegalMoves(p);
            int[] vMoves = new Int32[moves.Count()];
            if (p == Player.White)
            {
                int i = 0;
                foreach (var x in moves)
                {
                    int res;
                    doMove(x);
                    res = alphaBetaBlack(Int32.MinValue, Int32.MaxValue, maxDeep-1);
                    backMove();
                    vMoves[i++] = res;
                }
            }
            else
            {
                int i = 0;
                foreach (var x in moves)
                {
                    int res;
                    doMove(x);
                    res = alphaBetaWhite(Int32.MinValue, Int32.MaxValue, maxDeep-1);
                    backMove();
                    vMoves[i++] = res;
                }
            }
            int Max = vMoves[0], maxIdx = 0;
            for (int i = 1; i < vMoves.Length; i++)
            {
                if (vMoves[i] > Max)
                {
                    
                    Max = vMoves[i];
                    maxIdx = i;
                }
            }
            return moves.ToArray()[maxIdx];
        }
        #endregion

        #region private methods
        int alphaBetaWhite(int alpha, int beta, int depth)
        {
            int tmp;
            int max = Int32.MinValue;
            if (depth <= 0) return calculateScoreWhite() - calculateScoreBlack();
            var moves = _g.getAllLegalMoves(Player.White);
            foreach (var x in moves)
            {
                doMove(x);
                tmp = alphaBetaBlack(alpha, beta, depth - 1);
                backMove();
                if (tmp > max) max = tmp;
                if (tmp > alpha) alpha = tmp;
                if (max >= beta) return max;
            }
            return max;
        }

        private int alphaBetaBlack(int alpha, int beta, int depth)
        {
            int tmp;
            int min = Int32.MaxValue;
            if (depth <= 0) return calculateScoreBlack() - calculateScoreWhite();
            var moves = _g.getAllLegalMoves(Player.Black);
            foreach (var x in moves)
            {
                doMove(x);
                tmp = alphaBetaWhite(alpha, beta, depth - 1);
                backMove();
                if (tmp < min) min = tmp;
                if (tmp < beta) beta = tmp;
                if (min <= alpha) return min;
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
        void doMove(Step step)
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
        void backMove()
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
