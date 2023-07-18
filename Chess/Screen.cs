using Chess.Board;
using Chess.ChessGame;

namespace Chess
{
    class Screen
    {
        public static void Create(ChessBoard b)
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write(8 - i);
                for (int j = 0; j < 8; j++)
                {
                    if (b.GetPiece(i, j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        PrintPiece(b.GetPiece(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h ");
        }
        public static void PrintPiece(Piece p)
        {
            if (p.Color == Colors.White)
            {
                Console.WriteLine(p);
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
}