using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
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
            //msg += (fontInstall ? "Font(s) had to be updated.\n" : "");
            msg += "\nCorel must be restarted before continuing.";
            msg += "\n\nPlease save all work and press OK to close Corel.";

            if(libInstall && Lettering.corel.Visible) {
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
                List<string> installedFonts = GetInstalledFonts();

                string fontName = GetFontNameFromFile(fontFileName);
                string installedFontFile = "";
                if(!installedFonts.Contains(fontName)) {
                    // font not installed
                    needFontInstall = true;
                } else {
                    // font installed, check file date
                    installedFontFile = GetInstalledFontFileName(fontName);

                    if(installedFontFile == null) {
                        needFontInstall = true;
                    } else {
                        DateTime installedFontDT = File.GetLastWriteTime(installedFontFile);
                        DateTime fontDT = File.GetLastWriteTime(fullFontPath);

                        // is saved is newer than installed
                        if(fontDT > installedFontDT) {
                            needFontInstall = true;
                        }
                    }
                }
                string newInstalledFontFileName = installedFontFile.Length > 0 ? installedFontFile.Insert(installedFontFile.Length - 4, "_") :
                                                                                 fontFileName.Insert(fontFileName.Length - 4, "_");
                string newInstalledFontPath = installedFontFolder + '\\' + newInstalledFontFileName;

                if(needFontInstall) {
                    // cannot install fonts if corel is open
                    if(Lettering.corel.Visible) {
                        MessageBox.Show("Fonts must be updated. Please save all documents and press OK to close Corel.", "Font Update Required");

                        // set all documents as clean to prevent error on quit
                        foreach(VGCore.Document document in Lettering.corel.Documents) {
                            document.Dirty = false;
                        }

                        Lettering.corel.Quit();
                    }

                    // copy to font folder, then install font to current session
                    File.Copy(fullFontPath, newInstalledFontPath, true);

                    int result = AddFontResource(newInstalledFontPath);
                    int error = Marshal.GetLastWin32Error();

                    if(error != 0) {
                        MessageBox.Show(error + "\n" + new Win32Exception(error).Message + "\n\n" + fullFontPath);
                    } else {
                        // add to registry
                        RegistryKey key = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Fonts");
                        key.SetValue(fontName + " (TrueType)", Path.GetFileName(newInstalledFontPath));
                        key.Close();
                    }
                }
            }

            return needFontInstall;
        }

        private static List<string> GetInstalledFonts() {
            InstalledFontCollection fontsCollection = new InstalledFontCollection();
            FontFamily[] fontFamilies = fontsCollection.Families;
            List<string> installedFonts = new List<string>();
            foreach(FontFamily font in fontFamilies) {
                installedFonts.Add(font.Name);
            }

            return installedFonts;
        }

        private static string GetInstalledFontFileName(string fontName) {
            string regFontName = fontName + " (TrueType)";
            RegistryKey fonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Fonts", false);
            if(fonts == null) {
                fonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Fonts", false);
                if(fonts == null) {
                    throw new Exception("Can't find font registry database.");
                }
            }

            foreach(string fontKey in fonts.GetValueNames()) {
                if(fontKey == regFontName) {
                    return fonts.GetValue(fontKey).ToString();
                }
            }
            return null;
        }

        private static string GetFontNameFromFile(string fontFilePath) {
            PrivateFontCollection fontColl = new PrivateFontCollection();
            fontColl.AddFontFile(fontFilePath);
            return fontColl.Families[0].Name;
        }
    }
}
