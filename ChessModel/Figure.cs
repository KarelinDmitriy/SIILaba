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
        public static Figure[] _board;
        protected static Game _game;
        private int _cost;
        protected Player _player;
#endregion 

#region public methods
        public Figure(Player player, int cost, int x, int y)
        {
            _player = player;
            _cost = cost;
            X = x;
            Y = y;
            _board[(x<<3) + y] = this;
        }

        public abstract IEnumerable<Step> getRightMove();
        public abstract bool attackTarget(Figure f);

        public int Cost
        {
            get { return _cost; }
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
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
        protected bool RightMove(int x, int y)
        {
            if (_board[(x<<3)+y] == null ||
                _board[(x<<3)+y].isEnemy(this))
            {
                return true;
            }
            return false;
        }
#endregion 
    
        
    }
}
