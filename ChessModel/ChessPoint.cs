using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class ChessPoint
    {
        
#region variable
        public int x, y;
#endregion 

#region public methods
        public ChessPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool inBoard()
        {
            return x < 8 && x >= 0 && y < 8 && y >= 0;
        }

        public static bool  PointInBoard(int x, int y)
        {
            return x < 8 && x >= 0 && y < 8 && y >= 0;
        }
#endregion

#region private methods

#endregion
        
    }
}
