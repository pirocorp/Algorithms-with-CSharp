namespace _04._Towers_of_Hanoi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TowerOfHanoi
    {
        private readonly Stack<int> _source;
        private readonly Stack<int> _destination;
        private readonly Stack<int> _spare;

        private bool _diskIsMoving;

        public TowerOfHanoi(int count)
        {
            this._source = new Stack<int>(Enumerable.Range(1, count).Reverse());
            this._destination = new Stack<int>();
            this._spare = new Stack<int>();

            this._diskIsMoving = false;
        }

        public int GetDiskFromSource()
        {
            return this.GetDiskFromCollection(this._source);
        }

        public int GetDiskFromDestination()
        {
            return this.GetDiskFromCollection(this._destination);
        }

        public int GetDiskFromSpare()
        {
            return this.GetDiskFromCollection(this._spare);
        }

        public void AddDiskToDestination(int disk)
        {
            this.AddDiskToCollection(this._destination, disk);
        }

        public void AddDiskToSource(int disk)
        {
            this.AddDiskToCollection(this._source, disk);
        }

        public void AddDiskToSpare(int disk)
        {
            this.AddDiskToCollection(this._spare, disk);
        }

        public bool DiskOnTheMove => this._diskIsMoving;

        public IEnumerable<int> SourceRode => this._source;

        public IEnumerable<int> DestinationRode => this._destination;

        public IEnumerable<int> SpareRode => this._spare;

        private void AddDiskToCollection(Stack<int> collection, int disk)
        {
            this.ValidateThereIsMovingDisk();
            this.ValidateDiskSize(collection, disk);

            this._diskIsMoving = false;
            collection.Push(disk);
        }

        private int GetDiskFromCollection(Stack<int> collection)
        {
            this.ValidateThereIsNoMovingDisk();

            this._diskIsMoving = true;
            var disk = collection.Pop();
            return disk;
        }

        private void ValidateThereIsNoMovingDisk()
        {
            if (this._diskIsMoving)
            {
                throw new InvalidOperationException("There is already disk on the move!");
            }
        }

        private void ValidateThereIsMovingDisk()
        {
            if (!this._diskIsMoving)
            {
                throw new InvalidOperationException("There is no moving disk");
            }
        }

        private void ValidateDiskSize(Stack<int> collection, int disk)
        {
            if (collection.Count != 0 && disk > collection.Peek())
            {
                throw new InvalidOperationException("Invalid operation trying to add bigger disk on top of smaller");
            }
        }
    }
}