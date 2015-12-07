using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle
{
    public class Coord
    {
        bool x;
        bool y;

        //Constructor.
        public Coord(bool x, bool y)
        {
            this.x = x;
            this.y = y;
        }

        //Properties.
        public bool X
        {
            get { return x; }
            set { x = value; }
        }
        public bool Y
        {
            get { return y; }
            set { y = value; }
        }

    }
}
