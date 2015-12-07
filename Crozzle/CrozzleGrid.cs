using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    public class CrozzleGrid
    {
        private List<char[,]> crozzleGrid = new List<char[,]>();
        private CrozzleMap crozzleMap;
        private int rowSize;
        private int columnSize;
        private int totalScore;

        private WordDataList wordsInGrid;
        private int wordsInGridCount;
        private int difficulty;

        //Constructor.
        public CrozzleGrid(int rowSize, int columnSize, int difficulty)
        {
            char[,] arrGameBoard = new char[rowSize, columnSize];

            crozzleMap = new CrozzleMap(rowSize, columnSize);

            this.rowSize = rowSize;
            this.columnSize = columnSize;
            totalScore = 0;
            wordsInGridCount = 0;
            this.difficulty = difficulty;

            //List of words in the grid.
            wordsInGrid = new WordDataList();

            //Create new board.
            crozzleGrid.Add(arrGameBoard);
        }

        //Properties.
        public int RowSize
        {
            get { return rowSize; }
        }
        public int ColumnSize
        {
            get { return columnSize; }
        }
        public int WordsInGridCount
        {
            get { return wordsInGridCount; }
        }
        public int Difficulty
        {
            get { return difficulty; }
        }
        public int TotalScore
        {
            get { return totalScore; }
        }
        public WordDataList WordsInGrid
        {
            get { return wordsInGrid; }
        }
        public char[,] getGridAsArray()
        {
            return crozzleGrid.First();
        }


        public void addWordToGrid(WordData word)
        {
            char[,] grid = getGridAsArray();

            //Add new word, character-by-character.
            for (int i = 0; i < word.Length; i++)
            {
                if (word.Direction == Constants.HORIZONTAL_WORD)
                {
                    grid[word.Y, word.X + i] = word.Text[i];

                }
                else if (word.Direction == Constants.VERTICAL_WORD)
                {
                    grid[word.Y + i, word.X] = word.Text[i];
                }
            }

            //Add the word to the CrozzleMap (tracking) grid.
            crozzleMap.addWord(word, word.X, word.Y, word.Direction);

            //Clear the old grid.
            crozzleGrid.Clear();
            //Add the temp grid with the new word, as the new grid.
            crozzleGrid.Add(grid);


            //Increment the word count.
            wordsInGridCount++;
            wordsInGrid.addWord(word);

            //Calculate the new grid score.
            calculateGridScore();
        }

        //Console log (debug method).
        public void printGridToConsole()
        {
            char[,] grid = getGridAsArray();

            Console.WriteLine();
            Console.WriteLine("->>GRID<<-");
            Console.WriteLine();
            Console.WriteLine("Rows = {0}", RowSize);
            Console.WriteLine("Columns = {0}", ColumnSize);
            Console.WriteLine();

            for (int y = 0; y < RowSize; y++)
            {
                for (int x = 0; x < ColumnSize; x++)
                {
                    if (grid[y, x] != Constants.NULL_CHAR)
                    {
                        Console.Write(grid[y, x]);
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Total Score = {0}", totalScore);
            Console.WriteLine();
            Console.WriteLine("+++++++++++++++++++++++");
        }

        //Iterate over the entire grid, each character is equal to 1 point.
        private void calculateGridScore()
        {
            int points = 0;
            char[,] grid = getGridAsArray();

            for (int y = 0; y < rowSize; y++)
            {
                for (int x = 0; x < columnSize; x++)
                {
                    if (grid[y, x] != Constants.NULL_CHAR)
                    {
                        points += 1;
                    }
                }
            }

            totalScore = points;
        }


        //Check the word is within the bounds of the grid.
        public bool checkHorizontalWordFitsGrid(WordData word, int startPointX, int startPointY)
        {
            if (startPointX < 0)
                return false;


            if (startPointX + word.Length <= columnSize)
                return true;
            else
                return false;
        }
        public bool checkVerticalWordFitsGrid(WordData word, int startPointX, int startPointY)
        {
            if (startPointY < 0)
                return false;

            if (startPointY + word.Length <= rowSize)
                return true;
            else
                return false;
        }


        //Check there is sufficient surrounding space for the word.
        public bool checkTouchingHorizontalWords(WordData word, int startPointX, int startPointY)
        {
            bool isValid = true;

            foreach (WordData wordInGrid in wordsInGrid)
            {
                if (wordInGrid.Direction == Constants.HORIZONTAL_WORD)
                {
                    //If a word exists that is on either side of the word being checked.
                    if (wordInGrid.Y >= startPointY - 1 && wordInGrid.Y <= startPointY + 1)
                    {
                        //If the word being checked is adjacent to any of the words on the grid.
                        if (wordInGrid.X < startPointX - 1 && wordInGrid.X + wordInGrid.Length >= startPointX)
                        {
                            isValid = false;
                        }
                        else if (wordInGrid.X >= startPointX - 1 && wordInGrid.X <= startPointX + word.Length)
                        {
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }
        public bool checkTouchingVerticalWords(WordData word, int connectPointX, int connectPointY)
        {
            bool isValid = true;

            foreach (WordData wordInGrid in wordsInGrid)
            {
                if (wordInGrid.Direction == Constants.VERTICAL_WORD)
                {
                    if (wordInGrid.X >= connectPointX - 1 && wordInGrid.X <= connectPointX + 1)
                    {
                        if (wordInGrid.Y < connectPointY - 1 && wordInGrid.Y + wordInGrid.Length >= connectPointY)
                        {
                            isValid = false;
                        }
                        else if (wordInGrid.Y >= connectPointY - 1 && wordInGrid.Y <= connectPointY + word.Length)
                        {
                            isValid = false;
                        }
                    }
                }
            }
            return isValid;
        }


        //Check if a word is intersecting another word on the grid it is a valid intersection.
        public bool checkHorizontalWordPath(WordData word, int startPointX, int startPointY)
        {
            bool isValid = true;
            char[,] grid = getGridAsArray();

            for (int i = 0; i < word.Length; i++)
            {
                //If the word interects another character of an existing word.
                if (grid[startPointY, startPointX + i] != Constants.NULL_CHAR)
                {
                    //Does the intersecting character match the existing words character.
                    if (word.Text[i] != grid[startPointY, startPointX + i])
                    {
                        isValid = false;
                    }
                }


                if (startPointY - 1 >= 0)
                {
                    if (grid[startPointY - 1, startPointX + i] != Constants.NULL_CHAR && word.Text[i] != grid[startPointY, startPointX + i])
                    {
                        isValid = false;
                    }
                }

                if (startPointY + 1 <= rowSize - 1)
                {
                    if (grid[startPointY + 1, startPointX + i] != Constants.NULL_CHAR && word.Text[i] != grid[startPointY, startPointX + i])
                    {
                        isValid = false;
                    }
                }
            }


            //Check point before the word starts.
            if (startPointX - 1 >= 0)
            {
                if (grid[startPointY, startPointX - 1] != Constants.NULL_CHAR)
                {
                    isValid = false;
                }
            }


            //Check for empty cell after the word ends.
            if (startPointX + word.Length + 1 <= ColumnSize)
            {
                if (grid[startPointY, startPointX + word.Length] != Constants.NULL_CHAR)
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        public bool checkVerticalWordPath(WordData word, int startPointX, int startPointY)
        {
            bool isValid = true;
            char[,] grid = getGridAsArray();

            for (int i = 0; i < word.Length; i++)
            {
                if (grid[startPointY + i, startPointX] != Constants.NULL_CHAR)
                {
                    if (word.Text[i] != grid[startPointY + i, startPointX])
                    {
                        isValid = false;
                    }
                }


                if (startPointX - 1 >= 0)
                {
                    if (grid[startPointY + i, startPointX - 1] != Constants.NULL_CHAR && word.Text[i] != grid[startPointY + i, startPointX])
                    {
                        isValid = false;
                    }
                }

                if (startPointX + 1 <= columnSize - 1)
                {
                    if (grid[startPointY + i, startPointX + 1] != Constants.NULL_CHAR && word.Text[i] != grid[startPointY + i, startPointX])
                    {
                        isValid = false;
                    }
                }
            }

            //Check the point before the word starts.
            if (startPointY - 1 >= 0)
            {
                if (grid[startPointY - 1, startPointX] != Constants.NULL_CHAR)
                {
                    isValid = false;
                }
            }


            //Check the character after the word ends.
            if (startPointY + word.Length + 1 <= rowSize)
            {
                if (grid[startPointY + word.Length, startPointX] != Constants.NULL_CHAR)
                {
                    isValid = false;
                }
            }




            return isValid;
        }

        //Check each word in the grid has a valid number of intersections.
        //This method can be easily adapted to suit your needs.
        public bool checkGridIntersections(WordData word, int startPointX, int startPointY, int direction)
        {
            //Add the word to the crozzle map.
            crozzleMap.addWord(word, startPointX, startPointY, direction);

            Coord[,] grid = crozzleMap.GameGrid;
            int maxIntersetions = 2;
            bool isValid = true;

            foreach (WordData placedWord in wordsInGrid)
            {
                int intersectionCount = 0;
                for (int i = 0; i < placedWord.Length; i++)
                {
                    if (placedWord.Direction == Constants.HORIZONTAL_WORD)
                    {
                        //Check if X and Y is true for the coordinate, if both true an intersection exists.
                        if (grid[placedWord.Y, placedWord.X + i].X && grid[placedWord.Y, placedWord.X + i].Y)
                        {
                            //Increment the intersection count.
                            intersectionCount++;
                        }
                    }
                    else if (placedWord.Direction == Constants.VERTICAL_WORD)
                    {
                        if (grid[placedWord.Y + i, placedWord.X].X && grid[placedWord.Y + i, placedWord.X].Y)
                        {
                            intersectionCount++;
                        }
                    }
                }

                //Check if there is a valid number of intersections.
                if (intersectionCount > maxIntersetions)
                    isValid = false;
            }

            //Remove the word from the tracking grid, it will be officially added when the word is added to the grid later.
            crozzleMap.removeWord(word, startPointX, startPointY, direction);

            return isValid;
        }
    }
}
