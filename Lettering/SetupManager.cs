using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lettering {
    class SetupManager {
        private static string installedLibPath = 
                @"C:\Program Files\Corel\CorelDRAW Graphics Suite X7\Draw\GMS\Automation.gms";
        private static string libPath = @"\\production\Lettering\Corel\WORK FOLDERS\Automation\Automation.gms";
        private static string installedFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        private static string networkFontFolder = @"\\production\Lettering\Corel\WORK FOLDERS\VS Fonts";
        private static string fontFolder = Lettering.tempFolder;
        private static List<string> loadedFonts = new List<string>();
        private static string missingFonts = "";

        // font installer function
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        // font remover function
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int RemoveFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        // returns true if safe to continue
        public static bool CheckSetup() {
            bool libInstall = InstallLibrary();
            bool fontInstall = CheckFontInstall();

            string msg = "";
            msg += (libInstall ? "Library had to be updated.\n\n" : "");
            msg += (fontInstall ? "Font(s) missing or need to be updated:\n" + missingFonts : "");
            msg += "\nCorel must be restarted before continuing.";
            msg += "\n\nPlease save all work and press OK to close Corel.";

            if(libInstall || fontInstall) {
                if(Lettering.corel.Visible) {
                    MessageBox.Show(msg, "Corel Restart Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // set all documents as clean to prevent error on quit
                    foreach(VGCore.Document document in Lettering.corel.Documents) {
                        document.Dirty = false;
                    }

                    Lettering.corel.Quit();
                }

                if(fontInstall) {
                    Process.Start(networkFontFolder);
                    System.Threading.Thread.Sleep(200);     // ensure dialog on top of folder window
                    MessageBox.Show("Font(s) need to be installed or updated:\n" + missingFonts, "Missing Fonts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;   // prevent continuing without fonts installed
                } else {
                    return true;
                }
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
        private static bool CheckFontInstall() {
            bool needFontInstall = false;

            /*
            System.IO.Directory.CreateDirectory(fontFolder);
            string[] files = System.IO.Directory.GetFiles(networkFontFolder);
            foreach(string file in files) {
                System.IO.File.Copy(file, fontFolder + System.IO.Path.GetFileName(file), true);
            }
             * */

            foreach(string fullFontPath in Directory.GetFiles(networkFontFolder, "*.otf")) {
                string fontFileName = Path.GetFileName(fullFontPath);
                List<string> installedFonts = GetInstalledFonts();

                string fontName = GetFontNameFromFile(fullFontPath);
                if(!installedFonts.Contains(fontName)) {
                    // font not installed
                    //MessageBox.Show(fontName + " not installed.");
                    missingFonts += fontName + "\n";
                    needFontInstall = true;
                } else {
                    // font installed, check file date
                    string installedFontFile = GetInstalledFontFileName(fontName);

                    if(installedFontFile == null) {
                        // can't find font file (shouldn't happen)
                        //MessageBox.Show(fontName + " installed file name not found.");
                        missingFonts += fontName + "\n";
                        needFontInstall = true;
                    } else {
                        DateTime installedFontDT = File.GetLastWriteTime(installedFontFolder + "\\" + installedFontFile);
                        DateTime fontDT = File.GetLastWriteTime(fullFontPath);

                        // is saved is newer than installed
                        if(fontDT > installedFontDT) {
                            // font out of date
                            //MessageBox.Show(fontName + " has date difference.\nServer:    " + fontDT + "\nInstalled: " + installedFontDT);
                            missingFonts += fontName + "\n";
                            needFontInstall = true;
                        }
                    }
                }

                /*
                if(needFontInstall) {
                    int result = AddFontResource(fullFontPath);
                    loadedFonts.Add(fullFontPath);
                    int error = Marshal.GetLastWin32Error();

                    if(error != 0 && error != 1400) {
                        MessageBox.Show("Error no: " + error + "\n" + new Win32Exception(error).Message + "\n\n" + fullFontPath);
                    }
                }
                 */
            }

            return needFontInstall;
        }

        // doesn't work if fonts installed on system already
        public static void ReleaseFonts() {
            foreach(string f in loadedFonts) {
                int result;
                while((result = RemoveFontResource(f)) > 0) {
                    MessageBox.Show("Result: " + result + "\n\nRemoved font:\n" + f);
                    int error = Marshal.GetLastWin32Error();

                    if(error != 0 && error != 1400) {
                        MessageBox.Show("Error no: " + error + "\n" + new Win32Exception(error).Message + "\n\n" + f);
                    }
                }
            }
        }

        private static List<string> GetInstalledFonts() {
            /*
            InstalledFontCollection fontsCollection = new InstalledFontCollection();
            System.Drawing.FontFamily[] fontFamilies = fontsCollection.Families;
            List<string> installedFonts = new List<string>();
            foreach(System.Drawing.FontFamily font in fontFamilies) {
                installedFonts.Add(font.Name);
            }

            return installedFonts;
             */

            List<System.Windows.Media.FontFamily> installedFontFamilies = Fonts.SystemFontFamilies.ToList();
            List<string> installedFonts = new List<string>();

            foreach(System.Windows.Media.FontFamily fontFamily in installedFontFamilies) {
                installedFonts.Add(fontFamily.ToString().Split('#')[fontFamily.ToString().Split('#').Count() - 1]);
            }
            return installedFonts;
        }

        private static string GetInstalledFontFileName(string fontName) {
            string regFontName = fontName + " (TrueType)";
            string regFontNameTrim = fontName.Replace(" ", "") + " (TrueType)";  // NOTE(adam): handle FontLab fonts that remove spaces from name
            RegistryKey fonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Fonts", false);
            if(fonts == null) {
                fonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Fonts", false);
                if(fonts == null) {
                    throw new Exception("Can't find font registry database.");
                }
            }

            foreach(string fontKey in fonts.GetValueNames()) {
                if(fontKey == regFontName || fontKey == regFontNameTrim) {
                    return fonts.GetValue(fontKey).ToString();
                }
            }

            return null;
        }

        private static string GetFontNameFromFile(string fontFilePath) {
            foreach(System.Windows.Media.FontFamily fontFamily in Fonts.GetFontFamilies(fontFilePath)) {
                string fontName = fontFamily.ToString().Split('#')[fontFamily.ToString().Split('#').Count() - 1];
                return fontName;
            }
            return "Error with " + System.IO.Path.GetFileName(fontFilePath) + ": " + "No font families found.";

            /*
            PrivateFontCollection fontColl = new PrivateFontCollection();
            try {
                fontColl.AddFontFile(fontFilePath);
                return fontColl.Families[0].Name;
            } catch(FileNotFoundException ex) {
                return "Error with " + System.IO.Path.GetFileName(fontFilePath) + ": " + ex.Message;
            }
             */
        }
    }
}
