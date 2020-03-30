using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace WMIApp
{
    class DalManager
    {
        // Create a private instance of DalManager in order to use the class as Singleton
        private static DalManager instance;

        private DalManager() { }
        /// <summary>
        /// Instance of DalManager
        /// </summary>
        public static DalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DalManager();
                }
                return instance;
            }
        }
        /// <summary>
        /// Gets the info of the OS, User and Organization
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetOSInfo()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();
            return results;
        }
        /// <summary>
        /// Retrieves the boot device
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetBootDevice()
        {
            ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");

            //create object query
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

            //create object searcher
            ManagementObjectSearcher searcher =
                                    new ManagementObjectSearcher(scope, query);

            //get a collection of WMI objects
            ManagementObjectCollection queryCollection = searcher.Get();
            return queryCollection;
        }
        /// <summary>
        /// Get's the disk's on the computer and the path to them
        /// </summary>
        /// <returns></returns>
        public List<string> PopulateDisk()
        {
            List<string> disk = new List<string>();
            SelectQuery selectQuery = new SelectQuery("Win32_LogicalDisk");
            ManagementObjectSearcher mnagementObjectSearcher = new ManagementObjectSearcher(selectQuery);
            foreach (ManagementObject managementObject in mnagementObjectSearcher.Get())
            {
                disk.Add(managementObject.ToString());
            }
            return disk;
        }
        /// <summary>
        /// Get the memory info
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetMemory()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();
            return results;
        }
        /// <summary>
        /// Get info from the disk
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetDiskMetadata()
        {
            ManagementScope managementScope = new ManagementScope();
            ObjectQuery objectQuery = new ObjectQuery("select FreeSpace,Size,Name from Win32_LogicalDisk where DriveType=3");
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
            ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
            return managementObjectCollection;
        }
        /// <summary>
        /// Gets the harddisk serial number
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        public string GetHardDiskSerialNumber(string drive = "C")
        {
            ManagementObject managementObject = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + drive + ":\"");
            managementObject.Get();
            return managementObject["VolumeSerialNumber"].ToString();
        }
        /// <summary>
        /// Shows all services
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection ListAllServices()
        {
            ManagementObjectSearcher windowsServicesSearcher = new ManagementObjectSearcher("root\\cimv2", "select * from Win32_Service");
            ManagementObjectCollection objectCollection = windowsServicesSearcher.Get();
            return objectCollection;
        }
        /// <summary>
        /// Get's info about the CPU
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetCPUInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            return searcher.Get();
        }
    }
}
