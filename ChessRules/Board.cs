using System.Collections.Generic;

namespace ChessRules
{
    class Board
    {
        public string fen { get; protected set; }

        protected Figure[,] figures;
        public Color moveColor { get; protected set; }
        public bool canCastleA1 { get; protected set; } // Q
        public bool canCastleH1 { get; protected set; } // K
        public bool canCastleA8 { get; protected set; } // q
        public bool canCastleH8 { get; protected set; } // k
        public Square enpassant { get; protected set; }
        public int drawNumber { get; protected set; }
        public int moveNumber { get; protected set; }



        public Board(string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        public Board Move(FigureMoving fm)
        {
            return new NextBoard(fen, fm);
        }

        public IEnumerable<FigureOnSquare> YieldMyFigureOnSquares()
        {
            foreach (Square square in Square.YieldBoardSquares())
            {
                if (GetFigureAt(square).GetColor() == moveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
                return figures[square.x, square.y];
            return Figure.none;
        }

        void Init()
        {
            //   SetFigureAt(new Square("a1"), Figure.whiteKing);
            //   SetFigureAt(new Square("h8"), Figure.blackKing);
            //   moveColor = Color.white;


            // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            // 0                                           1 2    3 4 5

            string[] parts = fen.Split();
            InitFigures(parts[0]);
            InitMoveColors(parts[1]);
            InitCastleFlags(parts[2]);
            InitEnpassant(parts[3]);
            InitDrawNumber(parts[4]);
            InitMoveNumber(parts[5]);
        }

        private void InitFigures(string str)
        {
            for (int i = 8; i >= 2; i--)
            {
                str = str.Replace(i.ToString(), (i - 1).ToString() + "1");
            }
            str = str.Replace('1', (char)Figure.none);
            string[] lines = str.Split('/');
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    figures[x, y] = (Figure)lines[7 - y][x];
                }
            }
        }

        private void InitMoveColors(string str)
        {
            moveColor = (str == "b") ? Color.black : Color.white;
        }

        private void InitCastleFlags(string str)
        {
            canCastleA1 = str.Contains("Q");
            canCastleH1 = str.Contains("K");
            canCastleA8 = str.Contains("q");
            canCastleH8 = str.Contains("k");
        }

        private void InitEnpassant(string str)
        {
            enpassant = new Square(str);
        }

        private void InitDrawNumber(string str)
        {
            drawNumber = int.Parse(str);
        }

        private void InitMoveNumber(string str)
        {
            moveNumber = int.Parse(str);
        }
    }
}
