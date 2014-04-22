using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Pawn : Figure
    {
#region variable
        bool _doStep;
#endregion 

#region public methods
        public Pawn(Player p, int x, int y)
            : base(p, 50, x, y)
        {
            _doStep = false;
        }

        public override IEnumerable<Step> getRightMove()
        {
            //Пешки отстой, в общую концепию не вписываются(((
            List<Step> ret = new List<Step>();
            if (_player == Player.White)
            {
                if (((X+1) & (int.MaxValue - 7)) == 0) 
                {
                    if (_board[((X+1)<<3) + Y] == null)
                    {
                        ret.Add(new Step(X, Y, X + 1, Y));
                        if (!_doStep && _board[((X+2)<<3) + Y] == null)
                        {
                                ret.Add(new Step(X, Y, X+2, Y));
                        }
                    }
                } //if X+1 in board
                if ((((X + 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y + 1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X+1)<<3) + Y+1] != null && _board[((X+1)<<3) +  Y+1].isEnemy(this))
                    {
                        ret.Add(new Step(X, Y, X+1, Y+1));
                    }
                }
                if ((((X +1) & (int.MaxValue - 7)) == 0) &&
                       ((Y -1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X + 1)<<3) + Y - 1] != null && _board[((X + 1)<<3) +  Y - 1].isEnemy(this))
                    {
                        ret.Add(new Step(X, Y, X+1,Y-1));
                    }
                }
            }
            else 
            {
                if (((X -1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X-1)<<3) + Y ] == null)
                    {
                            ret.Add(new Step(X, Y, X-1, Y));
                        if (!_doStep && _board[((X-2)<<3) + Y] == null)
                        {
                                ret.Add(new Step(X, Y, X-2, Y));
                        }
                    }
                } //if X+1 in board
                if ((((X - 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y +  1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X - 1)<<3) + Y + 1] != null && _board[((X - 1)<<3) + Y + 1].isEnemy(this))
                    {
                        ret.Add(new Step(X,Y, X-1, Y+1));
                    }
                }
                if ((((X -1 ) & (int.MaxValue - 7)) == 0) &&
                       ((Y - 1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X - 1)<<3) + Y - 1] != null && _board[((X - 1)<<3) + Y - 1].isEnemy(this))
                    {
                        ret.Add(new Step(X, Y, X-1, Y-1));
                    }
                }
            }
            return ret;
        }

        public override bool attackTarget(Figure f)
        {
            if (_player == ChessModel.Player.White)
            {
                if ((((X + 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y + 1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X + 1)<<3) + Y + 1] == f) return true;
                }
                if ((((X + 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y -1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X + 1)<<3) + Y - 1] == f) return true;
                }
            }
            else
            {
                if ((((X - 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y + 1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X - 1)<<3) + Y + 1] == f) return true;
                }
                if ((((X - 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y - 1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X - 1)<<3) +  Y - 1] == f) return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player==ChessModel.Player.White ? "P" : "p";
        }
#endregion

#region private methods

#endregion
    }
}
