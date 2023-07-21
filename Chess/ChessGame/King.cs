using Chess.Board;

namespace Chess.ChessGame
{

    public class King : Piece

    {
        private Match ChessMatch { get; set; }
        public King(Colors color, ChessBoard chessBoard, Match match) : base(color, chessBoard)
        {

            ChessMatch = match;
        }

        private bool PossibleCastling(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null & p is Rook && p.Color == Color && p.Moves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possible = new bool[Board.Lines, Board.Rows];
            Position pos = new Position(0, 0);
            // Up
            pos.DefValues(Pos.Line - 1, Pos.Row);
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
            pos.DefValues(Pos.Line - 1, Pos.Row - 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Southwest
            pos.DefValues(Pos.Line + 1, Pos.Row - 1);
            if (Board.ValidPosition(pos) && canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }
            // Southeast
            pos.DefValues(Pos.Line + 1, Pos.Row + 1);
            if (Board.ValidPosition(pos) && !canMoveTo(pos))
            {
                possible[pos.Line, pos.Row] = true;
            }

            if (Moves == 0 && !ChessMatch.Check)
            {
                // Short Castling
                Position posR1 = new Position(Pos.Line, Pos.Row + 3);
                if (PossibleCastling(posR1))
                {
                    Position posK1 = new Position(Pos.Line, Pos.Row + 1);
                    Position posK2 = new Position(Pos.Line, Pos.Row + 2);
                    if (Board.GetPiece(posK1) == null && Board.GetPiece(posK2) == null)
                    {
                        possible[Pos.Line, Pos.Row + 2] = true;
                    }
                }
                // Long Castling
                Position posR2 = new Position(Pos.Line, Pos.Row - 4);
                if (PossibleCastling(posR2))
                {
                    Position p1 = new Position(Pos.Line, Pos.Row - 1);
                    Position p2 = new Position(Pos.Line, Pos.Row - 2);
                    Position p3 = new Position(Pos.Line, Pos.Row - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                    {
                        possible[Pos.Line, Pos.Row - 2] = true;
                    }
                }
            }
            return possible;
        }

        public override string ToString()
        {
            return " K ";
        }
    }
}