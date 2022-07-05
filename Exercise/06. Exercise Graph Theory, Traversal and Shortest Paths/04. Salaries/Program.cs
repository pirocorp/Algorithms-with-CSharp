namespace _04._Salaries;

using System;
using System.Linq;

public static class Program
{
    private static bool[][] graph;

    private static int[] salaries;

    static Program()
    {
        graph = Array.Empty<bool[]>();

        salaries = Array.Empty<int>();
    }

    public static void Main()
    {
        ReadInput();

        CalculateSalaries();

        PrintOutput();
    }

    private static void ReadInput()
    {
        var n = int.Parse(Console.ReadLine() ?? "0");

        graph = new bool[n][];
        salaries = new int[n];

        for (var i = 0; i < n; i++)
        {
            graph[i] = (Console.ReadLine() ?? string.Empty)
                .Select(x => x == 'Y')
                .ToArray();
        }
    }

    private static void PrintOutput()
        => Console.WriteLine(salaries.Sum());

    private static void CalculateSalaries()
    {
        for (var i = 0; i < salaries.Length; i++)
        {
            if (salaries[i] != 0)
            {
                continue;
            }

            salaries[i] = CalculateSalary(i);
        }
    }

    private static int CalculateSalary(int i)
    {
        if (salaries[i] != 0)
        {
            return salaries[i];
        }

        if (graph[i].Sum(x => x ? 1 : 0) == 0)
        {
            return 1;
        }

        var sum = 0;
        var row = graph[i];

        for (var j = 0; j < row.Length; j++)
        {
            if (!row[j])
            {
                continue;
            }

            salaries[j] = CalculateSalary(j);
            sum += salaries[j];
        }

        return sum;
    }
}
