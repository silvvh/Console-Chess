
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
        public Piece EnPassant { get; private set; }
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
            EnPassant = null;
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
            // Short Castling
            if (p is King && destiny.Row == initial.Row + 2)
            {
                Position initialR = new Position(initial.Line, initial.Row + 3);
                Position destinyR = new Position(initial.Line, initial.Row + 1);
                Piece R = Board.RemovePiece(initialR);
                R.MovesIncrease();
                Board.InsertPiece(destinyR, R);
            }
            // Long Castling
            if (p is King && destiny.Row == initial.Row - 2)
            {
                Position initialR = new Position(initial.Line, initial.Row - 4);
                Position destinyR = new Position(initial.Line, initial.Row - 1);
                Piece R = Board.RemovePiece(initialR);
                R.MovesIncrease();
                Board.InsertPiece(destinyR, R);
            }

            // En Passant
            if (p is Pawn)
            {
                if (initial.Row != destiny.Row && capturedPiece == null)
                {
                    Position pawnPosition;
                    if (p.Color == Colors.White)
                    {
                        pawnPosition = new Position(destiny.Line + 1, destiny.Row);
                    }
                    else
                    {
                        pawnPosition = new Position(destiny.Line - 1, destiny.Row);
                    }
                    capturedPiece = Board.RemovePiece(pawnPosition);
                    CapturedPieces.Add(capturedPiece);
                }
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
            Piece p = Board.GetPiece(destiny);

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

            if (p is Pawn)
            {
                if (p.Color == Colors.White && destiny.Line == 0 || p.Color == Colors.Black && destiny.Line == 7)
                {
                    p = Board.RemovePiece(destiny);
                    Pieces.Remove(p);
                    Piece queen = new Queen(p.Color, Board);
                    Board.InsertPiece(destiny,  queen);
                    Pieces.Add(queen);
                }
            }

            if (p is Pawn && (destiny.Line == origin.Line + 2 || destiny.Line == origin.Line - 2))
            {
                EnPassant = p;
            }
            else
            {
                EnPassant = null;
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
            if (p is King && destiny.Row == origin.Row + 2)
            {
                {
                    Position initialR = new Position(origin.Line, origin.Row + 3);
                    Position destinyR = new Position(origin.Line, origin.Row + 1);
                    Piece R = Board.RemovePiece(destinyR);
                    R.MovesDecrease();
                    Board.InsertPiece(origin, R);
                }
            }

            if (p is King && destiny.Row == origin.Row - 2)
            {
                {
                    Position initialR = new Position(origin.Line, origin.Row - 4);
                    Position destinyR = new Position(origin.Line, origin.Row - 1);
                    Piece R = Board.RemovePiece(destinyR);
                    R.MovesDecrease();
                    Board.InsertPiece(origin, R);
                }
            }

            // En Passant

            if (p is Pawn)
            {
                if (origin.Row != destiny.Row && captured == EnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position posP;
                    if (p.Color == Colors.White)
                    {
                        posP = new Position(3, destiny.Row);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Row);
                    }

                    Board.InsertPiece(posP, pawn);
                }
            }
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

            InsertNewPiece('a', 1, new Rook(Colors.White, Board));
            InsertNewPiece('b', 1, new Knight(Colors.White, Board));
            InsertNewPiece('c', 1, new Bishop(Colors.White, Board));
            InsertNewPiece('d', 1, new Queen(Colors.White, Board));
            InsertNewPiece('e', 1, new King(Colors.White, Board, this));
            InsertNewPiece('f', 1, new Bishop(Colors.White, Board));
            InsertNewPiece('g', 1, new Knight(Colors.White, Board));
            InsertNewPiece('h', 1, new Rook(Colors.White, Board));
            InsertNewPiece('a', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('b', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('c', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('d', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('e', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('f', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('g', 2, new Pawn(Colors.White, Board, this));
            InsertNewPiece('h', 2, new Pawn(Colors.White, Board, this));

            InsertNewPiece('a', 8, new Rook(Colors.Black, Board));
            InsertNewPiece('b', 8, new Knight(Colors.Black, Board));
            InsertNewPiece('c', 8, new Bishop(Colors.Black, Board));
            InsertNewPiece('d', 8, new Queen(Colors.Black, Board));
            InsertNewPiece('e', 8, new King(Colors.Black, Board, this));
            InsertNewPiece('f', 8, new Bishop(Colors.Black, Board));
            InsertNewPiece('g', 8, new Knight(Colors.Black, Board));
            InsertNewPiece('h', 8, new Rook(Colors.Black, Board));
            InsertNewPiece('a', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('b', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('c', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('d', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('e', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('f', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('g', 7, new Pawn(Colors.Black, Board, this));
            InsertNewPiece('h', 7, new Pawn(Colors.Black, Board, this));

        }
    }
}
