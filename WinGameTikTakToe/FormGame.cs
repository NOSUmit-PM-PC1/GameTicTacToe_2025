using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinGameTikTakToe
{
    public partial class FormGame : Form
    {
        // 0- пустое поле
        // 1 - крестик
        // 2 - нолик

        static int SIZE = 3;
        static int[,] gameField;
        int user = 1;
        string textError = "";
        public FormGame()
        {
            InitializeComponent();
            gameField = new int[SIZE, SIZE];
        }
        string Decode(int a)
        {
            switch (a)
            {
                case 0: return " ";
                case 1: return "x";
                default: return "o";
            }
        }
        bool userStep(int user, int rowUser, int colUser)
        {
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
        void printGameField(int[,] m)
        {
            for (int row = 0; row < m.GetLength(0); row++)
            {
                for (int col = 0; col < m.GetLength(1); col++)
                    if (m[row, col] > 0)
                    {
                        dataGridViewField.Rows[row].Cells[col].Style.BackColor = Color.LightCoral;
                        dataGridViewField.Rows[row].Cells[col].Value = Decode(m[row, col]);
                    }
            }
        }
        private void FormGame_Load(object sender, EventArgs e)
        {
            int sizeCell = (this.ClientSize.Width - 20) / SIZE;
            this.ClientSize = new Size(sizeCell * SIZE + 20, sizeCell * SIZE + 60); 
            for (int i = 0; i < SIZE; i++)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.Width = sizeCell;
                dataGridViewField.Columns.Add(textColumn);
                dataGridViewField.Rows.Add(1);
                dataGridViewField.Rows[i].Height = sizeCell;
            }

            
        }
        int getWin(int[,] game, int user)
        {
            /*
        -1 - игра продолжается
        1 - победа крестиков
        2 - победа ноликов
        0 - ничья
        */
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
        private void dataGridViewField_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            if (userStep(user, e.RowIndex, e.ColumnIndex))
            {
                int win = getWin(gameField, user);
                if (win == -1)
                {
                    user = user == 1 ? user = 2 : user = 1;
                    textError = "";
                }
                else
                {
                    if (win == 0)
                        textError =$"Ничья";
                    else
                        textError = $"Выиграли " + Decode(user);
                }
            }
            else
                textError = "Так ходить нельзя!";

            toolStripStatusLabelUserStep.Text = $"Ход {Decode(user)}";
            printGameField(gameField);
            if (textError != "") MessageBox.Show(textError);
        }
    }
}
