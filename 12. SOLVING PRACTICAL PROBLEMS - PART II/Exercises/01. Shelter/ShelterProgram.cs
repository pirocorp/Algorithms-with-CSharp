namespace _01._Shelter
{
    using System;
    using System.Linq;

    public static class ShelterProgram
    {
        public static void Main()
        {
            var inputArgs = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var soldersCount = inputArgs[0];
            var sheltersCount = inputArgs[1];
            var shelterCapacity = inputArgs[2];


        }
    }
}
