using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Game
    {
#region variable
        Figure _whiteKing, _blackKing;
        public static Figure[] _board;
        Player _player;
#endregion 

#region public methods
        public Game()
        {
            _player = Player.White;
            for (int i=0; i<8; i++)
                for (int j=0; j<8; j++)
                    if (_board[(i<<3)+j] is King)
                    {
                        if (_board[(i<<3)+j].Player == Player.White)
                            _whiteKing = _board[(i<<3)+j];
                        else _blackKing = _board[(i<<3)+j];
                    }
        }

        public void doMove(Step s)
        {
            var steps = getAllLegalMoves(_player);
            var yes = steps.FirstOrDefault(x => x.fx == s.fx &&
                                                x.fy == s.fy &&
                                                x.tx == s.tx &&
                                                x.ty == s.ty);
             if (yes == null)
                throw new ErrorStepExveption("Не возможно совершить ход");
            _board[(s.tx<<3)+s.ty] = _board[(s.fx<<3)+s.fy];
            _board[(s.fx<<3)+s.fy] = null;
            if (_board[(s.tx << 3) + s.ty] is Pawn)
            {
                if (_board[(s.tx << 3) + s.ty].Player == Player.White && s.tx == 7)
                    new Queen(Player.White, s.tx, s.ty);
                else if ((_board[(s.tx << 3) + s.ty].Player == Player.Black && s.tx == 0))
                    new Queen(Player.Black, s.tx, s.ty);
                else
                {
                    _board[(s.tx << 3) + s.ty].X = s.tx;
                    _board[(s.tx << 3) + s.ty].Y = s.ty;
                }
            }
            else
            {
                _board[(s.tx << 3) + s.ty].X = s.tx;
                _board[(s.tx << 3) + s.ty].Y = s.ty;
            }
            _player = getEnemy(_player);
        }

        public State calcState()
        {
            Figure ownKing;
            if (_player== Player.White) ownKing = _whiteKing;
            else ownKing = _blackKing;
            int kingAlert = countAtacksToFigure(ownKing);
            var count = getAllLegalMoves(_player).Count();
            if (kingAlert == 0)
            {
                if (count == 0) return State.Draw;
                return State.Calm;
            }
            else
            {
                if (count > 0) return State.Check;
                else return State.Checkmate;
            }
        }

        public IEnumerable<Step> getAllLegalMoves(Player p)
        {
            List<Step> ret = new List<Step>();
            var moves = getAllRightMoves(p);
            //запоминаем, какого короля мы должны атаковать
            Figure ownKing;
            if (p == Player.White) ownKing = _whiteKing;
            else ownKing = _blackKing;
            foreach (var move in moves)
            {
                //пытаемся делать ход+
                Debug.Assert(ownKing != null);
                Figure fLast = _board[(move.tx<<3)+move.ty]; //фигура, которая убирается с доски (возможно null)
                Figure fStep = _board[(move.fx<<3)+move.fy]; //фигура, которой ходят
                _board[(move.tx<<3)+move.ty] = fStep;
                _board[(move.fx<<3)+move.fy] = null;
                fStep.X = move.tx;
                fStep.Y = move.ty;
                //ход сделали, теперь пытаемя проведить, а не шах ли нам после этого
                var kingAlert = countAtacksToFigure(ownKing);
                if (kingAlert == 0) ret.Add(move);
                _board[(move.tx<<3)+move.ty] = fLast;
                _board[(move.fx<<3)+move.fy] = fStep;
                fStep.X = move.fx;
                fStep.Y = move.fy;
            }
            return ret;
        }

        public Player Player
        {
            get { return _player; }
        }
#endregion

#region private methods

        public IEnumerable<Step> getAllRightMoves(Player p)
        {
            List<Step> ret = new List<Step>();
            foreach (var x in _board)
            {
                if (x != null && x.Player==p)
                {
                    var IE = x.GetRightMove();
                    ret.AddRange(IE);
                }
            }
            return ret;
        }

        int countAtacksToFigure(Figure f)
        {
            int count = 0;
            foreach (var x in _board)
            {
                if (x !=null && x.Player!=f.Player)
                {
                    if (x.AttackTarget(f)) return 1;
                }
            }
            return count;
        }

        private Player getEnemy(Player p)
        {
            if (p == Player.White) return Player.Black;
            else return Player.White;
        }
#endregion
    }
}
