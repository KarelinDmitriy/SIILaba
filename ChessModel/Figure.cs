using System.Collections.Generic;

namespace ChessModel
{
	public abstract class Figure
	{
		#region protected methods

		protected bool RightMove(int x, int y)
		{
			return _board[(x << 3) + y] == null ||
			       _board[(x << 3) + y].IsEnemy(this);
		}

		#endregion

		#region variable

		public Board _board;
		protected Player _player;

		#endregion

		#region public methods

		protected Figure(Player player, Board board, int cost, int x, int y)
		{
			_player = player;
			_board = board;
			Cost = cost;
			X = x;
			Y = y;
			_board[(x << 3) + y] = this;
		}

		public abstract Figure Move(int newX, int newY);
		public abstract void CopyOnOtherBoard(Board newBoard);

		public abstract List<Step> GetRightMove();
		public abstract bool AttackTarget(Figure f);

		public int Cost { get; }

		public int X { get; private set; }
		public int Y { get; private set; }

		public bool IsEnemy(Figure f)
		{
			return f._player != _player;
		}

		public Player Player => _player;

		public abstract string PictureName();

		#endregion

		#region private methods

		#endregion
	}
}