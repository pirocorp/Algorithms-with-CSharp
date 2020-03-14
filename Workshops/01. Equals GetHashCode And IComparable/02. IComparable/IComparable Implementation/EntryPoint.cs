namespace IComparable_Implementation
{
    using System;
    using IComparable;

    public static class EntryPoint
    {
        public static void Main()
        {
            var pianos = new Piano[]
            {
                new Piano() { Mark = 3.2F },
                new GrandPiano() { Mark = 3.2F, Producer = "Petzold" },
                new Piano() { Mark = 3.4F },
                new PrivatePiano() { Mark = 3.2F, Owner = "Joe" },
                new GrandPiano() { Mark = 3.2F, Producer = "Schimmel" },
                new PrivatePiano() { Mark = 3.4F, Owner = "Henry" }
            };

            Array.Sort<Piano>(pianos);

            foreach (var piano in pianos)
            {
                Console.WriteLine(piano);
            }
        }
    }
}
