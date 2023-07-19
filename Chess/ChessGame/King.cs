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

        public override bool[,] PossibleMoves()
        {
            bool[,] possible = new bool[Board.Lines, Board.Rows];
            Position pos = new Position(0, 0);
            // Up
            pos.DefValues(Pos.Line, Pos.Row - 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Right 
            pos.DefValues(Pos.Line, Pos.Row + 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Down
            pos.DefValues(Pos.Line + 1, Pos.Row);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Left
            pos.DefValues(Pos.Line, Pos.Row - 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Northeast 
            pos.DefValues(Pos.Line - 1, Pos.Row + 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Northwest
            pos.DefValues(Pos.Line-1, Pos.Row - 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Southwest
            pos.DefValues(Pos.Line+1, Pos.Row+1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Southeast
            if (Board.ValidPosition(pos) && !canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            return possible;
        }

        public override string ToString()
        {
            return " K ";
        }
    }
}