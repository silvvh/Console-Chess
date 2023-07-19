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
        private HashSet<Piece> Pieces { get; }
        private HashSet<Piece> CapturedPieces { get; }

        public Match()
        {
            Board = new ChessBoard();
            Turn = 1;
            ActualPlayer = Colors.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            InsertPieces();
        }

        public void MoveTo(Position initial, Position destiny)
        {
            Piece p = Board.RemovePiece(initial);
            p.MovesIncrease();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(destiny, p);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
        }

        public HashSet<Piece> Captured(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (color != piece.Color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        public HashSet<Piece> InMatchPieces(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                aux.Add(piece);
            }
            aux.ExceptWith(Captured(color));
            return aux;
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

        public void InsertNewPiece(char row, int line, Piece piece)
        {
            Board.InsertPiece(new ChessPosition(row, line).ToPosition(), piece);
            Pieces.Add(piece);
        }
        private void InsertPieces()
        {
            InsertNewPiece('c', 1, new Rook(Colors.Black, Board));
            InsertNewPiece('d', 1, new King(Colors.Black, Board));
            InsertNewPiece('e', 1, new Rook(Colors.Black, Board));
            InsertNewPiece('e', 8, new Rook(Colors.White, Board));
            InsertNewPiece('d', 8, new King(Colors.White, Board));
        }
    }
}
