﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Rook : Figure
    {
#region variable
        static readonly int[] _dx = {0,0,1,-1};
        static readonly int[] _dy = {1,-1, 0,0};
#endregion 

#region public methods
        public Rook(Player p, int x, int y)
            :base (p, 300, x, y)
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

        public override bool attackTarget(Figure f)
        {
            for (int i = 0; i < 4; i++)
            {
                int j = 1;
                while (ChessPoint.PointInBoard(X + _dx[i] * j, Y + _dy[i] * j))
                {
                    ChessPoint to = new ChessPoint(X + _dx[i] * j, Y + _dy[i] * j);
                    if (_board[to.x, to.y] == f) return true;
                    if (_board[to.x, to.y] != null)
                        break;
                    j++;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == ChessModel.Player.White ? "R" : "r";
        }
#endregion

#region private methods

#endregion
    }
}
