using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Crozzle
{
    public class WordDataList : IEnumerable
    {
        private List<WordData> words;
        private int wordCount;

        //Constructor.
        public WordDataList()
        {
            words = new List<WordData>();
            wordCount = 0;
        }

        //Properties.
        public List<WordData> Words
        {
            get { return words; }
        }
        public int WordCount
        {
            get { return wordCount; }
        }

        //Methods.
        public void addWord(WordData word)
        {
            words.Add(word);
            wordCount++;
        }
        public void clearList()
        {
            words.Clear();
            wordCount = 0;
        }
        public bool listContains(WordData word)
        {
            bool wordUsed = false;
            foreach (WordData usedWord in words)
            {
                if (word.Text == usedWord.Text)
                    wordUsed = true;
            }
            return wordUsed;
        }
        public void printWordsToConsole()
        {
            foreach (WordData word in words)
            {
                string direction = "";
                if (word.Direction == Constants.HORIZONTAL_WORD)
                    direction = "Horizontal";
                else
                    direction = "Vertical";

                Console.WriteLine("Word:{0} at X:{1} Y:{2} is a {3} word and has {4} intersections.", word.Text, word.X, word.Y, direction, word.IntersectionCount);
            }
        }
        //Create a deep copy of the list.
        public WordDataList getCopy()
        {
            WordDataList deepCopiedList = new WordDataList();

            foreach (WordData word in words)
            {
                string text = word.Text;
                int length = word.Length;

                WordData copiedWord = new WordData(text, length);
                deepCopiedList.addWord(copiedWord);
            }

            return deepCopiedList;
        }
        public IEnumerator GetEnumerator()
        {
            return words.GetEnumerator();
        }

    }
}
