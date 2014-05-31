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
        static HashSet<int>[] pSteps = new HashSet<int>[64];
#endregion 

#region public methods
        public Knight(Player p, int x, int y)
            : base(p, 300, x,y)
        {

        }

        public override List<Step> GetRightMove()
        {
            var ret = new List<Step>();
            for (var i = 0; i < 8; i++)
            {
                if ((((X + _dx[i] ) & (int.MaxValue - 7)) == 0) &&
                         ((Y + _dy[i] ) & (int.MaxValue - 7)) == 0)
                {
                    var nc = ((X + _dx[i])<<3) + Y + _dy[i];
                    if (_board[nc]==null || _board[nc].IsEnemy(this))
                    {
                        ret.Add(new Step(X, Y, nc/8, nc%8));
                    }
                }
            }
            return ret;
        }

        public override bool AttackTarget(Figure f)
        {
            if (pSteps[(X << 3) + Y].Contains((f.X << 3) + f.Y)) return true;
            return false;
        }

        public override string ToString()
        {
            return _player == Player.White ? "H" : "h";
        }

        public override string PictureName()
        {
            if (_player == Player.White) return "WhiteKnight";
            else return "BlackKnight";
        }

        public static void preaCalc()
        {
            for (var j = 0; j < 64; j++)
            {
                pSteps[j] = new HashSet<int>();
                var x = j >> 3;
                var y = j & 7;
                for (var i = 0; i < 8; i++)
                {
                    if (x + _dx[i] >= 0 && x + _dx[i] < 8 && y + _dy[i] >= 0 && y + _dy[i] < 8)
                    {
                        pSteps[j].Add(((x + _dx[i]) << 3) + y + _dy[i]);
                    }
                }
            }
        }
#endregion

#region private methods

#endregion
    }
}
