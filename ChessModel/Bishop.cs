﻿using System;
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
                while ((((X + _dx[i] * j) & (int.MaxValue - 7)) == 0) &&
                       ((Y + _dy[i] * j) & (int.MaxValue - 7)) == 0)
                {
                    {
                        int nx = X + _dx[i] * j;
                        int ny = Y + _dy[i] * j;
                        if (_board[(nx<<3) + ny] != null)
                        {
                            if (_board[(nx<<3) + ny].Player ==_player)
                                ret.Add(new Step(X, Y, nx, ny));
                            break;
                        }
                        ret.Add(new Step(X, Y, nx, ny));
                        j++;
                    }
                }
            }
            return ret;
        }

        public override bool attackTarget(Figure f)
        {
            for (int i = 0; i < 4; i++)
            {
                int j = 1;
                while ((((X + _dx[i] * j) & (int.MaxValue - 7)) == 0) &&
                        ((Y + _dy[i] * j) & (int.MaxValue - 7)) == 0)
                {
                    int nx = X + _dx[i] * j;
                    int ny = Y + _dy[i] * j;
                    if (_board[(nx<<3) + ny] == f) return true;
                    if (_board[(nx<<3) + ny] != null)
                        break;
                    j++;
                }
            }
            return false;
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
