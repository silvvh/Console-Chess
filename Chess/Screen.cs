using Chess.Board;

namespace Chess
{
    class Screen
    {
        public static void Create()
        {

            ChessBoard b = new ChessBoard();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (b.GetPiece(i,j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.Write(" P ");
                    }
                }
                Console.Write("\n");
            }
        }
    }
}
