using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;
using System.Diagnostics;

namespace ChessTest
{
    [TestClass]
    public class TestTime
    {
        [TestMethod]
        public void FirstStep()
        {
            var board = new Board(true);
            Queen.PrecalcStep();
            Bishop.PrecalcStep();
            Rook.PrecalcStep();
            King.preaCalc();
            Knight.preaCalc();
            //Создаем пешек
            for (var i=0; i<8; i++)
            {
                new Pawn(Player.White, board, 1, i);
                new Pawn(Player.Black, board, 6, i);
            }
            new Rook(Player.White, board, 0, 0);
            new Rook(Player.Black, board, 7, 0);
            new Knight(Player.White, board, 0, 1);
            new Knight(Player.Black, board, 7, 1);
            new Bishop(Player.White, board, 5, 2);
            new Bishop(Player.Black, board, 7, 2);
            new Queen(Player.White, board, 3, 3);
            new Queen(Player.Black, board, 4, 3);
            new King(Player.White, board, 0, 4);
            new King(Player.Black, board, 7, 4);
            new Bishop(Player.White, board, 0, 5);
            new Bishop(Player.Black, board, 7, 5);
            new Knight(Player.White, board, 0, 6);
            new Knight(Player.Black, board, 7, 6);
            new Rook(Player.White, board, 2, 7);
            new Rook(Player.Black, board, 5, 7);

            for (var i = 0; i < 100000; i++)
            {
                var g = new Game(board);
                var steps = g.getAllLegalMoves(Player.White);
                var a = steps.Count();
                a++;
            }
        }

        [TestMethod]
        public void FirstStepState()
        {
            var board = new Board(true);
			Queen.PrecalcStep();
			Bishop.PrecalcStep();
			Rook.PrecalcStep();
			King.preaCalc();
			Knight.preaCalc();
			//Создаем пешек
			for (var i = 0; i < 8; i++)
            {
                new Pawn(Player.White, board, 1, i);
                new Pawn(Player.Black, board, 6, i);
            }
            new Rook(Player.White, board, 0, 0);
            new Rook(Player.Black, board, 7, 0);
            new Knight(Player.White, board, 0, 1);
            new Knight(Player.Black, board, 7, 1);
            new Bishop(Player.White, board, 0, 2);
            new Bishop(Player.Black, board, 7, 2);
            new Queen(Player.White, board, 3, 3);
            new Queen(Player.Black, board, 4, 3);
            new King(Player.White, board, 0, 4);
            new King(Player.Black, board, 7, 4);
            new Bishop(Player.White, board, 0, 5);
            new Bishop(Player.Black, board, 7, 5);
            new Knight(Player.White, board, 0, 6);
            new Knight(Player.Black, board, 7, 6);
            new Rook(Player.White, board, 2, 7);
            new Rook(Player.Black, board, 5, 7);

            for (long  i = 0; i < 100000; i++)
            {
                var g = new Game(board);
                var s = g.calcState();
            }

//            Assert.AreEqual(s, State.Calm);

        }

        [TestMethod]
        public void time1()
        {
            var board = new Board(true);
            Queen.PrecalcStep();
            Bishop.PrecalcStep();
            Rook.PrecalcStep();
            King.preaCalc();
            Knight.preaCalc();
            //Создаем пешек
            for (var i = 0; i < 8; i++)
            {
                new Pawn(Player.White, board, 1, i);
                new Pawn(Player.Black, board, 6, i);
            }
            new Rook(Player.White, board, 0, 0);
            new Rook(Player.Black, board, 7, 0);
            new Knight(Player.White, board, 0, 1);
            new Knight(Player.Black, board, 7, 1);
            new Bishop(Player.White, board, 5, 2);
            new Bishop(Player.Black, board, 7, 2);
            new Queen(Player.White, board, 3, 3);
            new Queen(Player.Black, board, 4, 3);
            new King(Player.White, board, 0, 4);
            new King(Player.Black, board, 7, 4);
            new Bishop(Player.White, board, 0, 5);
            new Bishop(Player.Black, board, 7, 5);
            new Knight(Player.White, board, 0, 6);
            new Knight(Player.Black, board, 7, 6);
            new Rook(Player.White, board, 2, 7);
            new Rook(Player.Black, board, 5, 7);

            var ai = new AI();

            var g = new Game(board);
            var timer = new Stopwatch();
            timer.Start();
            g.doMove(new Step(1, 3, 2, 3));

            var x = ai.SelectMove(Player.Black, 6, board);
            timer.Stop();
            Assert.AreEqual(1000, timer.ElapsedMilliseconds);
            Assert.AreEqual(10000, ai.Count);

        }

        [TestMethod]
        public void time2()
        {
            var board = new Board(true);
            Queen.PrecalcStep();
            Bishop.PrecalcStep();
            Rook.PrecalcStep();
            King.preaCalc();
            Knight.preaCalc();
            //Создаем пешек
            //Создаем пешек
            for (var i = 0; i < 8; i++)
            {
                new Pawn(Player.White, board, 1, i);
                new Pawn(Player.Black, board, 6, i);
            }
            new Rook(Player.White, board, 0, 0);
            new Rook(Player.Black, board, 7, 0);
            new Knight(Player.White, board, 0, 1);
            new Knight(Player.Black, board, 7, 1);
            new Bishop(Player.White, board, 0, 2);
            new Bishop(Player.Black, board, 7, 2);
            new Queen(Player.White, board, 0, 3);
            new Queen(Player.Black, board, 7, 3);
            new King(Player.White, board, 0, 4);
            new King(Player.Black, board, 7, 4);
            new Bishop(Player.White, board, 0, 5);
            new Bishop(Player.Black, board, 7, 5);
            new Knight(Player.White, board, 0, 6);
            new Knight(Player.Black, board, 7, 6);
            new Rook(Player.White, board, 0, 7);
            new Rook(Player.Black, board, 7, 7);

            var ai = new AI();

            var g = new Game(board);
            var timer = new Stopwatch();
            timer.Start();

            var x = ai.SelectMove(Player.White, 8, board);
            timer.Stop();
         //   Assert.AreEqual(1000, timer.ElapsedMilliseconds);
            Assert.AreEqual(10000, ai.Count);
        }
    }
}
