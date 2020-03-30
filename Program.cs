using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace WMIApp
{
    class Program
    {
        static SystemManager sm = SystemManager.Instance;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Menu();
                MenuSelector(int.Parse(Console.ReadLine())); 
            }
        } //Slut main

        static void Menu()
        {
            List<string> options = new List<string>()
            {
                "Get OS information",
                "Get boot device",
                "Get disk information",
                "Get memory information",
                "Get disk information",
                "Get harddisk serial number",
                "List all services",
                "Get CPU information",
                "Exit application"
            };
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i+1, options[i]);
            }
        }

        static void MenuSelector(int choise)
        {
            Console.Clear();
            switch (choise)
            {
                case 1:
                    GetOSInfo();
                    break;
                case 2:
                    GetBootDevice();
                    break;
                case 3:
                    PopulateDisk();
                    break;
                case 4:
                    GetMemory();
                    break;
                case 5:
                    GetDiskMetadata();
                    break;
                case 6:
                    GetHardDiskSerialNumber();
                    break;
                case 7:
                    ListAllServices();
                    break;
                case 8:
                    GetCPUInfo();
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        static void GetOSInfo()
        {
            foreach (ManagementObject result in sm.GetOSInfo())
            {
                Console.WriteLine("User:\t{0}", result["RegisteredUser"]);
                Console.WriteLine("Org.:\t{0}", result["Organization"]);
                Console.WriteLine("O/S :\t{0}", result["Name"]);
            }

        }

        static void GetBootDevice()
        {
            //enumerate the collection.
            foreach (ManagementObject m in sm.GetBootDevice())
            {
                // access properties of the WMI object
                Console.WriteLine("BootDevice : {0}", m["BootDevice"]);

            }
        }
        static void PopulateDisk()
        {
            foreach (string disk in sm.PopulateDisk())
            {
                Console.WriteLine(disk);
            }
        }

        static void GetMemory()
        {
            foreach (ManagementObject result in sm.GetMemory())
            {
                Console.WriteLine("Total Visible Memory: {0}KB", result["TotalVisibleMemorySize"]);
                Console.WriteLine("Free Physical Memory: {0}KB", result["FreePhysicalMemory"]);
                Console.WriteLine("Total Virtual Memory: {0}KB", result["TotalVirtualMemorySize"]);
                Console.WriteLine("Free Virtual Memory: {0}KB", result["FreeVirtualMemory"]);
            }
        }


        static void GetDiskMetadata()
        {
            foreach (ManagementObject managementObject in sm.GetDiskMetadata())
            {
                Console.WriteLine("Disk Name : " + managementObject["Name"].ToString());
                Console.WriteLine("FreeSpace: " + managementObject["FreeSpace"].ToString());
                Console.WriteLine("Disk Size: " + managementObject["Size"].ToString());
                Console.WriteLine("---------------------------------------------------");
            }
        }

        static void GetHardDiskSerialNumber(string drive = "C")
        {
            Console.WriteLine(sm.GetHardDiskSerialNumber(drive));
        }

        private static void ListAllServices()
        {
            ManagementObjectCollection objectCollection = sm.ListAllServices();
            Console.WriteLine("There are {0} Windows services: ", objectCollection.Count);
            foreach (ManagementObject windowsService in objectCollection)
            {
                PropertyDataCollection serviceProperties = windowsService.Properties;
                foreach (PropertyData serviceProperty in serviceProperties)
                {
                    if (serviceProperty.Value != null)
                    {
                        Console.WriteLine("Windows service property name: {0}", serviceProperty.Name);
                        Console.WriteLine("Windows service property value: {0}", serviceProperty.Value);
                    }
                }
                Console.WriteLine("---------------------------------------");
            }
        }

        static void GetCPUInfo()
        {
            foreach (ManagementObject obj in sm.GetCPUInfo())
            {
                var usage = obj["PercentProcessorTime"];
                var name = obj["Name"];
                Console.WriteLine(name + " : " + usage);
                Console.WriteLine("CPU");
            }
        }
    }

}
