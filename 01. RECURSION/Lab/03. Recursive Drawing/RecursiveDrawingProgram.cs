namespace _03._Recursive_Drawing
{
    using System;

    public static class RecursiveDrawingProgram
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            Draw(n);
        }

        private static void Draw(int count)
        {
            if (count == 0)
            {
                return;
            }

            Console.WriteLine(new string('*', count));

            Draw(count - 1);

            Console.WriteLine(new string('#', count));
        }
    }
}
