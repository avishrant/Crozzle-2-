using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Crozzle
{
    public partial class Form1 : Form
    {
        //User inputted words. 
        private WordDataList completeWordList = new WordDataList();

        //Highest scoring grid found.
        private CrozzleGrid highScoreGrid;

        //Grid use for generations.
        private CrozzleGrid crozzleGrid;

        public Form1()
        {
            InitializeComponent();

            //Disable the game generation until .CSV loaded.
            btnSolveGame.Enabled = false;
        }

        private void btnLoadWords_Click(object sender, EventArgs e)
        {
            //Open a .CSV file.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileExtension = Path.GetExtension(openFileDialog.FileName);
                if (String.Equals(fileExtension.ToUpper(), ".CSV") == true)
                {
                    validateCSVFile(openFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("Bad file format");
                }
            }
        }

        //Any validation code would go here, this example assumes all input is valid.
        private void validateCSVFile(String filename)
        {
            int difficulty = Constants.UNASSIGNED_DIFFICULTY;
            List<string> tempWordList = new List<string>();

            try
            {
                StreamReader sr = new StreamReader(filename);
                Char[] separator = { ',' };
                String[] line = sr.ReadLine().Split(separator);
                int field = 0;
                int numberOfWords = 0;
                int wordCount = 0;

                int rowSize = 0;
                int columnSize = 0;

                foreach (String value in line)
                {
                    if (field == 0)
                    {
                        int number = Convert.ToInt32(value);
                        numberOfWords = number;
                    }
                    else if (field == 1)
                    {
                        int number = Convert.ToInt32(value);
                        rowSize = number;
                    }
                    else if (field == 2)
                    {
                        int number = Convert.ToInt32(value);
                        columnSize = number;
                    }
                    else if (field == 3)
                    {
                        if (String.Equals(value.ToUpper(), "EASY") == true)
                        {
                            difficulty = Constants.EASY_DIFFICULTY;
                        }
                        else if (String.Equals(value.ToUpper(), "MEDIUM") == true)
                        {
                            difficulty = Constants.MEDIUM_DIFFICULTY;
                        }
                        else if (String.Equals(value.ToUpper(), "HARD") == true)
                        {
                            difficulty = Constants.HARD_DIFFICULTY;
                        }
                        else if (String.Equals(value.ToUpper(), "EXTREME") == true)
                        {
                            difficulty = Constants.EXTREME_DIFFICULTY;
                        }
                    }
                    else
                    {
                        wordCount++;
                        tempWordList.Add(value);
                    }
                    field++;
                }

                sr.Close();

                //Create a new grid.
                crozzleGrid = new CrozzleGrid(rowSize, columnSize, difficulty);
                buildWordData(tempWordList);

                //Enable generation button.
                btnSolveGame.Enabled = true;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error: File open, please close the file and try re-opening.");
            }
        }

        //Assign each word a score here based on the individual rules that apply.
        //Add WordData word to the word list.
        private void buildWordData(List<string> validWords)
        {
            if (crozzleGrid.Difficulty == Constants.EASY_DIFFICULTY)
            {
                foreach (string word in validWords)
                {
                    //Create WordData, text and the score.
                    completeWordList.addWord(new WordData(word, word.Length));
                }
            }

            //Other difficulty levels here.....
        }

        private void displayBoard()
        {
            char[,] arrGameBoard = highScoreGrid.getGridAsArray();

            txtGameDisplay.Text = "";

            for (int y = 0; y < highScoreGrid.RowSize; y++)
            {
                for (int x = 0; x < highScoreGrid.ColumnSize; x++)
                {
                    if (arrGameBoard[y, x] != Constants.NULL_CHAR)
                    {
                        txtGameDisplay.Text += arrGameBoard[y, x];
                    }
                    else
                    {
                        txtGameDisplay.Text += " ";
                    }
                }
                txtGameDisplay.Text += "\n";
            }
        }

        private void btnSolveGame_Click(object sender, EventArgs e)
        {
            //Get highest possible score.
            getHighestScoringGrid();

            //Display game board.
            displayBoard();

            //Display score.
            lblScoreDisplay.Text = highScoreGrid.TotalScore.ToString();
        }

        //Crozzle generation methods.
        private void getHighestScoringGrid()
        {
            //Create a WordData object we intend to place.
            WordData wordToPlace = null;

            //Checks both (HORIZONTAL_WORD = 0) and (VERTICAL_WORD = 1) word placement.
            for (int direction = 0; direction < 2; direction++)
            {
                //Start from the top left corner of the grix (x:0, y:0).
                for (int y = 0; y < crozzleGrid.RowSize; y++)
                {
                    for (int x = 0; x < crozzleGrid.ColumnSize; x++)
                    {
                        //Check if each word in the list fits the particular position.
                        foreach (WordData word in completeWordList)
                        {
                            //Create the first word placement.
                            wordToPlace = createFirstWordPlacement(word, x, y, direction);

                            //Add the first word to the grid.
                            crozzleGrid.addWordToGrid(wordToPlace);

                            //While a word can be found to connect to the previous word, add it to the grid.
                            while ((wordToPlace = createConnectingWordPlacement(wordToPlace)) != null)
                            {
                                crozzleGrid.addWordToGrid(wordToPlace);
                            }

                            //This solution can be further traversed and optimised here....


                            //Cache the highest scoring grid.
                            if (highScoreGrid == null || crozzleGrid.TotalScore > highScoreGrid.TotalScore)
                            {
                                highScoreGrid = crozzleGrid;
                            }

                            //Print each grid to the console (this significantly slows down the solution calculation).
                            //crozzleGrid.printGridToConsole(); 

                            //Create a new grid to use use for next grid generation
                            crozzleGrid = new CrozzleGrid(crozzleGrid.RowSize, crozzleGrid.ColumnSize, crozzleGrid.Difficulty);
                        }
                    }
                }
            }
        }

        private WordData createFirstWordPlacement(WordData word, int xStartPosition, int yStartPosition, int wordDirection)
        {
            //Create a deep copy of the word.
            WordData firstWordToPlace = word.getCopy();

            //Check if the word fits within the grid.
            //Assign the x-val and y-val and direction so the word can later be placed in the grid.
            if (wordDirection == Constants.HORIZONTAL_WORD)
            {
                if (crozzleGrid.checkHorizontalWordFitsGrid(word, xStartPosition, yStartPosition))
                {
                    firstWordToPlace.X = xStartPosition;
                    firstWordToPlace.Y = yStartPosition;
                    firstWordToPlace.Direction = Constants.HORIZONTAL_WORD;
                }
            }
            else if (wordDirection == Constants.VERTICAL_WORD)
            {
                if (crozzleGrid.checkVerticalWordFitsGrid(word, xStartPosition, yStartPosition))
                {
                    firstWordToPlace.X = xStartPosition;
                    firstWordToPlace.Y = yStartPosition;
                    firstWordToPlace.Direction = Constants.VERTICAL_WORD;
                }
            }
            return firstWordToPlace;
        }

        private WordData createConnectingWordPlacement(WordData previousWord)
        {
            WordData optimalWord = null;

            char[] previousWordCharArray = previousWord.Text.ToCharArray(0, previousWord.Length);

            //Iterate over all words in the word list.
            foreach (WordData word in completeWordList)
            {
                //Check if grid already contains the word.
                if (!crozzleGrid.WordsInGrid.listContains(word))
                {
                    //Iterate through letters of the previously placed word.
                    for (int previousWordIndex = 0; previousWordIndex < previousWordCharArray.Length; previousWordIndex++)
                    {
                        //Iterate through the letters of the current word.
                        for (int wordIndex = 0; wordIndex < word.Length; wordIndex++)
                        {
                            //If the two words share a letter.
                            if (word.Text[wordIndex] == previousWordCharArray[previousWordIndex])
                            {
                                int wordLeftHalfSize;

                                //Calculate the size of the left half of the word, to ensure when place within the grid
                                //it doesnt exceed x < 0.
                                if (wordIndex > 0)
                                    wordLeftHalfSize = wordIndex;
                                else
                                    wordLeftHalfSize = 0;

                                if (previousWord.Direction == Constants.HORIZONTAL_WORD)
                                {
                                    //Check the word fits within the grid.
                                    if (crozzleGrid.checkVerticalWordFitsGrid(word, previousWord.X + previousWordIndex, previousWord.Y - wordLeftHalfSize))
                                    {
                                        //If the word intersects any other words place on the grid ensure the intersecting letters match.
                                        if (crozzleGrid.checkVerticalWordPath(word, previousWord.X + previousWordIndex, previousWord.Y - wordLeftHalfSize))
                                        {
                                            //Check there is a valid amount of space around the word, (i.e. conforms to game spacing rules.
                                            if (crozzleGrid.checkTouchingVerticalWords(word, previousWord.X + previousWordIndex, previousWord.Y - wordLeftHalfSize))
                                            {
                                                if (crozzleGrid.checkGridIntersections(word, previousWord.X + previousWordIndex, previousWord.Y - wordLeftHalfSize, Constants.VERTICAL_WORD))
                                                {
                                                    //Check the word is at least 4 characters in length (this is to ensure maximum connectivity is possible for future words that are to short
                                                    //decrease the change for future connectivity). Smaller words can be added later in another optimisation type of method, smaller words should 
                                                    //however be added last (this is a heuristic rule I came up with).
                                                    //These statements (vertical aswell) here can and should be tweaked to get the best possible solution according to your set of rules.
                                                    if (optimalWord == null || word.Length >= 4)
                                                    {
                                                        optimalWord = word.getCopy();
                                                        optimalWord.X = previousWord.X + previousWordIndex;
                                                        optimalWord.Y = previousWord.Y - wordLeftHalfSize;
                                                        optimalWord.Direction = Constants.VERTICAL_WORD;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (previousWord.Direction == Constants.VERTICAL_WORD)
                                {
                                    if (crozzleGrid.checkHorizontalWordFitsGrid(word, previousWord.X - wordLeftHalfSize, previousWord.Y + previousWordIndex))
                                    {
                                        if (crozzleGrid.checkHorizontalWordPath(word, previousWord.X - wordLeftHalfSize, previousWord.Y + previousWordIndex))
                                        {
                                            if (crozzleGrid.checkTouchingHorizontalWords(word, previousWord.X - wordLeftHalfSize, previousWord.Y + previousWordIndex))
                                            {
                                                if (crozzleGrid.checkGridIntersections(word, previousWord.X - wordLeftHalfSize, previousWord.Y + previousWordIndex, Constants.HORIZONTAL_WORD))
                                                {
                                                    if (optimalWord == null || word.Length >= 4)
                                                    {
                                                        optimalWord = word.getCopy();
                                                        optimalWord.X = previousWord.X - wordLeftHalfSize;
                                                        optimalWord.Y = previousWord.Y + previousWordIndex;
                                                        optimalWord.Direction = Constants.HORIZONTAL_WORD;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return optimalWord;
        }
    }
}
