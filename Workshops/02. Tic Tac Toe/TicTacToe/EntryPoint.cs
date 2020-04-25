namespace TicTacToe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using playerFunc = System.Func<GameState, System.Tuple<int, int>>;

    public static class EntryPoint
    {
        private static GameResult BuildTree(Dictionary<GameState, GameEdge> tree, 
            GameState state, GameCell currentPlayer, GameCell otherPlayer)
        {
            if (tree.ContainsKey(state))
            {
                return tree[state].Result;
            }

            if (state.IsWinning(otherPlayer))
            {
                return GameResult.Losing;
            }

            GameEdge drawEdge = null;
            GameEdge looseEdge = null;

            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    var newState = state.MakeMove(row, col, currentPlayer);

                    if (newState == null)
                    {
                        continue;
                    }

                    var result = BuildTree(tree, newState, otherPlayer, currentPlayer);

                    switch (result)
                    {
                        case GameResult.Losing:
                            tree[state] = new GameEdge(row, col, GameResult.Winning);
                            return GameResult.Winning;
                        case GameResult.Draw:
                            if (drawEdge == null || new Random().Next() % 10 == 0)
                            {
                                drawEdge = new GameEdge(row, col, GameResult.Draw);
                            };
                            break;
                        case GameResult.Winning:
                            if (looseEdge == null || new Random().Next() % 10 == 0)
                            {
                                looseEdge = new GameEdge(row, col, GameResult.Losing);
                            }
                            break;
                    }
                }
            }

            if (drawEdge != null)
            {
                tree[state] = drawEdge;
                return GameResult.Draw;
            }

            if (looseEdge != null)
            {
                tree[state] = looseEdge;
                return GameResult.Losing;
            }

            return GameResult.Draw;
        }

        private static Tuple<int, int> KeyboardPlayer(GameState state)
        {
            Console.WriteLine(state);
            Console.Write("Choose your destiny: ");
            var coordinates = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            if (coordinates.Length != 2)
            {
                return new Tuple<int, int>(-1, -1);
            }

            return new Tuple<int, int>(coordinates[0], coordinates[1]);
        }

        private static Tuple<int, int> CpuPlayer(Dictionary<GameState, GameEdge> tree, GameState state)
        {
            var edge = tree[state];
            return new Tuple<int, int>(edge.Row, edge.Column);
        }

        private static int GamePlay(playerFunc p1Func, playerFunc p2Func)
        {
            var state = new GameState();
            var pFuncs = new playerFunc[] { p1Func, p2Func };
            var players = new GameCell[] {GameCell.Player1, GameCell.Player2};

            var currentPlayer = 0;
            var movesCount = 0;

            while (movesCount < 9)
            {
                var move = pFuncs[currentPlayer](state);
                var newState = state.MakeMove(move.Item1, move.Item2, players[currentPlayer]);

                if (newState == null)
                {
                    if (movesCount == 0)
                    {
                        Console.WriteLine($"Skipping...");
                        currentPlayer ^= 1;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move!");
                    }

                    continue;
                }

                state = newState;

                if (state.IsWinning(players[currentPlayer]))
                {
                    Console.WriteLine(state);
                    return currentPlayer + 1;
                }

                currentPlayer ^= 1;
                ++movesCount;
            }

            Console.WriteLine(state);
            return 0;
        }

        public static void Main()
        {
            var gameTree = new Dictionary<GameState, GameEdge>();

            BuildTree(gameTree, new GameState(), GameCell.Player1, GameCell.Player2);

            for (int i = 0; i < 10; i++)
            {
                var result = GamePlay(KeyboardPlayer, state => CpuPlayer(gameTree, state));

                switch (result)
                {
                    case 0:
                        Console.WriteLine("It's a draw.");
                        break;
                    case 1:
                    case 2:
                        Console.WriteLine($"Player {result} wins.");
                        break;

                    default:
                        throw new ArgumentException("Something is seriously wrong Exception");
                }
            }
        }
    }
}
