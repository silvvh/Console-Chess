using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Rows { get; set; }
        private Piece[,] Pieces { get; set; }
        public Board(int lines, int rows) { 
            Lines = lines;
            Rows = rows;
            Pieces = new Piece[lines, rows];
        }
    }
}
