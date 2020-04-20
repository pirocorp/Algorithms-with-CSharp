namespace _03._Black_Messup
{
    using System;

    public class Atom : IComparable<Atom>
    {
        public Atom(string name, int mass, int decay)
        {
            this.Name = name;
            this.Mass = mass;
            this.Decay = decay;
        }

        public string Name { get; }

        public int Mass { get; }

        public int Decay { get; }

        public int CompareTo(Atom other)
        {
            return -this.Mass.CompareTo(other.Mass);
        }

        public override string ToString()
        {
            return $"{this.Name}, Mass: {this.Mass}, Decay: {this.Decay}";
        }
    }
}
