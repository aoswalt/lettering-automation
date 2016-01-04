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
    internal class SetupManager {
        //NOTE(adam): returns true if safe to continue
        internal static bool CheckSetup() {
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

                    //NOTE(adam): set all documents as clean to prevent error on quit
                    foreach(VGCore.Document document in Lettering.corel.Documents) {
                        document.Dirty = false;
                    }

                    Lettering.corel.Quit();
                }

                if(fontInstall) {
                    Process.Start(networkFontFolder);
                    System.Threading.Thread.Sleep(200);     //NOTE(adam): delay to ensure dialog on top of folder window
                    MessageBox.Show("Font(s) need to be installed or updated:\n" + missingFonts, "Missing Fonts", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
