using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTicTacToe
{
    internal class Program
    {
        // 0- пустое поле
        // 1 - крестик
        // 2 - нолик

        static int SIZE = 3;
        static int[,] gameField;
        static void printGameField(int[,] m)
        {
            for (int row = 0; row < m.GetLength(0); row++)
            {
                for (int col = 0; col < m.GetLength(1); col++)
                    Console.Write(m[row, col]);
                Console.WriteLine();
            }
        }

        static string Decode(int a)
        {
            switch (a)
            { 
                case 0: return " ";
                case 1: return "x";
                default: return "o";
            }
        }

        static void printFineGameField(int[,] m)
        {
            /*
               | x | 
            ---+---+---
               | 0 | x 
            ---+---+---
             x | x | x 

            */
            Console.Clear();
            for (int row = 0; row < m.GetLength(0); row++)
            {
                if (row != 0)Console.WriteLine("---+---+---");
                string line = " " + Decode(m[row, 0]) + " | " + Decode(m[row, 1]) + " | " + Decode(m[row, 2]) + " ";
                Console.WriteLine(line);
                
            }
        }

        static bool userStep(int user)
        {
            Console.WriteLine("Введите координаты " + Decode(user));
            string[] temp = Console.ReadLine().Split();
            int rowUser = Convert.ToInt32(temp[0]) - 1;
            int colUser = Convert.ToInt32(temp[1]) - 1;
            if (0 <= rowUser && rowUser <= SIZE && 0 <= colUser && colUser <= SIZE)
            {
                if (gameField[rowUser, colUser] == 0)
                {
                    gameField[rowUser, colUser] = user;
                    return true;
                }
            }
            return false;
        }

        /*
         -1 - игра продолжается
         1 - победа крестиков
         2 - победа ноликов
         0 - ничья
         */
        static int getWin(int[,] game, int user)
        {
            int k_zero = 0;
            // по горизонтали
            for (int row = 0; row < game.GetLength(0); row++)
            {
                int k = 0;
                for (int col = 0; col < game.GetLength(1); col++)
                {
                    if (game[row, col] == user) k++;
                    if (game[row, col] == 0) k_zero++;
                }
                if (k == SIZE) return user;
            }
            // по вертикали
            for (int col = 0; col < game.GetLength(1); col++)
            {
                int k = 0;
                for (int row = 0; row < game.GetLength(0); row++)
                    if (game[row, col] == user) k++;
                if (k == SIZE) return user;
            }
            // по диагонали
            int k_main = 0;
            int k_reverse = 0;
            for (int col = 0; col < game.GetLength(1); col++)
            {
                if (game[col, col] == user) k_main++;
                if (game[SIZE - col - 1, col] == user) k_reverse++;
            }
            if (k_main == SIZE) return user;
            if (k_reverse == SIZE) return user;
            // ничья
            if (k_zero == 0)
                return 0;

            return -1;
        }

        static void Main(string[] args)
        {
            gameField = new int[SIZE, SIZE];
            int user = 1;
            string text_Error = "";
            while (true)
            {
                printFineGameField(gameField);
                Console.WriteLine(text_Error);
                if (userStep(user))
                {
                    int win = getWin(gameField, user);
                    if (win == -1)
                    {
                        user = user == 1 ? user = 2 : user = 1;
                        //if (user == 1) user = 2; else user = 1;
                        text_Error = "";
                    }
                    else 
                    {
                        printFineGameField(gameField);
                        if (win == 0)
                            Console.WriteLine($"Ничья");
                        else
                            Console.WriteLine($"Выиграли " + Decode(user));
                        break;
                    }
                }
                else
                    text_Error = "По такой координате ходить нельзя!";
            }
        }
    }
}

