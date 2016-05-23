using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq;

namespace ChessModel
{
    public sealed class AI
    {
        #region variable

	    const int ThreadCount = 4;
	    private int maxDeep;

		private readonly List<StepWithScore> _threadsResults = new List<StepWithScore>();

	    public long Count;
        #endregion

        #region public methods
        public AI()
        {
        }

        //выбирает ход для выбранного игрока с глубиной не более maxDeep
        public Step SelectMove(Player p, int maxDeep, Board board)
        {
			Count = 0;
	        this.maxDeep = maxDeep;
	        var game = new Game(board);
            var moves = game.getAllLegalMoves(p);
	        var b = moves.Batch(moves.Count/ThreadCount).ToList();
	        var thraads = new Thread[b.Count];
	        var n = 0;
	        foreach (var curMoves in b)
	        {
		        var args = new TheadArg
		        {
					board = new Board(board),
					startPlayer = p,
					steps = curMoves.ToList()
				};
		        var thread = new Thread(Process) {IsBackground = true};
				thread.Start(args);
		        thraads[n++] = thread;
	        }
	        while (thraads.Any(x => x.IsAlive))
	        {
		        Thread.Sleep(100);
	        }
	        var res = _threadsResults.MaxBy(x => x.Score);
	        return res.Step;
        }
        #endregion

        #region private methods

	    private void Process(object obj)
	    {
		    var args = obj as TheadArg;
			var game = new Game(args.board);
		    var bMoves = new int[args.steps.Count];
			if (args.startPlayer == Player.White)
			{
				var i = 0;
				foreach (var x in args.steps)
				{
					DoMove(x, args.board);
					var res = AlphaBetaBlackWS(int.MinValue, Int32.MaxValue, maxDeep - 1, game);
					BackMove(args.board);
					bMoves[i++] = res;
				}
			}
			else
			{
				var i = 0;
				foreach (var x in args.steps)
				{
					DoMove(x, args.board);
					int res = AlphaBetaWhiteBS(int.MinValue, Int32.MaxValue, maxDeep - 1, game);
					BackMove(args.board);
					bMoves[i++] = res;
				}
			}
			var max = bMoves.Max();
		    var arrMoves = args.steps;
			var bestSteps = new List<Step>();
			for (var i = 0; i < bMoves.Length; i++)
			{
				if (bMoves[i] == max)
					bestSteps.Add(arrMoves[i]);
			}
			var rand = new Random();
			this._threadsResults.Add(new StepWithScore
			{
				Score = max,
				Step = bestSteps[rand.Next() % bestSteps.Count]
			});
		}

        #region White Step First

        private int AlphaBetaWhiteWS(int alpha, int beta, int depth, Game game)
        {
            Count++;
            var max = Int32.MinValue;
            if (depth <= 0) return calculateScoreWhite(game._board) - calculateScoreBlack(game._board);
            var moves = game.getAllLegalMoves(Player.White);
            if (moves.Count() != 0)
                foreach (var x in moves)
                {
                    DoMove(x, game._board);
                    var tmp = AlphaBetaBlackWS(alpha, beta, depth - 1, game);
                    BackMove(game._board);
                    if (tmp > max) max = tmp;
                    if (tmp > alpha) alpha = tmp;
                    if (max >= beta) return max;
                }
            else
            {
                var state = game.calcState();
                if (state == State.Checkmate)
                    return 0;
                else return -10000;
            }
            return max;
        }

        private int AlphaBetaBlackWS(int alpha, int beta, int depth, Game game)
        {
            Count++;
            var min = int.MaxValue;
            if (depth <= 0) return calculateScoreBlack(game._board) - calculateScoreWhite(game._board);
            var moves = game.getAllLegalMoves(Player.Black);
            if (moves.Count() != 0)
                foreach (var x in moves)
                {
                    DoMove(x, game._board);
                    var tmp = AlphaBetaWhiteWS(alpha, beta, depth - 1, game);
                    BackMove(game._board);
                    if (tmp < min) min = tmp;
                    if (tmp < beta) beta = tmp;
                    if (min <= alpha) return min;
                }
            else
            {
                var state = game.calcState();
                return state == State.Checkmate ? 0 : 10000;
            }
            return min;
        }

        #endregion

        #region Black Step First

        private int AlphaBetaWhiteBS(int alpha, int beta, int depth, Game game)
        {
            Count++;
            var min = Int32.MaxValue;
            if (depth <= 0)
            {
                var state = game.calcState(Player.White);
                switch (state)
                {
                    case State.Calm:
                        return calculateScoreWhite(game._board) - calculateScoreBlack(game._board);
                        break;
                    case State.Check:
                        return calculateScoreWhite(game._board) - calculateScoreBlack(game._board) - 300 ;
                        break;
                    case State.Checkmate:
                        return Int32.MinValue;
                        break;
                    case State.Draw:
                        return 0;
                        break;
                }
            }
            var moves = game.getAllLegalMoves(Player.White);
            foreach (var x in moves)
            {
                DoMove(x, game._board);
                var tmp = AlphaBetaBlackBS(alpha, beta, depth - 1, game);
                BackMove(game._board);
                if (tmp < min) min = tmp;
                if (tmp < beta) beta = tmp;
                if (min <= alpha) return min;
            }
            return min;
        }

        private int AlphaBetaBlackBS(int alpha, int beta, int depth, Game game)
        {
            Interlocked.Increment(ref Count);
            var max = Int32.MinValue;
            if (depth <= 0)
            {
                var state = game.calcState(Player.White);
                switch (state)
                {
                    case State.Calm:
                        return calculateScoreBlack(game._board) - calculateScoreWhite(game._board);
                    case State.Check:
                        return calculateScoreBlack(game._board)- 300-calculateScoreWhite(game._board);
                    case State.Checkmate:
                        return Int32.MaxValue;
                    case State.Draw:
                        return 0;
                }
            }
            var moves = game.getAllLegalMoves(Player.Black);
            foreach (var x in moves)
            {
                DoMove(x, game._board);
                var tmp = AlphaBetaWhiteBS(alpha, beta, depth - 1, game);
                BackMove(game._board);
                if (tmp > max) max = tmp;
                if (tmp > alpha) alpha = tmp;
                if (max >= beta) return max;
            }
            return max;
        }

        #endregion


        //посчитать счет белых (пока не самым оптимальный способ)
        static int calculateScoreWhite(Board board)
        {
            var sum = 0;
            foreach (var figure in board.Figures)
            {
	            if (figure != null && figure.Player == Player.White)
		            sum += figure.Cost;
            }
	        return sum;
        }

        //посчитать счет черных (пока не самым оптимальный способ)
        static int calculateScoreBlack(Board board)
        {
            var sum = 0;
            foreach (var figure in board.Figures)
            {
	            if (figure != null && figure.Player == Player.Black)
		            sum += figure.Cost;
            }
	        return sum;
        }

        //делает ход и заносит в стек информацию
        //о том, что необходимо для его отмены 
        static void DoMove(Step step, Board board)
        {
           board.Move(step);
        }

        //отменяет последний ход в стеке
        void BackMove(Board board)
        {
            board.CanselMove();
        }

        #endregion
    }

	public class TheadArg
	{
		public Board board;
		public List<Step> steps;
		public Player startPlayer;
	}

	public class StepWithScore
	{
		public Step Step;
		public long Score;
	}
}
