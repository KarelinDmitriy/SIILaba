using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Move
    {
#region variable
        public Step step;
        public Figure fFrom;
        public Figure fOut;

        public static Figure[] _board;
#endregion 

#region public methods
        public Move(Step step)
        {
            this.step = new Step(step);
            fFrom = _board[(step.fx << 3) + step.fy];
            fOut = _board[(step.tx << 3) + step.ty];
        }
#endregion

#region private methods

#endregion
    }
}
