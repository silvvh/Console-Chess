using Chess.Board;

namespace Chess.ChessGame;

public class Knight : Piece
{
    public Knight(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override string ToString()
    {
        return " K ";
    }
}