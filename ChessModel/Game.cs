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
        public static Figure[,] _board;
        Player _player;
#endregion 

#region public methods
        public Game()
        {
            _player = Player.White;
            for (int i=0; i<8; i++)
                for (int j=0; j<8; j++)
                    if (_board[i,j] is King)
                    {
                        if (_board[i, j].Player == Player.White)
                            _whiteKing = _board[i, j];
                        else _blackKing = _board[i, j];
                    }
        }

        public void doMove(Step s)
        {
            var steps = getAllLegalMoves(_player);
            var yes = steps.FirstOrDefault(x => x.from.x == s.from.x &&
                                                x.from.y == s.from.y &&
                                                x.to.y == s.to.y &&
                                                x.to.x == s.to.x);
            if (yes == null)
                throw new ErrorStepExveption("Не возможно совершить ход");
            _board[s.to.x, s.to.y] = _board[s.from.x, s.from.y];
            _board[s.from.x, s.from.y] = null;
            _board[s.to.x, s.to.y].X = s.to.x;
            _board[s.to.x, s.to.y].Y = s.to.y;
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
            var moves = getAllRightMoves(_player);
            //запоминаем, какого короля мы должны атаковать
            Figure ownKing;
            if (p == Player.White) ownKing = _whiteKing;
            else ownKing = _blackKing;
            foreach (var move in moves)
            {
                //пытаемся делать ход+
                Figure fLast = _board[move.to.x, move.to.y]; //фигура, которая убирается с доски (возможно null)
                Figure fStep = _board[move.from.x, move.from.y]; //фигура, которой ходят
                _board[move.to.x, move.to.y] = fStep;
                _board[move.from.x, move.from.y] = null;
                //ход сделали, теперь пытаемя проведить, а не шах ли нам после этого
                var kingAlert = countAtacksToFigure(ownKing);
                if (kingAlert == 0) ret.Add(move);
                _board[move.to.x, move.to.y] = fLast;
                _board[move.from.x, move.from.y] = fStep;
            }
            return ret;
        }

        public Player Player
        {
            get { return _player; }
        }
#endregion

#region private methods

        IEnumerable<Step> getAllRightMoves(Player p)
        {
            List<Step> ret = new List<Step>();
            foreach (var x in _board)
            {
                if (x != null && x.Player==p)
                {
                    var IE = x.getRightMove();
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
                    if (x.attackTarget(f)) count++;
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
