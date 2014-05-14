﻿using System;
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
            : base(p, 300, x,y)
        {

        }

        public override IEnumerable<Step> getRightMove()
        {
            List<Step> ret = new List<Step>();
            for (int i = 0; i < 8; i++)
            {
                if ((((X + _dx[i] ) & (int.MaxValue - 7)) == 0) &&
                         ((Y + _dy[i] ) & (int.MaxValue - 7)) == 0)
                {
                    int nc = ((X + _dx[i])<<3) + Y + _dy[i];
                    if (_board[nc]==null || _board[nc].isEnemy(this))
                    {
                        ret.Add(new Step(X, Y, nc/8, nc%8));
                    }
                }
            }
            return ret;
        }

        public override bool attackTarget(Figure f)
        {
            for (int i = 0; i < 8; i++)
            {
                if ((((X + _dx[i]) & (int.MaxValue - 7)) == 0) &&
                         ((Y + _dy[i]) & (int.MaxValue - 7)) == 0)
                {
                    if (ReferenceEquals(_board[((X + _dx[i])<<3) + Y + _dy[i]], f)) return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == ChessModel.Player.White ? "H" : "h";
        }

        public override string PictureName()
        {
            if (_player == ChessModel.Player.White) return "WhiteKnight";
            else return "BlackKnight";
        }
#endregion

#region private methods

#endregion
    }
}
