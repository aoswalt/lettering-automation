using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lettering {
    //TODO(adam): font processing needs to be cleaned up and try to handle the obscure issues

    internal class FontChecker {
        private static string installedFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        private static string networkFontFolder = @"\\production\Lettering\Corel\WORK FOLDERS\VS Fonts";
        private static string fontFolder = Lettering.tempFolder;
        private static string missingFonts = "";
        private static List<string> loadedFonts = new List<string>();


        //NOTE(adam): returns true if fonts were installed
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
                    //NOTE(adam): font not installed
                    //MessageBox.Show(fontName + " not installed.");
                    missingFonts += fontName + "\n";
                    needFontInstall = true;
                } else {
                    //NOTE(adam): font installed, check file date
                    string installedFontFile = GetInstalledFontFileName(fontName);

                    if(installedFontFile == null) {
                        //NOTE(adam): can't find font file (shouldn't happen)
                        //MessageBox.Show(fontName + " installed file name not found.");
                        missingFonts += fontName + "\n";
                        needFontInstall = true;
                    } else {
                        DateTime installedFontDT = File.GetLastWriteTime(installedFontFolder + "\\" + installedFontFile);
                        DateTime fontDT = File.GetLastWriteTime(fullFontPath);

                        //NOTE(adam): is saved is newer than installed
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
            string regFontNameTrim = fontName.Replace(" ", "") + " (TrueType)";  //NOTE(adam): handle FontLab fonts that remove spaces from name
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
