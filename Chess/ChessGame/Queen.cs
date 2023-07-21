using Chess.Board;

namespace Chess.ChessGame;

public class Queen : Piece
{
    public Queen(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] possible = new bool[8, 8];
        Position p = new Position(0, 0);
        // NE
        p.DefValues(Pos.Line - 1, Pos.Row + 1);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Line--;
            p.Row++;
        }
        // NW
        p.DefValues(Pos.Line - 1, Pos.Row - 1);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Line--;
            p.Row--;
        }
        // SW
        p.DefValues(Pos.Line + 1, Pos.Row - 1);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Line++;
            p.Row--;
        }
        // SE
        p.DefValues(Pos.Line + 1, Pos.Row + 1);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Line++;
            p.Row++;
        }
        // Up
        p.DefValues(Pos.Line - 1, Pos.Row);
        while (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
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
            possible[p.Line, p.Row] = true;
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
            possible[p.Line, p.Row] = true;
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
            possible[p.Line, p.Row] = true;
            if (Board.GetPiece(p) != null && Board.GetPiece(p).Color != Color)
            {
                break;
            }
            p.Row++;
        }
        return possible;
    }

    public override string ToString()
    {
        return " Q ";
    }
}