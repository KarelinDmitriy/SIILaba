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
        static StepFromPosition[] pSteps = new StepFromPosition[64]; //препросчитанные шаги
        #endregion

        #region public methods
        public Queen(Player p, int x, int y)
            : base(p, 900, x, y)
        { }

        public override IEnumerable<Step> GetRightMove()
        {
            List<Step> ret = new List<Step>();
            StepFromPosition from = pSteps[(X << 3) + Y];
            for (int i = 0; i < from.Rays.Length; i++)
            {
                Ray cur = from.Rays[i];
                while (cur != null)
                {
                    if (_board[(cur.step.tx<<3)+cur.step.ty] == null)
                    {
                        ret.Add(cur.step);
                        cur = cur.NextRay;
                    }
                    else
                    {
                        if (_board[(cur.step.tx << 3) + cur.step.ty].Player != _player)
                            ret.Add(cur.step);
                        break;
                    }
                }
            }
            return ret;
        }

        public override bool AttackTarget(Figure f)
        {
            int t = (f.X << 3) + f.Y;
            StepFromPosition p = pSteps[(X << 3) + Y];
            if (p.Attack.ContainsKey(t))
            {
                int r = p.Attack[t];
                Ray cur = p.Rays[r];
                while (cur != null)
                {
                    if (cur.step.tx == f.X && cur.step.ty == f.Y) return true;
                    if (_board[(cur.step.tx << 3) + cur.step.ty] != null) return false;
                    cur = cur.NextRay;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == Player.White ? "Q" : "q";
        }

        public override string PictureName()
        {
            if (_player == Player.White) return "WhiteQueen";
            return "BlackQueen";
        }

        public static void PrecalcStep()
        {
            for (int j=0; j<64; j++)
            {
                StepFromPosition f = new StepFromPosition(8);
                int x = j>>3;
                int y = j & 7;
                for (int i=0; i<8; i++)
                {
                    int nx = x + _dx[i];
                    int ny = y + _dy[i];
                    Ray last = null;
                    if (nx>=0 && nx <8 && ny>=0 && ny<8)
                    {
                        Step s = new Step(x, y, nx, ny);
                        Ray r = new Ray(s);
                        f.Rays[i] = r;
                        last = r;
                        f.Attack.Add((nx << 3) + ny, i);
                    }
                    nx += _dx[i];
                    ny += _dy[i];
                    while (nx >=0 && nx <8 && ny>=0 && ny<8)
                    {
                        Step s = new Step(x, y, nx, ny);
                        f.Attack.Add((nx << 3) + ny, i);
                        Ray n = new Ray(s);
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
