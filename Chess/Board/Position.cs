using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    internal class Position
    {
        public int Line { get; set; }
        public int Row { get; set; } 

        public Position(int line, int row)
        {
            Line = line;
            Row = row;
        }
        public override string ToString()
        {
            return $"{Line}, {Row}";
        }
    }
}
