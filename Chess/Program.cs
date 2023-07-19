using System.Reflection.PortableExecutable;
using Chess;
using Chess.Board;
using Chess.ChessGame;

try
{
    Match match = new Match();
    while (!match.Finished)
    {
        Console.Clear();
        Screen.Create(match.Board);
        Console.Write("Initial Position: ");
        Position origin = Screen.ReadChessPosition().ToPosition();
        bool[,] possiblePositions = match.Board.GetPiece(origin).PossibleMoves();
        Console.Clear();
        Screen.Create(match.Board, possiblePositions);  
        Console.WriteLine("Desired Position: ");
        Position destiny = Screen.ReadChessPosition().ToPosition();
        match.MoveTo(origin, destiny);

    }
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();