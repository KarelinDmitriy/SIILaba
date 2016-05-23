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
        static StepFromPosition[] pSteps = new StepFromPosition[64]; //препросчитанные шаги
#endregion 

#region public methods
        public Bishop(Player p, Board board, int x, int y)
            :base(p, board, 300, x, y)
        {

        }

	    public override Figure Move(int newX, int newY)
	    {
		    return new Bishop(Player, _board, newX, newY);
	    }

	    public override void CopyOnOtherBoard(Board newBoard)
	    {
		    new Bishop(Player, newBoard, X, Y);
	    }

	    public override List<Step> GetRightMove()
        {
            var ret = new List<Step>();
            var from = pSteps[(X << 3) + Y];
            for (var i = 0; i < from.Rays.Length; i++)
            {
                var cur = from.Rays[i];
                while (cur != null)
                {
                    if (_board[(cur.step.ToX << 3) + cur.step.ToY] == null)
                    {
                        ret.Add(cur.step);
                        cur = cur.NextRay;
                    }
                    else
                    {
                        if (_board[(cur.step.ToX << 3) + cur.step.ToY].Player != _player)
                            ret.Add(cur.step);
                        break;
                    }
                }
            }
            return ret;
        }

        public override bool AttackTarget(Figure f)
        {
            var t = (f.X << 3) + f.Y;
            var p = pSteps[(X << 3) + Y];
            if (p.Attack.ContainsKey(t))
            {
                var r = p.Attack[t];
                var cur = p.Rays[r];
                while (cur != null)
                {
                    if (cur.step.ToX == f.X && cur.step.ToY == f.Y) return true;
                    if (_board[(cur.step.ToX << 3) + cur.step.ToY] != null) return false;
                    cur = cur.NextRay;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == Player.White ? "B" : "b";
        }

        public override string PictureName()
        {
            return _player == Player.Black ? "BlackBishop" : "WhiteBishop";
        }

        public static void PrecalcStep()
        {
            for (var j = 0; j < 64; j++)
            {
                var f = new StepFromPosition(4);
                var x = j >> 3;
                var y = j & 7;
                for (var i = 0; i < 4; i++)
                {
                    var nx = x + _dx[i];
                    var ny = y + _dy[i];
                    Ray last = null;
                    if (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
                    {
                        var s = new Step(x, y, nx, ny);
                        var r = new Ray(s);
                        f.Rays[i] = r;
                        last = r;
                        f.Attack.Add((nx << 3) + ny, i);
                    }
                    nx += _dx[i];
                    ny += _dy[i];
                    while (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
                    {
                        var s = new Step(x, y, nx, ny);
                        f.Attack.Add((nx << 3) + ny, i);
                        var n = new Ray(s);
                        last.NextRay = n;
                        last = n;
                        nx += _dx[i];
                        ny += _dy[i];
                    }
                }
                pSteps[j] = f;
            }
        }
#endregion

#region private methods

#endregion
    }
}
