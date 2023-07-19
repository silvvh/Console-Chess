using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using Chess;
using Chess.Board;
using Chess.ChessGame;

{
    Match match = new Match();
    while (!match.Finished)
    {
        try
        {
            Console.Clear();
            Screen.PrintMatch(match);
            Console.Write("Initial Position: ");
            Position origin = Screen.ReadChessPosition().ToPosition();
            match.ValidateOriginPosition(origin);
            bool[,] possiblePositions = match.Board.GetPiece(origin).PossibleMoves();
            Console.Clear();
            Screen.Create(match.Board, possiblePositions);
            Console.WriteLine("Desired Position: ");
            Position destiny = Screen.ReadChessPosition().ToPosition();
            match.ValidateDestinyPosition(origin, destiny);
            match.MakesMove(origin, destiny);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
}