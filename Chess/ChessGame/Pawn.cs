using Chess.Board;

namespace Chess.ChessGame;

public class Pawn : Piece
{
    public Pawn(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public bool HasEnemy(Position Pos)
    {
        Piece p = Board.GetPiece(Pos);
        return p != null && p.Color != Color;
    }

    public bool FreePosition(Position p)
    {
        return Board.GetPiece(p) == null;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] possible = new bool[8, 8];
        Position p = new Position(0, 0);
        if (Color == Colors.White)
        {
            p.DefValues(Pos.Line - 1, Pos.Row);
            if (Board.ValidPosition(p) && FreePosition(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line - 2, Pos.Row);
            if (Board.ValidPosition(p) && FreePosition(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line - 1, Pos.Row+1);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line - 1, Pos.Row-1);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
        }
        else
        {
            p.DefValues(Pos.Line + 1, Pos.Row);
            if (Board.ValidPosition(p) && FreePosition(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line + 2, Pos.Row);
            if (Board.ValidPosition(p) && FreePosition(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line + 1, Pos.Row);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line + 1, Pos.Row);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
        }
        return possible;
    }

    public override string ToString()
    {
        return " P ";
    }
}