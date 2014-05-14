using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Queen : Figure
    {
        #region variable
        static readonly int[] _dx = { 1, 0, -1, 1, -1, 1, 0, -1 };
        static readonly int[] _dy = { 1, 1, 1, 0, 0, -1, -1, -1 };
        #endregion

        #region public methods
        public Queen(Player p, int x, int y)
            : base(p, 900, x, y)
        { }

        public override IEnumerable<Step> getRightMove()
        {
            List<Step> ret = new List<Step>();
            for (int i = 0; i < 8; i++)
            {
                int j = 1;
                while ((((X + _dx[i] * j) & (int.MaxValue-7)) == 0) && 
                        ((Y + _dy[i] * j) & (int.MaxValue-7)) == 0)
                {
                    int nc = ((X + _dx[i] * j)<<3) + Y + _dy[i] * j;
                    if (_board[nc] != null)
                    {
                        if (_board[nc].Player!=_player)
                            ret.Add(new Step(X, Y, nc/8, nc%8));
                        break;
                    }
                    ret.Add(new Step(X, Y, nc / 8, nc % 8));
                    j++;
                }
            }
            return ret;
        }

        public override bool attackTarget(Figure f)
        {
            for (int i = 0; i < 8; i++)
            {
                int j = 1;
                while ((((X + _dx[i] * j) & (int.MaxValue - 7)) == 0) &&
                       ((Y + _dy[i] * j) & (int.MaxValue - 7)) == 0)
                {
                    int nc = ((X + _dx[i] * j)<<3) + Y + _dy[i] * j;
                    if (ReferenceEquals(_board[nc], f)) return true;
                    if (ReferenceEquals(_board[nc],null))
                        break;
                    j++;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == ChessModel.Player.White ? "Q" : "q";
        }

        public override string PictureName()
        {
            if (_player == ChessModel.Player.White) return "WhiteQueen";
            else return "BlackQueen";
        }
        #endregion

        #region private methods

        #endregion
    }
}
