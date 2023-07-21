using Chess.Board;
using Chess.ChessGame;
using System.Text.RegularExpressions;
using Match = Chess.ChessGame.Match;

namespace Chess
{
    class Screen
    {
        public static void PrintMatch(Match match)
        {
            Screen.Create(match.Board);
            Console.WriteLine();
            PrintCaptured(match);
            Console.WriteLine();
            Console.WriteLine($"Turn: {match.Turn}");
            if (!match.Finished)
            {
                Console.WriteLine($"Waiting for move: {match.ActualPlayer}");
                Console.WriteLine();
                if (match.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
            }
        }

        public static void PrintCaptured(Match match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("Whites: ");
            PrintSet(match.Captured(Colors.White));
            Console.WriteLine();
            Console.Write("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.Captured(Colors.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("|");
            foreach (Piece s in set)
            {
                Console.Write(s + " ");
            }
            Console.Write("|");
        }
        public static void Create(ChessBoard b)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < 8; j++)
                {
                    PrintPiece(b.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h ");
        }

        public static void Create(ChessBoard b, bool[,] possiblePos)
        {
            ConsoleColor original = Console.BackgroundColor;
            ConsoleColor altered = ConsoleColor.DarkGray;
            for (int i = 0; i < 8; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < 8; j++)
                {
                    if (possiblePos[i, j])
                    {
                        Console.BackgroundColor = altered;
                    }
                    else
                    {
                        Console.BackgroundColor = original;
                    }
                    PrintPiece(b.GetPiece(i, j));
                    Console.BackgroundColor = original;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h ");
        }
        public static void PrintPiece(Piece p)
        {
            if (p == null)
            {
                Console.Write(" - ");
            }
            else
            {
                if (p.Color == Colors.White)
                {
                    Console.Write(p);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p);
                    Console.ForegroundColor = aux;
                }
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char row = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(row, line);
        }
    }
}