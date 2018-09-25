using System;

namespace ChessRules
{
    class FigureMoving
    {
        public Figure figure { get; private set; }
        public Square from { get; private set; }
        public Square to { get; private set; }
        public Figure promotion { get; private set; }

        public FigureMoving(FigureOnSquare fs, Square to, Figure promotion = Figure.none)
        {
            this.figure = fs.figure;
            this.from = fs.square;
            this.to = to;
            this.promotion = promotion;
        }

        public FigureMoving(string move) //Pe2e4, Pe7e8Q
                                         //01234  012345
        {
            this.figure = (Figure)move[0];
            this.from = new Square(move.Substring(1, 2));
            this.to = new Square(move.Substring(3, 2));
            if (move.Length == 6)
                this.promotion = (Figure)move[5];
            else
                this.promotion = Figure.none;
        }

        public override string ToString()
        {
            return ((char)figure).ToString() +
                from.Name +
                to.Name;
        }

        public int DeltaX { get { return to.x - from.x; } }
        public int DeltaY { get { return to.y - from.y; } }

        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }
        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }
    }
}
