using Chess.Board;

namespace Chess.ChessGame;

public class Rook : Piece
{
    public Rook(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return " R ";
    }
}