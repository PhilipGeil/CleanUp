using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace WMIApp
{
    class SystemManager
    {
        private static SystemManager instance;

        private SystemManager() { }

        public static SystemManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemManager();
                }
                return instance;
            }
        }

        DalManager dm = DalManager.Instance;

        /// <summary>
        /// Gets the info of the OS, User and Organization
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetOSInfo()
        {
            return dm.GetOSInfo();
        }

        /// <summary>
        /// Retrieves the boot device
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetBootDevice()
        {
            return dm.GetBootDevice();
        }

        /// <summary>
        /// Get's the disk's on the computer and the path to them
        /// </summary>
        /// <returns></returns>
        public List<string> PopulateDisk()
        {
            return dm.PopulateDisk();
        }

        /// <summary>
        /// Get the memory info
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetMemory()
        {
            return dm.GetMemory();
        }

        /// <summary>
        /// Get info from the disk
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetDiskMetadata()
        {
            return dm.GetDiskMetadata();
        }

        /// <summary>
        /// Gets the harddisk serial number
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        public string GetHardDiskSerialNumber(string drive = "C")
        {
            return dm.GetHardDiskSerialNumber(drive);
        }

        /// <summary>
        /// Shows all services
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection ListAllServices()
        {
            return dm.ListAllServices();
        }

        /// <summary>
        /// Get's info about the CPU
        /// </summary>
        /// <returns></returns>
        public ManagementObjectCollection GetCPUInfo()
        {
            return dm.GetCPUInfo();
        } 
    }
}
