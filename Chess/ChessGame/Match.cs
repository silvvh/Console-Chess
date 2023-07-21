using System;
using System.Collections.Generic;
using System.Drawing;
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

        public bool Check { get; private set; }

        public Match()
        {
            Board = new ChessBoard();
            Turn = 1;
            ActualPlayer = Colors.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            Check = false;
            InsertPieces();
        }

        public Piece MoveTo(Position initial, Position destiny)
        {
            Piece p = Board.RemovePiece(initial);
            p.MovesIncrease();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(destiny, p);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public HashSet<Piece> Captured(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (color == piece.Color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        private Colors OppositeColor(Colors color)
        {
            if (color == Colors.White)
            {
                return Colors.Black;
            }
            return Colors.White;
        }

        private Piece IsKing(Colors color)
        {
            foreach (Piece p in InMatchPieces(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool IsInCheck(Colors color)
        {
            Piece king = IsKing(color);
            if (king == null)
            {
                throw new BoardException(".");
            }
            foreach (Piece p in InMatchPieces(OppositeColor(color)))
            {
                bool[,] m = p.PossibleMoves();
                if (m[king.Pos.Line, king.Pos.Row])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInCheckMate(Colors color)
        {
            if (!IsInCheck(color)) { return false; }

            foreach (Piece p in InMatchPieces(color))
            {
                bool[,] m = p.PossibleMoves();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Rows; j++)
                    {
                        if (m[i, j])
                        {
                            Position origin = p.Pos;
                            Position destiny = new Position(i, j);
                            Piece captured = MoveTo(origin, destiny);
                            bool checkTest = IsInCheck(color);
                            CancelMove(origin, destiny, captured);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Piece> InMatchPieces(Colors color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
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
            Piece captured = MoveTo(origin, destiny);
            if (IsInCheck(ActualPlayer))
            {
                CancelMove(origin, destiny, captured);
                throw new BoardException("Impossible move, you are putting yourself in check!");
            }

            if (IsInCheck(OppositeColor(ActualPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheckMate(OppositeColor(ActualPlayer)))
            {
                Finished = true;
            }

            else
            {
                Turn++;
                ChangePlayer();

            }

        }

        public void CancelMove(Position origin, Position destiny, Piece captured)
        {
            Piece p = Board.RemovePiece(destiny);

            p.MovesDecrease();
            if (captured != null)
            {
                Board.InsertPiece(destiny, captured);
                CapturedPieces.Remove(captured);
            }
            Board.InsertPiece(origin, p);
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
            InsertNewPiece('c', 1, new Rook(Colors.White, Board));
            InsertNewPiece('d', 1, new King(Colors.White, Board));
            InsertNewPiece('h', 7, new Rook(Colors.White, Board));
            InsertNewPiece('a', 8, new King(Colors.Black, Board));
            InsertNewPiece('b', 8, new Rook(Colors.Black, Board));

        }
    }
}
