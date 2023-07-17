using Chess.Board;

namespace Chess.ChessGame;

public class Pawn : Piece
{
    public Pawn(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return " P ";
    }
}