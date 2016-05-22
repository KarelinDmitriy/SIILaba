using System.Collections.Generic;
using System.Linq;

namespace ChessModel
{
	public class Game
	{
		#region variable

		private readonly Figure _whiteKing;
		private readonly Figure _blackKing;
		public Board _board;

		#endregion

		#region public methods

		public Game(Board board)
		{
			_board = board;
			Player = Player.White;
			for (var i = 0; i < 8; i++)
				for (var j = 0; j < 8; j++)
					if (_board[(i << 3) + j] is King)
					{
						if (_board[(i << 3) + j].Player == Player.White)
							_whiteKing = _board[(i << 3) + j];
						else _blackKing = _board[(i << 3) + j];
					}
		}

		public void doMove(Step s)
		{
			_board.Move(s);
			Player = getEnemy(Player);
		}

		public State calcState()
		{
			var ownKing = Player == Player.White ? _whiteKing : _blackKing;
			var kingAlert = countAtacksToFigure(ownKing);
			var count = getAllLegalMoves(Player).Count();
			if (kingAlert == 0)
			{
				return count == 0 ? State.Draw : State.Calm;
			}
			return count > 0 ? State.Check : State.Checkmate;
		}

		public State calcState(Player p)
		{
			var ownKing = p == Player.White ? _whiteKing : _blackKing;
			var kingAlert = countAtacksToFigure(ownKing);
			var count = getAllLegalMoves(Player).Count();
			if (kingAlert == 0)
			{
				return count == 0 ? State.Draw : State.Calm;
			}
			return count > 0 ? State.Check : State.Checkmate;
		}

		public List<Step> getAllLegalMoves(Player p)
		{
			var ret = new List<Step>();
			var moves = getAllRightMoves(p);
			//запоминаем, какого короля мы должны атаковать
			var ownKing = p == Player.White ? _whiteKing : _blackKing;
			foreach (var move in moves)
			{
				//пытаемся делать ход+
				_board.Move(move);
				//ход сделали, теперь пытаемя проведить, а не шах ли нам после этого
				var kingAlert = countAtacksToFigure(ownKing);
				if (kingAlert == 0) ret.Add(move);
				_board.CanselMove();
			}
			return ret;
		}

		public Player Player { get; private set; }

		#endregion

		#region private methods

		public List<Step> getAllRightMoves(Player p)
		{
			var ret = new List<Step>();
			foreach (var x in _board.Figures)
			{
				if (x == null || x.Player != p) continue;
				var IE = x.GetRightMove();
				ret.AddRange(IE);
			}
			return ret;
		}

		private int countAtacksToFigure(Figure f)
		{
			foreach (var x in _board.Figures)
			{
				if (x == null || x.Player == f.Player) continue;
				if (x.AttackTarget(f)) return 1;
			}
			return 0;
		}

		private Player getEnemy(Player p)
		{
			return p == Player.White ? Player.Black : Player.White;
		}

		#endregion
	}
}