namespace _02._Processor_Scheduling
{
    public class Task
    {
        public Task(int number, int value, int deadline)
        {
            this.Number = number;
            this.Value = value;
            this.Deadline = deadline;
        }

        public int Number { get; }

        public int Value { get; }

        public int Deadline { get; }

        public override string ToString()
        {
            return string.Format($"Number: {this.Number}, Value: {this.Value}, Deadline: {this.Deadline}");
        }
    }
}
