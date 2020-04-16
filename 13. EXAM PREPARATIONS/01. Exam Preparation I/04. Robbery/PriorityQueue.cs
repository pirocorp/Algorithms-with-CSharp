namespace _04._Robbery
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly Dictionary<T, int> _searchCollection;
        private readonly List<T> _heap;

        public PriorityQueue()
        {
            this._heap = new List<T>();
            this._searchCollection = new Dictionary<T, int>();
        }

        public int Count => this._heap.Count;

        public bool Contains(T element)
        {
            return this._searchCollection.ContainsKey(element);
        }

        public T PeakMin()
        {
            return this._heap[0];
        }

        public void Enqueue(T element)
        {
            this._searchCollection.Add(element, this._heap.Count);
            this._heap.Add(element);
            this.HeapifyUp(this._heap.Count - 1);
        }

        public T ExtractMin()
        {
            var min = this._heap[0];
            var last = this._heap[this._heap.Count - 1];

            this._searchCollection[last] = 0;
            this._heap[0] = last;
            this._heap.RemoveAt(this._heap.Count - 1);

            if (this._heap.Count > 0)
            {
                this.HeapifyDown(0);
            }

            this._searchCollection.Remove(min);
            return min;
        }

        public void DecreaseKey(T element)
        {
            var index = this._searchCollection[element];
            this.HeapifyUp(index);
        }

        private void Swap(int from, int to)
        {
            T old = this._heap[from];
            this._searchCollection[old] = to;
            this._searchCollection[this._heap[to]] = from;
            this._heap[from] = this._heap[to];
            this._heap[to] = old;
        }

        private void HeapifyDown(int i)
        {
            var left = (2 * i) + 1;
            var right = (2 * i) + 2;
            var smallest = i;

            if (left < this._heap.Count
                && this._heap[left].CompareTo(this._heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < this._heap.Count
                && this._heap[right].CompareTo(this._heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                this.Swap(i, smallest);
                this.HeapifyDown(smallest);
            }
        }

        private void HeapifyUp(int i)
        {
            var parent = (i - 1) / 2;

            while (i > 0 && this._heap[i].CompareTo(this._heap[parent]) < 0)
            {
                this.Swap(i, parent);

                i = parent;
                parent = (i - 1) / 2;
            }
        }
    }
}
