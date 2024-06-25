using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameGrid
    {
        private readonly int[,] grid;

        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.grid = new int[rows, columns];
        }

        public void Reset()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    grid[i, j] = 0;
                }
            }
        }
        public bool IsInside(int r, int c)
        {
            return r < this.Rows && c < this.Columns && r >= 0 && c >= 0;
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        public bool IsRowFull(int r) // Clear row when row full
        {
            for (int c = 0; c < this.Columns; c++)
            {
                if (grid[r, c] == 0) return false;
            }
            return true;
        }
        public bool IsRowEmpty(int r) // Make up row down when row empty
        {
            for (int c = 0; c < this.Columns; c++)
            {
                if (grid[r, c] != 0) return false;
            }
            return true;
        }

        public int ClearFullRow()
        {
            int cleared = 0;

            for (int r = this.Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    cleared++;
                    ClearRow(r);
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            return cleared;
        }

        private void MoveRowDown(int r, int n)
        {
            if (r + n >= this.Rows) return;
            for (int c = 0; c < this.Columns; c++)
            {
                grid[r + n, c] = grid[r, c];
                grid[r, c] = 0;
            }

        }

        private void ClearRow(int r)
        {
            for (int c = 0; c < this.Columns; c++)
            {
                grid[r, c] = 0;
            }
        }
    }
}
