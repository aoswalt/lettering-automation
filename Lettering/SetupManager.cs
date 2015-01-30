using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lettering {
    class SetupManager {
        private static string installedLibPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + 
                @"\Corel\CorelDRAW Graphics Suite X7\Draw\GMS\Automation.gms";
        private static string libPath = @"\\production\lettering\Corel\WORK FOLDERS\Automation\Automation.gms";
        private static string installedFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        private static string fontFolder = @"\\production\lettering\Corel\WORK FOLDERS\VS Fonts";

        // font installer function
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        // returns true if no setup was required
        public static bool CheckSetup() {
            bool libInstall = InstallLibrary();
            bool fontInstall = InstallFonts();

            string msg = "";
            msg += (libInstall ? "Library had to be updated.\n" : "");
            msg += (fontInstall ? "Font(s) had to be updated.\n" : "");
            msg += "\nCorel must be restarted before continuing.";

            if(libInstall || fontInstall) {
                MessageBox.Show(msg, "Corel Restart Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            } else {
                return true;
            }
        }

        // returns true if library was installed
        private static bool InstallLibrary() {
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

        // returns true if fonts were installed
        private static bool InstallFonts() {
            bool needFontInstall = false;

            foreach(string fullFontPath in Directory.GetFiles(fontFolder)) {
                string fontFileName = Path.GetFileName(fullFontPath);
                string installedFontPath = installedFontFolder + '\\' + fontFileName;

                if(!File.Exists(installedFontPath)) {
                    needFontInstall = true;
                } else {
                    DateTime installedFontDT = File.GetLastWriteTime(installedFontPath);
                    DateTime fontDT = File.GetLastWriteTime(fullFontPath);

                    // is saved is newer than installed
                    if(fontDT > installedFontDT) {
                        needFontInstall = true;
                    }
                }

                if(needFontInstall) {
                    // copy to font folder, then install font to current session
                    File.Copy(fullFontPath, installedFontPath, true);

                    int result = AddFontResource(installedFontPath);
                    int error = Marshal.GetLastWin32Error();

                    if(error != 0) {
                        MessageBox.Show(error + "\n" + new Win32Exception(error).Message + "\n\n" + fullFontPath);
                    }
                }
            }

            return needFontInstall;
        }
    }
}
