using System.Collections.Generic;

namespace ChessModel
{
	public class Board
	{
		private readonly Stack<Move> _moves = new Stack<Move>();

		public Board()
		{
			for (var i = 0; i < 8; i++)
			{
				new Pawn(Player.White, this, 1, i);
				new Pawn(Player.Black, this, 6, i);
			}
			new Rook(Player.White, this, 0, 0);
			new Rook(Player.Black, this, 7, 0);
			new Knight(Player.White, this, 0, 1);
			new Knight(Player.Black, this, 7, 1);
			new Bishop(Player.White, this, 0, 2);
			new Bishop(Player.Black, this, 7, 2);
			new Queen(Player.White, this, 0, 3);
			new Queen(Player.Black, this, 7, 3);
			new King(Player.White, this, 0, 4);
			new King(Player.Black, this, 7, 4);
			new Bishop(Player.White, this, 0, 5);
			new Bishop(Player.Black, this, 7, 5);
			new Knight(Player.White, this, 0, 6);
			new Knight(Player.Black, this, 7, 6);
			new Rook(Player.White, this, 0, 7);
			new Rook(Player.Black, this, 7, 7);
		}

		public Board(bool isEmpty) //дурацкий конструктор
		{
			
		}

		public Board(Board board)
		{
			for (int i = 0; i < 64; i++)
			{
				if (board.Figures[i] != null)
				{
					board.Figures[i].CopyOnOtherBoard(this);
				}
			}
		}

		public Figure[] Figures { get; set; } = new Figure[64];

		public Figure this[int index]
		{
			get { return Figures[index]; }
			set { Figures[index] = value; }
		}

		public void Move(Step step)
		{
			var move = new Move(step, this);
			_moves.Push(move);
			move.DoMove();
		}

		public void CanselMove()
		{
			var move = _moves.Pop();
			move.Rollback();
		}
	}
}