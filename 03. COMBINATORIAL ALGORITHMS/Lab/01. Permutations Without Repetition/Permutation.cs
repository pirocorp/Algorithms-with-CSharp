namespace _01._Permutations
{
    using System;
    using System.Collections.Generic;

    public class Permutation<T>
    {
        private readonly T[] _elements;
        private bool[] _used;
        private T[] _currentPermutation;

        private readonly IList<T[]> _permutations;

        public Permutation(T[] elements)
        {
            this._elements = elements;
            this._permutations = new List<T[]>();
        }

        public IList<T[]> GetPermutationsRecursive()
        {
            this._used = new bool[this._elements.Length];
            this._currentPermutation = new T[this._elements.Length];

            this.GeneratePermutationRecursive(0);

            return this._permutations;
        }

        public IList<T[]> GetPermutationsSwap()
        {
            this.GeneratePermutationSwap(0);

            return this._permutations;
        }

        public IList<T[]> GetPermutationsWithRepetition()
        {
            this.GetPermutationsWithRepetition(0);

            return this._permutations;
        }

        private void GetPermutationsWithRepetition(int index)
        {
            if (index == this._elements.Length)
            {
                this._permutations.Add(this.ClonePermutation(this._elements));
            }
            else
            {
                var swapped = new HashSet<T>();

                for (var i = index; i < this._elements.Length; i++)
                {
                    if (!swapped.Contains(this._elements[i]))
                    {
                        this.Swap(this._elements, index, i);
                        this.GetPermutationsWithRepetition(index + 1);
                        this.Swap(this._elements, index, i);
                        swapped.Add(this._elements[i]);
                    }
                }

            }
        }

        private void GeneratePermutationSwap(int index)
        {
            if (index == this._elements.Length)
            {
                this._permutations.Add(this.ClonePermutation(this._elements));
            }
            else
            {
                this.GeneratePermutationSwap(index + 1);

                for (var i = index + 1; i < this._elements.Length; i++)
                {
                    this.Swap(this._elements, index, i);
                    this.GeneratePermutationSwap(index + 1);
                    this.Swap(this._elements, index, i);
                }
            }
        }

        private void Swap(T[] collection, int from, int to)
        {
            var temp = collection[from];
            collection[from] = collection[to];
            collection[to] = temp;
        }

        private void GeneratePermutationRecursive(int currentCellIndex)
        {
            if (currentCellIndex == this._currentPermutation.Length)
            {
                this._permutations.Add(this.ClonePermutation(this._currentPermutation));
            }
            else
            {
                for (var i = 0; i < this._elements.Length; i++)
                {
                    var currentElement = this._elements[i];

                    if (!this._used[i])
                    {
                        this._used[i] = true;

                        this._currentPermutation[currentCellIndex] = currentElement;
                        this.GeneratePermutationRecursive(currentCellIndex + 1);

                        this._used[i] = false;
                    }
                }
            }
        }

        private T[] ClonePermutation(T[] source)
        {
            var size = source.Length;
            var clone = new T[size];
            Array.Copy(source, clone, size);
            return clone;
        }
    }
}
