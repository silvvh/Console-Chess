using Chess.Board;

namespace Chess.ChessGame;

public class Pawn : Piece
{
    private Match ChessMatch { get; set; }
    public Pawn(Colors color, ChessBoard board, Match match) : base(color, board)
    {
        ChessMatch = match;
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
            if (Board.ValidPosition(p) && FreePosition(p) && Moves == 0)
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line - 1, Pos.Row + 1);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line - 1, Pos.Row - 1);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
            // En Passant
            if (Pos.Line == 3)
            {
                Position left = new Position(Pos.Line, Pos.Row - 1);
                if (Board.ValidPosition(left) && HasEnemy(left) && Board.GetPiece(left) == ChessMatch.EnPassant)
                {
                    possible[left.Line - 1, left.Row] = true;
                }

                Position right = new Position(Pos.Line, Pos.Row + 1);
                if (Board.ValidPosition(right) && HasEnemy(right) && Board.GetPiece(right) == ChessMatch.EnPassant)
                {
                    possible[right.Line - 1, right.Row] = true;
                }
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
            if (Board.ValidPosition(p) && FreePosition(p) && Moves == 0)
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line + 1, Pos.Row - 1);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
            p.DefValues(Pos.Line + 1, Pos.Row + 1);
            if (Board.ValidPosition(p) && HasEnemy(p))
            {
                possible[p.Line, p.Row] = true;
            }
            // En Passant
            if (Pos.Line == 4)
            {
                Position left = new Position(Pos.Line, Pos.Row - 1);
                if (Board.ValidPosition(left) && HasEnemy(left) && Board.GetPiece(left) == ChessMatch.EnPassant)
                {
                    possible[left.Line+1, left.Row] = true;
                }

                Position right = new Position(Pos.Line, Pos.Row + 1);
                if (Board.ValidPosition(right) && HasEnemy(right) && Board.GetPiece(right) == ChessMatch.EnPassant)
                {
                    possible[right.Line+1, right.Row] = true;
                }
            }
        }
        return possible;
    }

    public override string ToString()
    {
        return " P ";
    }
}