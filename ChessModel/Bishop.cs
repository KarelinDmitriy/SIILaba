using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Bishop : Figure
    {
#region variable
        static readonly int[] _dx = { 1, 1, -1, -1 };
        static readonly int[] _dy = { 1, -1, 1, -1 };
#endregion 

#region public methods
        public Bishop(Player p, int x, int y)
            :base(p, 200, x, y)
        {

        }

        public override IEnumerable<Step> getRightMove()
        {
            List<Step> ret = new List<Step>();
            for (int i = 0; i < 4; i++)
            {
                int j = 1;
                while (ChessPoint.PointInBoard(X + _dx[i] * j, Y + _dy[i] * j))
                {
                    ChessPoint to = new ChessPoint(X + _dx[i] * j, Y + _dy[i] * j);
                    if (_board[to.x, to.y] != null && !_board[to.x, to.y].isEnemy(this))
                        break;
                    if (RightMove(to))
                        ret.Add(new Step(new ChessPoint(X, Y), to));
                    j++;
                }
            }
            return ret;
        }

        public override string ToString()
        {
            return _player == ChessModel.Player.White ? "B" : "b";
        }
#endregion

#region private methods

#endregion
    }
}
