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
        public int Turn { get; private set; }
        public Colors ActualPlayer { get; private set; }
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

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.GetPiece(origin) == null)
            {
                throw new BoardException("Invalid position");
            }
            if (!Board.GetPiece(origin).HasPossibleMoves())
            {
                throw new BoardException("The chosen piece hasn't possible moves");
            }

            if (ActualPlayer != Board.GetPiece(origin).Color)
            {
                throw new BoardException("It's the other player's turn.");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).PossibleMoves()[destiny.Line, destiny.Row])
            {
                throw new BoardException("Impossible move");
            }
        }

        public void MakesMove(Position origin, Position destiny)
        {
            MoveTo(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            if (ActualPlayer == Colors.Black)
            {
                ActualPlayer = Colors.White;
            }
            else
            {
                ActualPlayer = Colors.Black;
            }
        }

        private void InsertPieces()
        {
            Board.InsertPiece(new ChessPosition('c', 1).ToPosition(), new Rook(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition('d', 1).ToPosition(), new King(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition('e', 1).ToPosition(), new Rook(Colors.Black, Board));
            Board.InsertPiece(new ChessPosition('e', 5).ToPosition(), new Rook(Colors.White, Board));
        }
    }
}
