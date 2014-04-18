using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Step
    {
#region variable
        public ChessPoint from;
        public ChessPoint to;
#endregion 

#region public methods
        public Step(ChessPoint from, ChessPoint to)
        {
            this.from = from;
            this.to = to;
        }
#endregion

#region private methods

#endregion      
    }
}
