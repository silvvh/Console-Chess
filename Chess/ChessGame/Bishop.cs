using Chess.Board;

namespace Chess.ChessGame;

public class Bishop : Piece
{
    public Bishop(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return " B ";
    }
}