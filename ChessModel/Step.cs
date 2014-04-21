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

        public static Step stringToStep(string a)
        {
            int y1 = a[0] - 'a';
            int x1 = int.Parse(a[1]+"")-1;
            int y2 = a[2] - 'a';
            int x2 = int.Parse(a[3] + "")-1;
            return new Step(new ChessPoint(x1, y1), new ChessPoint(x2, y2));
        }
#endregion

#region private methods

#endregion      
    }
}
