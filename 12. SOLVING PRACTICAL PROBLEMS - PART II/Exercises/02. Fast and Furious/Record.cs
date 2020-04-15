namespace _02._Fast_and_Furious
{
    using System;

    public class Record : IComparable<Record>
    {
        public Record(string town, string plate, Time time)
        {
            this.Town = town;
            this.Plate = plate;
            this.Time = time;
        }

        public string Town { get; }

        public string Plate { get; }

        public Time Time { get; }

        public int CompareTo(Record other)
        {
            return this.Time.CompareTo(other.Time);
        }

        public override string ToString()
        {
            return $"{this.Town} {this.Plate} {this.Time}";
        }
    }
}
