namespace TicTacToe
{
    public class GameEdge
    {
        public GameEdge(int row, int column, GameResult result)
        {
            this.Row = row;
            this.Column = column;
            this.Result = result;
        }

        public int Row { get; private set; }

        public int Column { get; private set; }

        public GameResult Result { get; private set; }
    }
}
