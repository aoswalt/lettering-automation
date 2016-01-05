using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering {
    internal class LibraryInstaller {
        private static string installedLibPath = @"C:\Program Files\Corel\CorelDRAW Graphics Suite X7\Draw\GMS\Automation.gms";
        private static string libPath = @"\\production\Lettering\Corel\WORK FOLDERS\Automation\Automation.gms";

        //NOTE(adam): returns true if library was installed
        internal static bool InstallLibrary() {
            bool needLibInstall = false;

            if(!File.Exists(installedLibPath)) {
                needLibInstall = true;
            } else {
                DateTime installedLibDT = File.GetLastWriteTime(installedLibPath);
                DateTime libDT = File.GetLastWriteTime(libPath);

                // is saved is newer than installed
                needLibInstall = (libDT > installedLibDT);
            }

            if(needLibInstall) {
                File.Copy(libPath, installedLibPath, true);
            }

            return needLibInstall;
        }
    }
}
