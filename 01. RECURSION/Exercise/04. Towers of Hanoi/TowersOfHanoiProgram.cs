namespace _04._Towers_of_Hanoi
{
    using System;

    public static class TowersOfHanoiProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var tower = new TowerOfHanoi(n);
            var solution = new TowerOfHanoiSolution(tower);
            solution.Solve();
        }
    }
}
