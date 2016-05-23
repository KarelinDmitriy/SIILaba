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
        static HashSet<int>[] pSteps = new HashSet<int>[64]; //препросчитанные шаги
        #endregion

        #region public methods
        public King(Player p, Board board, int x, int y)
            : base(p, board, 10000, x, y)
        {

        }

	    public override Figure Move(int newX, int newY)
	    {
			return new King(Player, _board, newX, newY);
	    }

	    public override void CopyOnOtherBoard(Board newBoard)
	    {
		    new King(Player, newBoard, X, Y);
	    }

	    public override List<Step> GetRightMove()
        {
            var ret = new List<Step>();
            for (var i = 0; i < 8; i++)
            {
                if ((((X + _dx[i]) & (int.MaxValue - 7)) == 0) &&
                       ((Y + _dy[i]) & (int.MaxValue - 7)) == 0)
                {
                    if (RightMove(X + _dx[i], Y + _dy[i]))
                    {
                        ret.Add(new Step(X, Y, X + _dx[i], Y + _dy[i]));
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
            return _player == Player.White ? "K" : "k";
        }

        public override string PictureName()
        {
            if (_player == Player.Black) return "BlackKing";
            else return "WhiteKing";
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
