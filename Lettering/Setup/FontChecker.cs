using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using Lettering.Errors;
using Lettering.Data;

namespace Lettering {
    //TODO(adam): font processing needs to be cleaned up and try to handle the obscure issues

    internal class FontChecker {
        private static string installedFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        internal static string networkFontFolder = @"\\production\Lettering\Corel\WORK FOLDERS\VS Fonts";
        internal static string missingFonts = "";
        private static List<string> loadedFonts = new List<string>();


        //NOTE(adam): returns true if fonts were installed
        internal static bool CheckFontInstall() {
            bool needFontInstall = false;
            missingFonts = "";

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
                if(fontName != null && !installedFonts.Contains(fontName)) {
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

            List<FontFamily> installedFontFamilies = Fonts.SystemFontFamilies.ToList();
            List<string> installedFonts = new List<string>();

            foreach(FontFamily fontFamily in installedFontFamilies) {
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
                    ErrorHandler.HandleError(ErrorType.Critical, "Cannot find font registry database for GetInstalledFontFileName.");
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
            foreach(FontFamily fontFamily in Fonts.GetFontFamilies(fontFilePath)) {
                string fontName = fontFamily.ToString().Split('#')[fontFamily.ToString().Split('#').Count() - 1];
                return fontName;
            }
            ErrorHandler.HandleError(ErrorType.Alert, $"Error with {Path.GetFileName(fontFilePath)}: No font families found.");
            return null;

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
