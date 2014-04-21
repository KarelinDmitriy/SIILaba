using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public abstract class Figure
    {
#region variable
        public static Figure[,] _board;
        protected static Game _game;
        private int _cost;
        protected ChessPoint _point;
        protected Player _player;
#endregion 

#region public methods
        public Figure(Player player, int cost, int x, int y)
        {
            _player = player;
            _cost = cost;
            _point = new ChessPoint(x, y);
            _board[x, y] = this;
        }

        public abstract IEnumerable<Step> getRightMove();

        public int Cost
        {
            get { return _cost; }
        }

        public int X
        {
            get { return _point.x; }
            set { _point.x = value; }
        }

        public int Y
        {
            get { return _point.y;}
            set { _point.y= value; }
        }

        public bool isEnemy(Figure f)
        {
            return f._player != _player;
        }

        public Player Player
        {
            get { return _player; }
        }
#endregion

#region private methods

#endregion

#region protected methods
        protected bool RightMove(ChessPoint to)
        {
            if (_board[to.x, to.y] == null ||
                _board[to.x, to.y].isEnemy(this))
            {
                return true;
            }
            return false;
        }
#endregion 
    }
}
