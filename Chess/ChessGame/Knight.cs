using Chess.Board;

namespace Chess.ChessGame;

public class Knight : Piece
{
    public Knight(Colors color, ChessBoard board) : base(color, board)
    {
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] possible = new bool[8, 8];
        Position p = new Position(0, 0);
        // UP
        p.DefValues(Pos.Line-2, Pos.Row+1);
        if (Board.ValidPosition(p) && canMoveTo(p)) 
        {
            possible[p.Line, p.Row] = true;
        }
        // DOWN
        p.DefValues(Pos.Line+2, Pos.Row+1);
        if (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
        }
        // LEFT|UP
        p.DefValues(Pos.Line-2, Pos.Row-1);
        if (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
        }
        // LEFT|DOWN
        p.DefValues(Pos.Line+2, Pos.Row-1);
        if (Board.ValidPosition(p) && canMoveTo(p))
        {
            possible[p.Line, p.Row] = true;
        }
        return possible;
    }

    public override string ToString()
    {
        return " H ";
    }
}