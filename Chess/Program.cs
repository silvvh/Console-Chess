using Chess;
using Chess.Board;
using Chess.ChessGame;

try
{
    Match match = new Match();
    Screen.Create(match.Board);
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();