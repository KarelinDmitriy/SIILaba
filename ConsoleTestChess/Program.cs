﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;
using System.Diagnostics;

namespace ConsoleTestChess
{
    class Program
    {
        static void PrintBoard(Figure[] board)
        {
            Console.WriteLine("  A B C D E F G H");
            Console.WriteLine();
            for (int i=0; i<8; i++)
            {
                Console.Write((i + 1).ToString() + " ");
                for (int j=0; j<8; j++)
                {
                    if (board[(i<<3) + j] == null) Console.Write(". ");
                    else Console.Write(board[(i<<3) + j].ToString()+" ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Figure._board = new Figure[64];
            Game._board = Figure._board;
            Move._board = Figure._board;
            AI._board = Figure._board;
            //Создаем пешек
            //for (int i = 0; i < 8; i++)
            //{
            //    new Pawn(Player.White, 1, i);
            //    new Pawn(Player.Black, 6, i);
            //}
            //new Rook(Player.White, 0, 0);
            //new Rook(Player.Black, 7, 0);
            //new Knight(Player.White, 0, 1);
            //new Knight(Player.Black, 7, 1);
            //new Bishop(Player.White, 0, 2);
            //new Bishop(Player.Black, 7, 2);
            //new Queen(Player.White, 0, 3);
            //new Queen(Player.Black, 7, 3);
            //new King(Player.White, 0, 4);
            //new King(Player.Black, 7, 4);
            //new Bishop(Player.White, 0, 5);
            //new Bishop(Player.Black, 7, 5);
            //new Knight(Player.White, 0, 6);
            //new Knight(Player.Black, 7, 6);
            //new Rook(Player.White, 0, 7);
            //new Rook(Player.Black, 7, 7);

            for (int i = 0; i < 8; i++)
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

            Game g = new Game();
            AI ai = new AI();
            State s = State.Calm;

            while (s != State.Checkmate)
            {
                PrintBoard(Figure._board);
                Console.WriteLine();

                Console.Write("Введите ваш ход({0}): ", g.Player);
                Step st;
                if (g.Player == Player.White)
                {
                    string step = Console.ReadLine();
                    st = Step.stringToStep(step);
                }
                else
                {
                    Stopwatch tim = new Stopwatch();
                    tim.Start();
                    st = ai.SelectMove(g.Player, 6);
                    tim.Stop();
                    Console.WriteLine(tim.ElapsedMilliseconds);
                    Console.WriteLine();
                }
                try
                {
                        g.doMove(st);
                    s = g.calcState();
                }
                catch (ErrorStepExveption)
                {
                    Console.WriteLine("Не коректный ход");
                }
            }
        }
    }
}
