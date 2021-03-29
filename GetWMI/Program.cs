using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace GetWMI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Write("Input command: ");
                string command = Console.ReadLine();

                if (command == "")
                    running = false;
                else
                {
                    try
                    {
                        foreach (var col in GetWMIValue(command))
                            foreach (var mo in col.Properties)
                                Console.WriteLine($"{mo.Name}: {mo.Value}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine();
                }
            }
        }

        private static ManagementObjectCollection GetWMIValue(string wminame)
        {
            var scope = new ManagementScope("");
            scope.Connect();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {wminame}");
            searcher.Scope = scope;
            return searcher.Get();
        }
    }
}
