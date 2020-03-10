namespace _04._Towers_of_Hanoi
{
    using System;
    using System.Linq;

    public class TowerOfHanoiSolution
    {
        private readonly TowerOfHanoi _tower;
        private int _step;

        private readonly Action<int> _addToSource;
        private readonly Action<int> _addToDestination;
        private readonly Action<int> _addToSpare;

        private readonly Func<int> _getFromSource;
        private readonly Func<int> _getFromDestination;
        private readonly Func<int> _getFromSpare;

        public TowerOfHanoiSolution(TowerOfHanoi tower)
        {
            this._tower = tower;
            this._step = 0;

            this._addToSource += this.AddToSourceAction;
            this._addToDestination += this.AddToDestinationAction;
            this._addToSpare += this.AddToSpareAction;

            this._getFromSource += this.GetFromSource;
            this._getFromDestination += this.GetFromDestination;
            this._getFromSpare += this.GetFromSpare;
        }

        public void Solve()
        {
            Console.WriteLine($"Source: {string.Join(", ", this._tower.SourceRode.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", this._tower.DestinationRode.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", this._tower.SpareRode.Reverse())}");
            Console.WriteLine();

            var n = this._tower.SourceRode.Count();

            this.Move(n, this._getFromSource, this._addToSource, 
                this._getFromSpare, this._addToSpare, 
                this._getFromDestination, this._addToDestination);
        }

        private void Move(int n,
            Func<int> getFromSource, Action<int> addToSource,
            Func<int> getFromSpare, Action<int> addToSpare,
            Func<int> getFromDestination, Action<int> addToDestination)
        {
            if (n == 1)
            {
                this.MoveDisk(getFromSource, addToDestination);
            }
            else
            {
                this.Move(n - 1, getFromSource, addToSource,
                    getFromDestination, addToDestination,
                    getFromSpare, addToSpare);

                this.MoveDisk(getFromSource, addToDestination);

                this.Move(n - 1, getFromSpare, addToSpare,
                    getFromSource, addToSource,
                    getFromDestination, addToDestination);
            }
        }

        private void MoveDisk(Func<int> getFromSource, Action<int> addToDestination)
        {
            var disk = getFromSource();
            addToDestination(disk);
            this._step++;
            this.PrintRods();
        }

        private void PrintRods()
        {
            Console.WriteLine($"Step #{this._step}: Moved disk");
            Console.WriteLine($"Source: {string.Join(", ", this._tower.SourceRode.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", this._tower.DestinationRode.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", this._tower.SpareRode.Reverse())}");
            Console.WriteLine();
        }

        private void AddToSourceAction(int disk)
        {
            this._tower.AddDiskToSource(disk);
        }

        private void AddToDestinationAction(int disk)
        {
            this._tower.AddDiskToDestination(disk);
        }

        private void AddToSpareAction(int disk)
        {
            this._tower.AddDiskToSpare(disk);
        }

        private int GetFromSource()
        {
            return this._tower.GetDiskFromSource();
        }

        private int GetFromDestination()
        {
            return this._tower.GetDiskFromDestination();
        }

        private int GetFromSpare()
        {
            return this._tower.GetDiskFromSpare();
        }
    }
}
