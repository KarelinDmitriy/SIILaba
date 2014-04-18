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
                if (ChessPoint.PointInBoard(X, Y+1))
                {
                    if (_board[X, Y+1] == null)
                    {
                         ret.Add(new Step(
                                new ChessPoint(X, Y),
                                new ChessPoint(X , Y+ 1)
                                ));
                        if (!_doStep && _board[X, Y+2] == null)
                        {
                                ret.Add(new Step(
                                    new ChessPoint(X, Y),
                                    new ChessPoint(X , Y+ 2)
                                    ));
                        }
                    }
                } //if X+1 in board
                if (ChessPoint.PointInBoard(X+1, Y+1))
                {
                    if (RightMove(new ChessPoint(X+1, Y+1)))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X + 1, Y + 1)));
                    }
                }
                if (ChessPoint.PointInBoard(X-1, Y+1))
                {
                    if (RightMove(new ChessPoint(X - 1, Y + 1)))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X - 1, Y + 1)));
                    }
                }
            }
            else 
            {
                if (ChessPoint.PointInBoard(X, Y - 1))
                {
                    if (_board[X, Y - 1] == null)
                    {
                            ret.Add(new Step(
                                new ChessPoint(X, Y),
                                new ChessPoint(X, Y - 1)
                                ));
                        if (!_doStep && _board[X, Y - 2] == null)
                        {
                                ret.Add(new Step(
                                    new ChessPoint(X, Y),
                                    new ChessPoint(X, Y - 2)
                                    ));
                        }
                    }
                } //if X+1 in board
                if (ChessPoint.PointInBoard(X + 1, Y - 1))
                {
                    if (RightMove(new ChessPoint(X + 1, Y - 1)))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X + 1, Y - 1)));
                    }
                }
                if (ChessPoint.PointInBoard(X - 1, Y - 1))
                {
                    if (RightMove(new ChessPoint(X - 1, Y - 1)))
                    {
                        ret.Add(new Step(
                            new ChessPoint(X, Y),
                            new ChessPoint(X - 1, Y - 1)));
                    }
                }
            }
            return ret;
        }
#endregion

#region private methods

#endregion
    }
}
