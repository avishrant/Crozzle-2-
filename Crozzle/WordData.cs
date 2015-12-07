using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    public class WordData
    {
        private string text;
        private int length;
        private int score;
        private int intersectionCount;
        private int x;
        private int y;
        private int direction;

        //Constructor.
        public WordData(string text, int score)
        {
            this.text = text;
            this.score = score;
            this.length = text.Length;

            x = 0;
            y = 0;

            intersectionCount = 0;
        }

        //Properties.
        public string Text
        {
            get { return text; }
            set { text = value; }//DELETE
        }
        public int Score
        {
            get { return score; }
        }
        public int Length
        {
            get { return length; }
        }
        public int IntersectionCount
        {
            get { return intersectionCount; }
            set { intersectionCount = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        //Methods.
        public WordData getCopy()
        {
            WordData word = new WordData(this.text, this.score);
            word.X = this.x;
            word.Y = this.y;
            word.Direction = this.direction;
            word.IntersectionCount = this.intersectionCount;

            return word;
        }
    }
}
