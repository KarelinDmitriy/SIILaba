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
        #endregion

        #region public methods
        public Pawn(Player p, int x, int y)
            : base(p, 100, x, y)
        {
            
        }

        public override List<Step> GetRightMove()
        {
            //Пешки отстой, в общую концепию не вписываются(((
            var ret = new List<Step>();
            if (_player == Player.White)
            {
                //считаем, что пешка не можешь быть на позиции х = 7, 
                //тогда любой ход вперед может допустим, если следующая
                //клетка не занята
                if (_board[((X + 1) << 3) + Y] == null)
                {
                    ret.Add(new Step(X, Y, X + 1, Y));
                    if (X == 1 && _board[((X + 2) << 3) + Y] == null)
                    {
                        ret.Add(new Step(X, Y, X + 2, Y));
                    }
                }
                //Теперь обсчитываем взятия (пока считаем, что взятия не проходе нет)
                //опять же предполагаем, что мы не можем находиться на х = 7
                if (((Y - 1) & Int32.MaxValue - 7) == 0
                    && _board[((X + 1) << 3) + (Y - 1)] != null
                    && _board[((X + 1) << 3) + (Y - 1)].Player == Player.Black)
                {
                    ret.Add(new Step(X, Y, X + 1, Y - 1));
                }
                if (((Y + 1) & Int32.MaxValue - 7) == 0
                    && _board[((X + 1) << 3) + (Y + 1)] != null
                    && _board[((X + 1) << 3) + (Y + 1)].Player == Player.Black)
                {
                    ret.Add(new Step(X, Y, X + 1, Y + 1));
                }
            }
            else
            {
                //считаем, что пешка не можешь быть на позиции х = 0, 
                //тогда любой ход вперед может допустим, если следующая
                //клетка не занята
                if (_board[((X - 1) << 3) + Y] == null)
                {
                    ret.Add(new Step(X, Y, X - 1, Y));
                    if (X == 6 && _board[((X - 2) << 3) + Y] == null)
                    {
                        ret.Add(new Step(X, Y, X - 2, Y));
                    }
                }
                //Теперь обсчитываем взятия (пока считаем, что взятия не проходе нет)
                //опять же предполагаем, что мы не можем находиться на х = 0
                if (((Y - 1) & Int32.MaxValue - 7) == 0
                    && _board[((X - 1) << 3) + (Y - 1)] != null
                    && _board[((X - 1) << 3) + (Y - 1)].Player == Player.White)
                {
                    ret.Add(new Step(X, Y, X - 1, Y - 1));
                }
                if (((Y + 1) & Int32.MaxValue - 7) == 0
                    && _board[((X - 1) << 3) + (Y + 1)] != null
                    && _board[((X - 1) << 3) + (Y + 1)].Player == Player.White)
                {
                    ret.Add(new Step(X, Y, X - 1, Y + 1));
                }
            }
            return ret;
        }

        public override bool AttackTarget(Figure f)
        {
            if (_player == Player.White)
            {
                if ((((X + 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y + 1) & (int.MaxValue - 7)) == 0)
                {
                    if (ReferenceEquals(_board[((X + 1) << 3) + Y + 1], f)) return true;
                }
                if ((((X + 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y - 1) & (int.MaxValue - 7)) == 0)
                {
                    if (ReferenceEquals(_board[((X + 1) << 3) + Y - 1], f)) return true;
                }
            }
            else
            {
                if ((((X - 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y + 1) & (int.MaxValue - 7)) == 0)
                {
                    if (ReferenceEquals(_board[((X - 1) << 3) + Y + 1], f)) return true;
                }
                if ((((X - 1) & (int.MaxValue - 7)) == 0) &&
                       ((Y - 1) & (int.MaxValue - 7)) == 0)
                {
                    if (_board[((X - 1) << 3) + Y - 1] == f) return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return _player == Player.White ? "P" : "p";
        }

        public override string PictureName()
        {
            if (_player == Player.White) return "WhitePawn";
            else return "BlackPawn";
        }
        #endregion

        #region private methods

        #endregion
    }
}
