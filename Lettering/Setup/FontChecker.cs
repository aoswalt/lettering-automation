using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using Lettering.Data;
using Lettering.Errors;
using Lettering.Forms;
using Microsoft.Win32;

namespace Lettering {
    internal class FontChecker {
        //NOTE(adam): returns string of needed fonts
        internal static string GetNeededFonts(MainWindow mainWindow) {
            string neededFonts = "";
            RegistryKey registryFonts = GetRegistryFonts();

            List<string> installedFontNames = new List<string>();
            foreach(FontFamily fontFamily in Fonts.SystemFontFamilies.ToList()) {
                installedFontNames.Add(fontFamily.ToString().Split('#')[fontFamily.ToString().Split('#').Count() - 1]);
            }

            string networkFontsPath = Lettering.Config.Setup.FilePaths.NetworkFontsFolderPath;
            string[] networkFontFiles = null;
            if(Directory.Exists(networkFontsPath)) {
                networkFontFiles = Directory.GetFiles(networkFontsPath, "*.otf");
            } else {
                ErrorHandler.HandleError(ErrorType.Critical, $"Could not find network font folder at \n{networkFontsPath}");
                return "";
            }

            FontCheckingWindow fontCheckingWindow = new FontCheckingWindow();
            fontCheckingWindow.Show();
            fontCheckingWindow.Location = new System.Drawing.Point(mainWindow.Location.X + (mainWindow.Width - fontCheckingWindow.Width) / 2,
                                                                   mainWindow.Location.Y + (mainWindow.Height - fontCheckingWindow.Height) / 2);

            //TODO(adam): investigate why it takes time to check each font (may be slow because of remote connection)
            for(int i = 0; i != networkFontFiles.Length; ++i) {
                string networkFontFile = networkFontFiles[i];

                if(fontCheckingWindow != null) {
                    fontCheckingWindow.SetFontsProgress(Path.GetFileName(networkFontFile), i + 1, networkFontFiles.Length);
                }

                string networkFontName = GetFontNameFromFile(networkFontFile);

                if(networkFontName != null && !installedFontNames.Contains(networkFontName)) {
                    //NOTE(adam): font not installed
                    ErrorHandler.HandleError(ErrorType.Log, $"Font not installed: {networkFontFile}");
                    neededFonts += networkFontName + '\n';
                    continue;
                }
                
                string installedFontFileName = GetInstalledFontFileName(registryFonts, networkFontName);

                if(installedFontFileName == null) {
                    //NOTE(adam): can't find installed font file (shouldn't happen)
                    ErrorHandler.HandleError(ErrorType.Log, $"Font missing from registry: {networkFontFile}");
                    neededFonts += networkFontName + '\n';
                    continue;
                }

                DateTime installedFontDateTime = File.GetLastWriteTime(FilePaths.installedFontsFolderPath + '\\' + installedFontFileName);
                DateTime networkFontDateTime = File.GetLastWriteTime(networkFontFile);

                if(networkFontDateTime > installedFontDateTime) {
                    //NOTE(adam): network font newer than installed font
                    ErrorHandler.HandleError(ErrorType.Log, $"Font out of date: {installedFontFileName}");
                    neededFonts += networkFontName + '\n';
                }
            }

            fontCheckingWindow.Close();
            return neededFonts;
        }

        private static RegistryKey GetRegistryFonts() {
            RegistryKey registryFonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Fonts", false);
            if(registryFonts == null) {
                registryFonts = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Fonts", false);
                if(registryFonts == null) {
                    ErrorHandler.HandleError(ErrorType.Critical, "Cannot find font registry database for GetRegistryFonts.");
                }
            }

            return registryFonts;
        }

        private static string GetInstalledFontFileName(RegistryKey registryFonts, string fontName) {
            string fontNameTT = fontName + " (TrueType)";
            string fontNameTrimmedTT = fontName.Replace(" ", "") + " (TrueType)";   //NOTE(adam): handle FontLab fonts that remove spaces from name

            foreach(string registryFontName in registryFonts.GetValueNames()) {
                if(registryFontName == fontNameTT || registryFontName == fontNameTrimmedTT) {
                    return registryFonts.GetValue(registryFontName).ToString();
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
        }
    }
}
