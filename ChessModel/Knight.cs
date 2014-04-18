using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Knight : Figure
    {
#region variable
        static readonly int[] _dx = { 1, 1, -1, -1, 2, 2, -2, -2 };
        static readonly int[] _dy = { 2, -2, 2, -2, 1, -1, 1, -1 };
#endregion 

#region public methods
        public Knight(Player p, int x, int y)
            : base(p, 200, x,y)
        {

        }

        public override IEnumerable<Step> getLegalMove()
        {
            List<Step> ret = new List<Step>();
            for (int i = 0; i < 8; i++)
            {
                if (ChessPoint.PointInBoard(X + _dx[i], Y+_dy[i]))
                {
                    if (LegalMove(new ChessPoint(X + _dx[i], Y+_dy[i])))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y), 
                            new ChessPoint(X + _dx[i], Y + _dy[i])
                            )
                        );
                    }
                }
            }
            return ret;
        }
#endregion

#region private methods

#endregion
    }
}
