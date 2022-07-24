namespace _01._Trains;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public static class Program
{
    private static double[] arrivals;

    private static double[] departure;

    private static Dictionary<int, (double Arrival, double Departure)> plarforms;

    static Program()
    {
        arrivals = Array.Empty<double>();
        departure = Array.Empty<double>();

        plarforms = new Dictionary<int, (double Arrival, double Departure)>();
    }

    public static void Main()
    {
        ReadInput();

        var result = CalculatePlatforms();

        Console.WriteLine(result);
    }

    private static void ReadInput()
    {
        arrivals = ReadSequenceFromConsole();

        departure = ReadSequenceFromConsole();
    }

    private static double[] ReadSequenceFromConsole()
    {
        return (Console.ReadLine() ?? string.Empty)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .Select(double.Parse)
            .ToArray();
    }

    private static int CalculatePlatforms()
    {
        Array.Sort(arrivals);
        Array.Sort(departure);

        var platforms = 1;
        var result = 1;

        var arrivalIndex  = 1;
        var departureIndex = 0;

        var count = arrivals.Length;

        while (arrivalIndex < count && departureIndex < count)
        {
            if (arrivals[arrivalIndex] < departure[departureIndex])
            {
                platforms++;
                arrivalIndex++;
            }
            else if (arrivals[arrivalIndex] >= departure[departureIndex])
            {
                platforms--;
                departureIndex++;
            }

            if (platforms > result)
            {
                result = platforms;
            }
        }

        return result;
    }
}
