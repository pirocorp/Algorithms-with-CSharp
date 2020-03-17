namespace _04._Best_Lectures_Schedule
{
    public class Lecture
    {
        public Lecture(string name, int startTime, int endTime)
        {
            this.Name = name;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public string Name { get; }

        public int StartTime { get; }

        public int EndTime { get; }

        public override string ToString()
        {
            return $"Name: {this.Name}, Start: {this.StartTime}, End: {this.EndTime}";
        }
    }
}
