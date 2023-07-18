using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Board;

namespace Chess.ChessGame
{
    internal class ChessPosition
    {
        public int Line { get; set; }
        public char Row { get; set; }

        public ChessPosition(int line, char row)
        {
            Line = line;
            Row = row;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Row - 'a');
        }

        public override string ToString()
        {
            return "" + Row + Line;
        }
    }
}
