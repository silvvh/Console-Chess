using Chess.Board;

namespace Chess.ChessGame;

public class Rook : Piece
{
    public Rook(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] possibleMoves = new bool[Board.Lines, Board.Rows];
        Position p = new Position(0, 0);
        // Up
        p.DefValues(Pos.Line -1, Pos.Row);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possibleMoves[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Line--;
        }
        // Down
        p.DefValues(Pos.Line + 1, Pos.Row);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possibleMoves[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Line++;
        }
        // Left
        p.DefValues(Pos.Line, Pos.Row - 1);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possibleMoves[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }

            p.Row--;
        }
        // Right
        p.DefValues(Pos.Line, Pos.Row + 1);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possibleMoves[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Row++;
        }
        return possibleMoves;
    }

    public override string ToString()
    {
        return " R ";
    }
}