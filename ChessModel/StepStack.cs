using System.Collections.Generic;

namespace ChessModel
{
    public sealed class StepStack
    {
        private readonly Stack<Move> _stack;
        private readonly Figure[] _board;

        public StepStack()
        {
            _stack = new Stack<Move>();
            _board = Figure._board;
        }

        public void Push(Step step)
        {
            var m = new Move(step);
            _stack.Push(m);
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

        public void Pop()
        {
            var m = _stack.Pop();
            _board[(m.step.fx << 3) + m.step.fy] = m.fFrom;
            _board[(m.step.tx << 3) + m.step.ty] = m.fOut;
            m.fFrom.X = m.step.fx;
            m.fFrom.Y = m.step.fy;
        }
    }
}
