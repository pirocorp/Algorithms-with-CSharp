namespace _01._Activities
{
    public class Activity
    {
        public Activity(int startTime, int endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public int StartTime { get; set; }

        public int EndTime { get; set; }
    }
}
