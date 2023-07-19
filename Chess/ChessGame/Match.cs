using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Board;

namespace Chess.ChessGame
{
    public class Match
    {
        public ChessBoard Board { get; }
        private int Turn { get; set; }
        private Colors ActualPlayer { get; set; }
        public bool Finished { get; set; }

        public Match()
        {
            Board = new ChessBoard();
            Turn = 1;
            ActualPlayer = Colors.White;
            Finished = false;
            InsertPieces();
        }

        public void MoveTo(Position initial, Position destiny)
        {
            Piece p = Board.RemovePiece(initial);
            p.MovesIncrease();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(destiny, p);
        }

        private void InsertPieces()
        {
            Board.InsertPiece(new ChessPosition('c', 1).ToPosition(), new Rook(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition('d', 1).ToPosition(), new King(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition('e', 1).ToPosition(), new Rook(Colors.Black, Board));
        }
    }
}
