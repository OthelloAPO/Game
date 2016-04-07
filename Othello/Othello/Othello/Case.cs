using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    class Case
    {
        private int x;
        private int y;

        public Case(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Boolean estEgale(int x, int y)
        {
            if (x == this.x && y == this.y)
                return true;
            else
                return false;
        }
    }
}
