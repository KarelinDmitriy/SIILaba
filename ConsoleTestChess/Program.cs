using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessModel;

namespace ConsoleTestChess
{
    class Program
    {
        static void PrintBoard(Figure[,] board)
        {
            Console.WriteLine("  A B C D E F J I");
            Console.WriteLine();
            for (int i=0; i<8; i++)
            {
                Console.Write((i + 1).ToString() + " ");
                for (int j=0; j<8; j++)
                {
                    if (board[i, j] == null) Console.Write(". ");
                    else Console.Write(board[i, j].ToString()+" ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
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
            new Queen(Player.White, 0, 3);
            new Queen(Player.Black, 7, 3);
            new King(Player.White, 0, 4);
            new King(Player.Black, 7, 4);
            new Bishop(Player.White, 0, 5);
            new Bishop(Player.Black, 7, 5);
            new Knight(Player.White, 0, 6);
            new Knight(Player.Black, 7, 6);
            new Rook(Player.White, 0, 7);
            new Rook(Player.Black, 7, 7);

            Game g = new Game();
            State s = State.Calm;

            while (s != State.Checkmate)
            {
                PrintBoard(Figure._board);
                Console.WriteLine();

                Console.Write("Введите ваш ход: ");
                string step = Console.ReadLine();
                Step st = Step.stringToStep(step);
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
