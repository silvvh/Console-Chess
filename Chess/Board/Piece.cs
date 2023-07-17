using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Board
{
    public class Piece
    {
        public Position Pos { get; set; }
        public Colors Color { get; protected set; }
        public int Moves { get; protected set; }
        public ChessBoard Board { get; set; }
        public Piece(Colors color, ChessBoard board)
        {
            Color = color;
            Board = board;
            Moves = 0;
        }
    }
}
