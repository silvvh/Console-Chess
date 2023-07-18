using Chess;
using Chess.Board;
using Chess.ChessGame;

ChessBoard b = new ChessBoard();
b.InsertPiece(new Position(0, 2), new Bishop(Colors.Black, b));
b.InsertPiece(new Position(0, 0), new Queen(Colors.Black, b));
Screen.Create(b);
ChessPosition pos = new ChessPosition(3, 'A');
Console.WriteLine(pos);