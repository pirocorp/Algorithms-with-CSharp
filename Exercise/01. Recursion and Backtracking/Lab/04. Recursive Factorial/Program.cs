namespace _04._Recursive_Factorial;

using System;

public class Program
{
    public static void Main(string[] args)
    {
        var result = GetFactorial(int.Parse(Console.ReadLine() ?? "0"));
        Console.WriteLine(result);
    }

    private static long GetFactorial(int num)
    {
        if (num == 0)
        {
            return 1;
        } 
   
        return num * GetFactorial(num - 1);
    } 
}
