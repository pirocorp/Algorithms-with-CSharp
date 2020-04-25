namespace _01._String_Searching
{
    public class Hash
    {
        private readonly int _numberBase;
        private readonly long _mod;
        private long _basePower;
        private long _hash;

        public Hash(int numberBase, long mod, string pattern)
            : this(numberBase, mod, pattern, pattern.Length)
        {
        }

        public Hash(int numberBase, long mod, string pattern, int endIndex)
        {
            this._numberBase = numberBase;
            this._mod = mod;
            this._basePower = 1;
            this._hash = 0;
            this.CalculateHash(pattern, endIndex);
        }

        public void Roll(char right, char left)
        {
            this.AddRight(right);
            this.RemoveLeft(left);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Hash;

            return this._hash == other?._hash; 
                   //&& this._mod == other._mod
                   //&& this._numberBase == other._numberBase;
        }

        public override int GetHashCode()
        {
            return this._hash.GetHashCode() ^ this._mod.GetHashCode() ^ this._numberBase;
        }

        private void AddRight(char character)
        {
            this._hash = (this._hash * this._numberBase + (character - 'a')) % this._mod;
        }

        private void RemoveLeft(char character)
        {
            this._hash = ((this._mod + this._hash) - ((character - 'a') * this._basePower) % this._mod) % this._mod;
        }

        private void CalculateHash(string pattern, int endIndex)
        {
            for (var index = 0; index < endIndex; index++)
            {
                var character = pattern[index];
                this.AddRight(character);
                this._basePower = this._basePower * this._numberBase % this._mod;
            }
        }
    }
}
