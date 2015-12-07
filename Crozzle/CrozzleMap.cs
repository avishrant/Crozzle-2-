using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    public class CrozzleMap
    {
        private List<Coord[,]> gameGrid = new List<Coord[,]>();

        private int rowSize;
        private int columnSize;

        //Constructor.
        public CrozzleMap(int rowSize, int columnSize)
        {
            Coord[,] arrGameBoard = new Coord[rowSize, columnSize];

            this.rowSize = rowSize;
            this.columnSize = columnSize;


            for (int y = 0; y < rowSize; y++)
            {
                for (int x = 0; x < columnSize; x++)
                {
                    arrGameBoard[y, x] = new Coord(false, false);
                }
            }

            gameGrid.Add(arrGameBoard);
        }

        //Properties.
        public Coord[,] GameGrid
        {
            get { return gameGrid.First(); }
        }
        public int ColumnSize
        {
            get { return columnSize; }
        }
        public int RowSize
        {
            get { return rowSize; }
        }

        //Methods.
        public void addWord(WordData word, int x, int y, int direction)
        {
            Coord[,] grid = GameGrid;

            for (int i = 0; i < word.Length; i++)
            {
                if (direction == Constants.HORIZONTAL_WORD)
                {
                    grid[y, x + i].X = true;
                }
                else
                {
                    grid[y + i, x].Y = true;
                }
            }
        }
        public void removeWord(WordData word, int x, int y, int direction)
        {
            Coord[,] grid = GameGrid;

            for (int i = 0; i < word.Length; i++)
            {
                if (direction == Constants.HORIZONTAL_WORD)
                {
                    grid[y, x + i].X = false;
                }
                else
                {
                    grid[y + i, x].Y = false;
                }
            }
        }
        public void printGridToConsole()
        {
            Coord[,] grid = GameGrid;

            Console.WriteLine();
            Console.WriteLine("Row={0}", rowSize);
            Console.WriteLine("Column={0}", columnSize);
            Console.WriteLine();

            for (int y = 0; y < rowSize; y++)
            {
                for (int x = 0; x < columnSize; x++)
                {

                    if (grid[y, x].X == true && grid[y, x].Y == true)
                    {
                        Console.Write("B");
                    }
                    else if (grid[y, x].X == true && grid[y, x].Y == false)
                    {
                        Console.Write("X");
                    }
                    else if (grid[y, x].X == false && grid[y, x].Y == true)
                    {
                        Console.Write("Y");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
