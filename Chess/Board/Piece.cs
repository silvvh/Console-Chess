using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Board
{
    public abstract class Piece
    {
        public Position? Pos { get; set; }
        public Colors Color { get; protected set; }
        public int Moves { get; protected set; }
        public ChessBoard Board { get; set; }
        public Piece(Colors color, ChessBoard board)
        {
            Color = color;
            Board = board;
            Moves = 0;
        }

        public virtual bool canMoveTo(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

 

        public void MovesIncrease()
        {
            Moves++;
        }

        public void MovesDecrease()
        {
            Moves--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] possibleMoves = PossibleMoves();
            foreach (bool possible in possibleMoves)
            {
                if (possible)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract bool[,] PossibleMoves();
    }
}
