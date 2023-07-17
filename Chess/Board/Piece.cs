using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    internal class Piece
    {
        public Position Pos { get; set; }
        public Colors Color { get; protected set; }
        public int Moves { get; protected set; }
        public Board NewBoard { get; set; }
        public Piece(Position position, Colors color, Board board)
        {
            Pos = position;
            Color = color;
            NewBoard = board;
            Moves = 0;
        }
    }
}
