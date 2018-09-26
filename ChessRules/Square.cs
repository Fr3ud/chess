using System.Collections.Generic;

namespace ChessRules
{
    struct Square
    {
        public static Square none = new Square(-1, -1);

        public int x
        {
            get;
            private set;
        }

        public int y
        {
            get;
            private set;
        }

        public Square(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Square(string name) //e2, g5, h8 ...
        {
            if (name.Length == 2 &&
                name[0] >= 'a' && name[0] <= 'h' &&
                name[1] >= '1' && name[1] <= '8')
            {
                x = name[0] - 'a'; // 'a' - 'a' = 0; 'b' - 'a' = 1 ...
                y = name[1] - '1';
            }
            else
            {
                this = none;
            }
        }

        public string Name
        {
            get
            {
                if (OnBoard())
                {
                    return ((char)('a' + x)).ToString() +
                            (y + 1).ToString();
                }
                else
                {
                    return "-";
                }
            }
        }

        public bool OnBoard()
        {
            return (x >= 0 && x < 8) &&
                    (y >= 0 && y < 8);
        }

        public static IEnumerable<Square> YieldBoardSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }
    }
}
