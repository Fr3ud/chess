using System;
using System.Text;
using ChessRules;

namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            // Chess chess1 = new Chess("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8");
            // Console.WriteLine(NextMoves(4, chess1));
            // return;

            /*
            Client client = new Client();
            string fen = client.GetFenFromServer();
            Console.WriteLine(client.GameID);
            Chess chess = new Chess(fen);
            */

            //Chess chess = new Chess("r1bqkb1r/pPPp2Pp/8/8/8/8/pPPPpPpP/1NBQKBNR b KQkq - 0 1");

            //  Chess chess = new Chess("4k3/3p1p2/8/4P1P1/2p1p3/8/1P1P1P2/4K3 w KQkq - 0 1");
            //  Chess chess = new Chess("r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq - 0 1");
            //  Chess chess = new Chess("r3k2r/8/8/q7/8/8/1PP5/R3K2R w KQkq - 0 1");
            //  Chess chess = new Chess("8/8/5k2/8/P7/4P3/8/R1BQK3 w - - 0 1");
              Chess chess = new Chess("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NpPP/RNBQK2R w KQ - 1 8");
            //  Chess chess = new Chess("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            //  Chess chess = new Chess("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 0");
            //  chess.GetFigureAt(3, 5);

            //  Console.WriteLine(NextMoves(4, chess));

            while (true)
            {
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                foreach (string moves in chess.YieldValidMoves())
                {
                    Console.WriteLine(moves);
                }
                string move = Console.ReadLine();

                if (move == "") break;

                /*
                if (move == "s")
                {
                    fen = client.GetFenFromServer();
                    chess = new Chess(fen);
                    continue;
                }
                */

                if (!chess.IsValidMove(move)) continue;

                chess = chess.Move(move);
                //fen = client.SendMove(move);
                //chess = new Chess(fen);
            }

            /* 
            while (true)
            {
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                foreach (string moves in chess.YieldValidMoves())
                {
                    Console.WriteLine(moves);
                }
                string move = Console.ReadLine();

                if (move == "") break;

                if (!chess.IsValidMove(move)) continue;

                chess = chess.Move(move);

            }
            */


            return;


        }

        static int NextMoves(int step, Chess chess)
        {
            if (step == 0) return 1;

            int count = 0;

            foreach (string moves in chess.YieldValidMoves())
            {
                count += NextMoves(step - 1, chess.Move(moves));
            }

            return count;
        }

        static string ChessToAscii(Chess chess)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  +-----------------+");
            for (int y = 7; y >= 0; y--)
            {
                sb.Append(y + 1);
                sb.Append(" | ");
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(chess.GetFigureAt(x, y) + " ");
                }
                sb.AppendLine("|");
            }
            sb.AppendLine("  +-----------------+");
            sb.AppendLine("    a b c d e f g h ");

            if (chess.IsCheck) sb.AppendLine("IS CHECK");
            if (chess.IsCheckmate) sb.AppendLine("IS CHECKMATE");
            if (chess.IsStalemate) sb.AppendLine("IS STALEMATE");

            return sb.ToString();
        }

        static void Print(string text)
        {
            ConsoleColor old = Console.ForegroundColor;
            foreach (char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = old;
        }
    }
}
