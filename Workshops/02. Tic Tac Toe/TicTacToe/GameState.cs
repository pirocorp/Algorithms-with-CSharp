namespace TicTacToe
{
    using System;

    public class GameState
    {
        private const int BOARD_SIZE = 3;
        private readonly GameCell[,] _board = new GameCell[BOARD_SIZE, BOARD_SIZE];

        public GameState()
        {
            for (var row = 0; row < BOARD_SIZE; row++)
            {
                for (var col = 0; col < BOARD_SIZE; col++)
                {
                    this._board[row, col] = GameCell.Empty;
                }
            }
        }

        public GameState(GameState origin)
        {
            for (var row = 0; row < BOARD_SIZE; row++)
            {
                for (var col = 0; col < BOARD_SIZE; col++)
                {
                    this._board[row, col] = origin._board[row, col];
                }
            }
        }

        public GameState MakeMove(int row, int col, GameCell player)
        {
            if (row < 0 || row > 2 || col < 0 || col > 2)
            {
                return null;
            }

            if (this._board[row, col] != GameCell.Empty)
            {
                return null;
            }

            var newState = new GameState(this);
            newState._board[row, col] = player;
            return newState;
        }

        public bool IsWinning(GameCell player)
        {
            var diag1Winning = true;
            var diag2Winning = true;

            for (var row = 0; row < BOARD_SIZE; row++)
            {
                var rowWinning = true;
                var colWinning = true;

                if (this._board[row, row] != player)
                {
                    diag1Winning = false;
                }

                if (this._board[row, BOARD_SIZE - 1 - row] != player)
                {
                    diag2Winning = false;
                }

                for (var col = 0; col < BOARD_SIZE; col++)
                {
                    if (this._board[row, col] != player)
                    {
                        rowWinning = false;
                    }

                    if (this._board[col, row] != player)
                    {
                        colWinning = false;
                    }
                }

                if (rowWinning || colWinning)
                {
                    return true;
                }
            }

            return diag1Winning || diag2Winning;
        }

        public override int GetHashCode()
        {
            var hash = 0;

            for (var row = 0; row < BOARD_SIZE; row++)
            {
                for (var col = 0; col < BOARD_SIZE; col++)
                {
                    hash = hash * BOARD_SIZE + (int)this._board[row, col];
                }
            }

            return hash;
        }

        public override bool Equals(object? obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return $@"{GetSymbol(this._board[0, 0])} {GetSymbol(this._board[0, 1])} {GetSymbol(this._board[0, 2])}
{GetSymbol(this._board[1, 0])} {GetSymbol(this._board[1, 1])} {GetSymbol(this._board[1, 2])}
{GetSymbol(this._board[2, 0])} {GetSymbol(this._board[2, 1])} {GetSymbol(this._board[2, 2])}
";
        }

        private static char GetSymbol(GameCell cell)
        {
            switch (cell)
            {
                case GameCell.Empty:
                    return ' ';
                case GameCell.Player1:
                    return 'X';
                case GameCell.Player2:
                    return 'O';
                default:
                    throw new ArgumentOutOfRangeException(nameof(cell), cell, null);
            }
        }
    }
}
