using Chess.Board;

namespace Chess.ChessGame
{

    public class King : Piece

    {
        public King(Colors color, ChessBoard chessBoard) : base(color, chessBoard)
        {
            Color = color;
            Board = chessBoard;
        }

        public override string ToString()
        {
            return " K ";
        }
    }
}