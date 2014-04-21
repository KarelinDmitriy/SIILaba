using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class King : Figure
    {
        #region variable
        static readonly int[] _dx = { 1, 0, -1, 1, -1, 1, 0, -1 };
        static readonly int[] _dy = { 1, 1, 1, 0, 0, -1, -1, -1 };

        #endregion

        #region public methods
        public King(Player p, int x, int y)
            : base(p, 10000, x, y)
        {

        }

        public override IEnumerable<Step> getRightMove()
        {
            List<Step> ret = new List<Step>();
            for (int i = 0; i < 8; i++)
            {
                if (ChessPoint.PointInBoard(X + _dx[i], Y + _dy[i]))
                {
                   if (RightMove(new ChessPoint(X + _dx[i], Y + _dy[i])))
                   {
                       ret.Add(new Step(
                           new ChessPoint(X, Y), 
                           new ChessPoint(X + _dx[i], Y + _dy[i]))
                       );
                   }
                }
            }
            return ret;
        }

        public override bool attackTarget(Figure f)
        {
            for (int i = 0; i < 8; i++)
            {
                if (ChessPoint.PointInBoard(X + _dx[i], Y + _dy[i]))
                {
                    if (_board[X + _dx[i], Y + _dy[i]] == f)
                        return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == ChessModel.Player.White ? "K" : "k";
        }
        #endregion

        #region private methods

        #endregion
    }
}
