namespace _02._Fast_and_Furious
{
    using System;

    public class Time : IComparable<Time>
    {
        public Time(int hours, int minutes, int seconds)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public int Hours { get; }

        public int Minutes { get; }

        public int Seconds { get; }

        public decimal GetHoursInterval(Time otherTime)
        {
            var thisTimeInterval = this.Hours * 3600M + this.Minutes * 60M + this.Seconds;
            var otherTimeInterval = otherTime.Hours * 3600M + otherTime.Minutes * 60M + otherTime.Seconds;
            var intervalInSeconds = Math.Abs(thisTimeInterval - otherTimeInterval);

            return intervalInSeconds / 3600M;
        }

        public int CompareTo(Time other)
        {
            var cmp = this.Hours.CompareTo(other.Hours);

            if (cmp == 0)
            {
                cmp = this.Minutes.CompareTo(other.Minutes);
            }

            if (cmp == 0)
            {
                cmp = this.Seconds.CompareTo(other.Seconds);
            }

            return cmp;
        }

        public override string ToString()
        {
            return $"{this.Hours}:{this.Minutes}:{this.Seconds}";
        }
    }
}
