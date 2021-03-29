using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace GetInfo
{
    class Program
    {
        private static readonly string outputFileName = "ComputerInfo.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("    _______________________________");
            Console.WriteLine("   |  ___________________________  |");
            Console.WriteLine("   | |                           | |");
            Console.WriteLine("   | |           GetInfo         | |");
            Console.WriteLine("   | |     Made by Stiig Gade!   | |");
            Console.WriteLine("   | |___________________________| |");
            Console.WriteLine("   |_______________________________|");
            Console.WriteLine();

            Console.Write("Henter info fra computeren... ");

            ComputerInfo ci;

            try
            {
                ci = new ComputerInfo
                {
                    Username = Environment.UserName,
                    ComputerName = Environment.MachineName,
                    Model = GetWMIValue("Win32_ComputerSystem", "Model"),
                    OS = GetWMIValue("Win32_OperatingSystem", "Caption"),
                    SerialNumber = GetWMIValue("Win32_BIOS", "SerialNumber"),
                    CPU = GetWMIValue("Win32_Processor", "Name"),
                    CPUCores = Int32.Parse(GetWMIValue("Win32_Processor", "NumberOfCores")),
                    RAMGB = (int)(Int64.Parse(GetWMIValue("Win32_PhysicalMemory", "Capacity")) / 1073741824),
                    DiskType = (!GetWMIValue("Win32_OperatingSystem", "Caption").Contains("7")) ? (DiskType)Int32.Parse(GetWMIValue("MSFT_PhysicalDisk", "MediaType", @"\\.\root\microsoft\windows\storage")) : DiskType.Unspecified,
                    //DiskSize = (int)(Int64.Parse(GetWMIValue("MSFT_PhysicalDisk", "Size", @"\\.\root\microsoft\windows\storage")) / 1073741824),
                    DiskSize = (int)((DriveInfo.GetDrives()).FirstOrDefault(i => i.Name == @"C:\").TotalSize / 1073741824),
                    GFX = GetWMIValue("Win32_DisplayConfiguration", "DeviceName"),
                    TeamViewerId = GetTeamViewerId()
                };
            }
            catch (Exception e)
            {
                ci = null;

                Console.WriteLine("Der skete en fejl!");
                Console.WriteLine(e.Message);
                Console.WriteLine("Tryk på en tast for at afslutte...");
                Console.ReadKey();
            }

            if (ci != null)
            {
                Console.WriteLine("OK!");
                Console.WriteLine();

                Console.Write("Myndighed: ");
                var myn = Console.ReadLine().ToUpper();

                Console.Write("Bygning: ");
                var byg = Console.ReadLine().ToUpper();

                Console.Write("Rum: ");
                var lok = Console.ReadLine().ToUpper();

                Console.Write("Ejer: ");
                var ejer = Console.ReadLine().ToUpper();

                Console.WriteLine();
                Console.WriteLine("-------------------Info-------------------");
                Console.WriteLine($"MYN:          {myn}");
                Console.WriteLine($"BYG:          {byg}");
                Console.WriteLine($"LOK:          {lok}");
                Console.WriteLine($"Ejer:         {ejer}");
                Console.WriteLine($"Serialnumber: {ci.SerialNumber}");
                Console.WriteLine($"Username:     {ci.Username}");
                Console.WriteLine($"ComputerName: {ci.ComputerName}");
                Console.WriteLine($"Model:        {ci.Model}");
                Console.WriteLine($"OS:           {ci.OS}");
                Console.WriteLine($"CPU:          {ci.CPU}");
                Console.WriteLine($"CPUCores:     {ci.CPUCores}");
                Console.WriteLine($"RAMGB:        {ci.RAMGB}");
                Console.WriteLine($"DiskType:     {ci.DiskType}");
                Console.WriteLine($"DiskSize:     {ci.DiskSize}");
                Console.WriteLine($"GFX:          {ci.GFX}");
                Console.WriteLine($"TeamViewerId: {ci.TeamViewerId}");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine();

                bool save = GetUserConfirmation("Gem?");

                while (save)
                {
                    try
                    {
                        if (!File.Exists(outputFileName))
                            File.WriteAllText(outputFileName, "Timestamp;MYN;BYG;LOK;Ejer;Serialnumber;Username;ComputerName;Model;OS;CPU;CPUCores;RAMGB;DiskType;DiskSize;GFX;TeamViewerId");

                        File.AppendAllText(outputFileName, $"{Environment.NewLine}{DateTime.Now};{myn};{byg};{lok};{ejer};{ci}");

                        save = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Der skete en fejl!");
                        Console.WriteLine(ex.Message);
                        save = GetUserConfirmation("Prøv igen?");
                    }
                }
            }
        }

        private static bool GetUserConfirmation(string message)
        {
            bool? confirmation = null;

            while (confirmation == null)
            {
                Console.Write($"{message} (j/n): ");

                var pressedKey = Console.ReadKey();

                Console.WriteLine();

                if (pressedKey.Key == ConsoleKey.J)
                    confirmation = true;
                else if (pressedKey.Key == ConsoleKey.N)
                    confirmation = false;
                else
                    Console.WriteLine("Forkert input, prøv igen!\n");
            }

            return confirmation.Value;
        }

        private static string GetTeamViewerId()
        {
            object key = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\TeamViewer\", "ClientID", "");
            if (key == null)
                return "TeamViewer ikke installeret";
            else if (key.ToString() == "")
                return "Key 'ClientID' findes ikke";
            else
                return key.ToString();
        }

        private static string GetWMIValue(string wminame, string property, string scope = "")
        {
            try
            {
                var sc = new ManagementScope(scope);
                sc.Connect();

                var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {wminame}");
                searcher.Scope = sc;

                var name = (from x in searcher.Get().Cast<ManagementObject>()
                            select x.GetPropertyValue(property)).FirstOrDefault();
                return name != null ? name.ToString() : "Unknown";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

    class ComputerInfo
    {
        public string Username { get; set; }
        public string ComputerName { get; set; }
        public string Model { get; set; }
        public string OS { get; set; }
        public string SerialNumber { get; set; }
        public string CPU { get; set; }
        public int CPUCores { get; set; }
        public int RAMGB { get; set; }
        public DiskType DiskType { get; set; }
        public int DiskSize { get; set; }
        public string GFX { get; set; }
        public string TeamViewerId { get; set; }

        public override string ToString()
        {
            return $"{SerialNumber};{Username};{ComputerName};{Model};{OS};{CPU};{CPUCores};{RAMGB};{DiskType};{DiskSize};{GFX};{TeamViewerId}";
        }
    }

    enum DiskType
    {
        Unspecified = 0,
        HDD = 3,
        SSD = 4,
        SCM = 5
    }
}