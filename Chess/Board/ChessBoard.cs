

namespace Chess.Board
{
    public class ChessBoard
    {
        private int Lines { get ; set; }
        private int Rows { get; set; }
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

        public void InsertPiece(Position position, Piece piece)
        {
            Pieces[position.Line, position.Row] = piece;
            piece.Pos = position;
        }
    }
}
