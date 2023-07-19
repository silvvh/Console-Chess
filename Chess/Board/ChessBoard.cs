

namespace Chess.Board
{
    public class ChessBoard
    {
        public int Lines { get ; set; }
        public int Rows { get; set; }
        private Piece[,] Pieces { get; set; }

        public ChessBoard()
        {
            Lines = 8;
            Rows = 8;
            Pieces = new Piece[8,8];
        }

        public Piece GetPiece(int lines, int rows)
        {
            return Pieces[lines, rows];
        }

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Line, pos.Row];
        }

        public void InsertPiece(Position position, Piece piece)
        {
            if (!FreePosition(position))
            {
                throw new BoardException("This position is being used.");
            }
            Pieces[position.Line, position.Row] = piece;
            piece.Pos = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (GetPiece(position) == null)
            {
                return null;
            }

            Piece aux = GetPiece(position);
            aux.Pos = null;
            Pieces[position.Line, position.Row] = null;
            return aux;
        }

        public bool FreePosition(Position pos)
        {
            ValidPosition(pos);
            return GetPiece(pos) == null;
        }
        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Row < 0 || pos.Line >= Lines || pos.Row >= Rows)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid position.");
            }
        }
    }
}
