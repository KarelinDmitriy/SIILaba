namespace ChessModel
{
	public class Move
	{
		private readonly Board _board;

		#region public methods

		public Move(Step step, Board board)
		{
			_board = board;
			Step = new Step(step);
			oldFrom = _board[(step.FromX << 3) + step.FromY];
			oldTo = _board[(step.ToX << 3) + step.ToY];
		}

		public void DoMove()
		{
			_board[(Step.FromX << 3) + Step.FromY].Move(Step.ToX, Step.ToY);
			_board[(Step.FromX << 3) + Step.FromY] = null;
		}

		public void Rollback()
		{
			_board[(Step.FromX << 3) + Step.FromY] = oldFrom;
			_board[(Step.ToX << 3) + Step.ToY] = oldTo;
		}

		#endregion

		#region variable

		public Step Step;
		public Figure oldFrom;
		public Figure oldTo;

		#endregion

		#region private methods

		#endregion
	}
}