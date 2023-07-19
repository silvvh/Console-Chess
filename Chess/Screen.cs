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
                   PrintPiece(b.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h ");
        }

        public static void Create(ChessBoard b, bool[,] possiblePos)
        {
            ConsoleColor original = Console.ForegroundColor;
            for (int i = 0; i < 8; i++)
                {
                    Console.Write(8 - i);
                    for (int j = 0; j < 8; j++)
                    {
                        if (possiblePos[i, j])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = original;
                        }
                        PrintPiece(b.GetPiece(i, j));
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