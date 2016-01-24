using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VrPlayer.Helpers
{
    public class DiscHelper
    {
        public static IEnumerable<string> GetDiscDrives()
        {
            return DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.CDRom).Select(drive => drive.Name).ToList();
        }
    }
}
