using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Game
    {
#region variable
        Figure _whiteKing, _blackKing;
        static Figure[,] _board;
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
            
        }

        public State calcState()
        {
            var enemyMoves = getAllRightMoves(getEnemy(_player));
            Figure ownKing;
            if (_player== Player.White) ownKing = _whiteKing;
            else ownKing = _blackKing;
            var kingAlert = enemyMoves
                   .Where(mov => _board[mov.to.x, mov.to.y] == ownKing)
                   .Select(mov => _board[mov.from.x, mov.from.y]).Count();
            if (kingAlert == 0) return State.Calm;
            else
            {
                var count = getAllLegalMoves(_player).Count();
                if (count > 0) return State.Check;
                else return State.Checkmate;
            }
        }

        public IEnumerable<Step> getAllLegalMoves(Player p)
        {
            List<Step> ret = new List<Step>();
            var moves = getAllRightMoves(_player);
            //запоминаем, какого короля мы должна атаковать
            Figure ownKing;
            if (p == Player.White) ownKing = _whiteKing;
            else ownKing = _blackKing;
            foreach (var move in moves)
            {
                //пытаемся делать ход
                Figure fLast = _board[move.to.x, move.to.y]; //фигура, которая убирается с доски (возможно null)
                Figure fStep = _board[move.from.x, move.from.y]; //фигура, которой ходят
                _board[move.to.x, move.to.y] = fStep;
                _board[move.from.x, move.from.y] = null;
                //ход сделали, теперь пытаемя проведить, а не шах ли нам после этого
                var enemyMoves = getAllRightMoves(getEnemy(p));
                //Проверяем, не атакуют ли нас сейчас
                var kingAlert = enemyMoves
                    .Where(mov => _board[mov.to.x, mov.to.y] == ownKing)
                    .Select(mov => _board[mov.from.x, mov.from.y]).Count();
                if (kingAlert == 0) ret.Add(move);
            }
            return ret;
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

        private Player getEnemy(Player p)
        {
            if (p == Player.White) return Player.Black;
            else return Player.White;
        }
#endregion
    }
}
