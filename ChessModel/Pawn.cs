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
                if (ChessPoint.PointInBoard(X+1, Y))
                {
                    if (_board[X+1, Y] == null)
                    {
                         ret.Add(new Step(
                                new ChessPoint(X, Y),
                                new ChessPoint(X+1 , Y)
                                ));
                        if (!_doStep && _board[X+2, Y] == null)
                        {
                                ret.Add(new Step(
                                    new ChessPoint(X, Y),
                                    new ChessPoint(X+2 , Y)
                                    ));
                        }
                    }
                } //if X+1 in board
                if (ChessPoint.PointInBoard(X+1, Y+1))
                {
                    if (_board[X+1, Y+1] != null && _board[X+1, Y+1].isEnemy(this))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X + 1, Y + 1)));
                    }
                }
                if (ChessPoint.PointInBoard(X+1, Y-1))
                {
                    if (_board[X + 1, Y - 1] != null && _board[X + 1, Y - 1].isEnemy(this))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X + 1, Y - 1)));
                    }
                }
            }
            else 
            {
                if (ChessPoint.PointInBoard(X-1, Y))
                {
                    if (_board[X-1, Y ] == null)
                    {
                            ret.Add(new Step(
                                new ChessPoint(X, Y),
                                new ChessPoint(X-1, Y)
                                ));
                        if (!_doStep && _board[X-2, Y] == null)
                        {
                                ret.Add(new Step(
                                    new ChessPoint(X, Y),
                                    new ChessPoint(X-2, Y)
                                    ));
                        }
                    }
                } //if X+1 in board
                if (ChessPoint.PointInBoard(X - 1, Y + 1))
                {
                    if (_board[X - 1, Y + 1] != null && _board[X - 1, Y + 1].isEnemy(this))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X - 1, Y + 1)));
                    }
                }
                if (ChessPoint.PointInBoard(X - 1, Y - 1))
                {
                    if (_board[X - 1, Y - 1] != null && _board[X - 1, Y - 1].isEnemy(this))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X - 1, Y - 1)));
                    }
                }
            }
            return ret;
        }

        public override bool attackTarget(Figure f)
        {
            if (_player == ChessModel.Player.White)
            {
                if (ChessPoint.PointInBoard(X + 1, Y + 1))
                {
                    if (_board[X + 1, Y + 1] == f) return true;
                }
                if (ChessPoint.PointInBoard(X + 1, Y - 1))
                {
                    if (_board[X + 1, Y - 1] == f) return true;
                }
            }
            else
            {
                if (ChessPoint.PointInBoard(X - 1, Y + 1))
                {
                    if (_board[X - 1, Y + 1] == f) return true;
                }
                if (ChessPoint.PointInBoard(X - 1, Y - 1))
                {
                    if (_board[X - 1, Y - 1] == f) return true;
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
