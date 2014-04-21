using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessModel;

namespace ChessTest
{
    [TestClass]
    public class TestTime
    {
        [TestMethod]
        [Timeout(50)]
        public void FirstStep()
        {
            Figure._board = new Figure[8, 8];
            Game._board = Figure._board;
            //Создаем пешек
            for (int i=0; i<8; i++)
            {
                new Pawn(Player.White, 1, i);
                new Pawn(Player.Black, 6, i);
            }
            new Rook(Player.White, 0, 0);
            new Rook(Player.Black, 7, 0);
            new Knight(Player.White, 0, 1);
            new Knight(Player.Black, 7, 1);
            new Bishop(Player.White, 5, 2);
            new Bishop(Player.Black, 7, 2);
            new Queen(Player.White, 3, 3);
            new Queen(Player.Black, 4, 3);
            new King(Player.White, 0, 4);
            new King(Player.Black, 7, 4);
            new Bishop(Player.White, 0, 5);
            new Bishop(Player.Black, 7, 5);
            new Knight(Player.White, 0, 6);
            new Knight(Player.Black, 7, 6);
            new Rook(Player.White, 2, 7);
            new Rook(Player.Black, 5, 7);

            var g = new Game();

            var steps = g.getAllLegalMoves(Player.White);

        }

        [TestMethod]
        [Timeout(50)]
        public void FirstStepState()
        {
            Figure._board = new Figure[8, 8];
            Game._board = Figure._board;
            //Создаем пешек
            for (int i = 0; i < 8; i++)
            {
                new Pawn(Player.White, 1, i);
                new Pawn(Player.Black, 6, i);
            }
            new Rook(Player.White, 0, 0);
            new Rook(Player.Black, 7, 0);
            new Knight(Player.White, 0, 1);
            new Knight(Player.Black, 7, 1);
            new Bishop(Player.White, 0, 2);
            new Bishop(Player.Black, 7, 2);
            new Queen(Player.White, 3, 3);
            new Queen(Player.Black, 4, 3);
            new King(Player.White, 0, 4);
            new King(Player.Black, 7, 4);
            new Bishop(Player.White, 0, 5);
            new Bishop(Player.Black, 7, 5);
            new Knight(Player.White, 0, 6);
            new Knight(Player.Black, 7, 6);
            new Rook(Player.White, 2, 7);
            new Rook(Player.Black, 5, 7);

            var g = new Game();
            var s = g.calcState();

            Assert.AreEqual(s, State.Calm);

        }
    }
}
