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
#endregion 

#region public methods
        public AI()
        {
            _moves = new Stack<Move>();
        }
#endregion

#region private methods
        void doStep(Step step)
        {
            Move m = new Move(step);
            _moves.Push(m);
            _board[(step.tx << 3) + step.ty] = m.fFrom;
            m.fFrom.X = step.tx;
            m.fFrom.Y = step.ty;
        }

#endregion
    }
}
