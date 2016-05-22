using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ChessGame
{
    static  class Functions
    {
        public static void Precalc()
        {
            Queen.PrecalcStep();
            Bishop.PrecalcStep();
            Rook.PrecalcStep();
            King.preaCalc();
            Knight.preaCalc();
        }

        
    }
}
