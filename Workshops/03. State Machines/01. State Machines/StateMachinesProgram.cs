namespace _01._State_Machines
{
    using System;
    using System.Collections.Generic;

    public static class StateMachinesProgram
    {
        public static void Main()
        {
            var script = new List<string>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == null)
                {
                    break;
                }

                script.Add(line);
            }

            var scriptRunner = new Expression(string.Join(Environment.NewLine, script));
            scriptRunner.Eval();
        }
    }
}
