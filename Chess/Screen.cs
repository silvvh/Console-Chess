using Chess.Board;
using Chess.ChessGame;

namespace Chess
{
    class Screen
    {
        public static void Create()
        {
            try
            {
                ChessBoard b = new ChessBoard();
                b.InsertPiece(new Position(0, 0), new Bishop(Colors.Black, b));
                b.InsertPiece(new Position(0, 0), new Queen(Colors.Black, b));
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (b.GetPiece(i, j) == null)
                        {
                            Console.Write(" - ");
                        }
                        else
                        {
                            Console.Write(b.GetPiece(i, j));
                        }
                    }

                    Console.Write("\n");
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine("Position error: " + e.Message);
            }
        }
    }
}
