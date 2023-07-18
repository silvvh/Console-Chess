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

        public Match()
        {
            Board = new ChessBoard();
            Turn = 1;
            ActualPlayer = Colors.White;
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
            Board.InsertPiece(new ChessPosition(1, 'c').ToPosition(), new Rook(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition(1, 'd').ToPosition(), new King(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition(1, 'e').ToPosition(), new Rook(Colors.Black, Board));
        }
    }
}
