using System;
using System.Collections.Generic;
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

        static void printFineGameField(int[,] m)
        {
            /*
               | x | 
            ---+---+---
               | 0 | x
            ---+---+---
             x | x | x

            */
            for (int row = 0; row < m.GetLength(0); row++)
            {
                for (int col = 0; col < m.GetLength(1); col++)
                    Console.Write(m[row, col]);
                Console.WriteLine();
            }
        }

        static bool userStep(int user)
        {
            string[] temp = Console.ReadLine().Split();
            int rowUser = Convert.ToInt32(temp[0]) - 1;
            int colUser = Convert.ToInt32(temp[1]) - 1;
            if (0 <= rowUser && rowUser <= SIZE && 0 <= colUser && colUser <= SIZE)
            {
                gameField[rowUser, colUser] = user;
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            gameField = new int[SIZE, SIZE];
            int user = 1;
            while (true)
            {
                printGameField(gameField);
                if (userStep(user))
                    user = user == 1 ? user = 2: user = 1;
                    //if (user == 1) user = 2; else user = 1;
            }
            
        }
    }
}

