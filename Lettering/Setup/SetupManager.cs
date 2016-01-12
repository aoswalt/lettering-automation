using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lettering {
    internal class SetupManager {
        //NOTE(adam): returns true if safe to continue
        internal static bool CheckSetup() {
            bool libInstall = LibraryInstaller.InstallLibrary();
            bool fontInstall = FontChecker.CheckFontInstall();

            string msg = "";
            msg += (libInstall ? "Library had to be updated.\n\n" : "");
            msg += (fontInstall ? "Font(s) missing or need to be updated:\n" + FontChecker.missingFonts : "");
            msg += "\nCorel must be restarted before continuing.";
            msg += "\n\nPlease save all work and press OK to close Corel.";

            if(libInstall || fontInstall) {
                if(Lettering.corel.Visible) {
                    MessageBox.Show(msg, "Corel Restart Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //NOTE(adam): set all documents as clean to prevent error on quit
                    foreach(VGCore.Document document in Lettering.corel.Documents) {
                        document.Dirty = false;
                    }

                    Lettering.corel.Quit();
                }

                if(fontInstall) {
                    //NOTE(adam): open font folder and display message listing needed fonts
                    Process.Start(FontChecker.networkFontFolder);
                    System.Threading.Thread.Sleep(200);     //NOTE(adam): delay to ensure dialog on top of folder window
                    //TODO(adam): messaging
                    MessageBox.Show("Font(s) need to be installed or updated:\n" + FontChecker.missingFonts, "Missing Fonts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;   //NOTE(adam): prevent continuing without fonts installed
                } else {
                    return true;
                }
            } else {
                return true;
            }
        }
    }
}
