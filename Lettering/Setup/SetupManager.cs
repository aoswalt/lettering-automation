using System.Diagnostics;
using Lettering.Data;

namespace Lettering {
    internal class SetupManager {
        //NOTE(adam): returns true if safe to continue
        internal static bool CheckSetupOld(MainWindow mainWindow, bool hasPreviouslyRun) {
            bool libraryInstalled = LibraryInstaller.InstallLibrary();
            string neededFonts = FontChecker.GetNeededFonts(mainWindow);
            bool needFontInstall = neededFonts.Length > 0;

            string msg = "";
            msg += (libraryInstalled ? "Library had to be updated.\n\n" : "");
            msg += (needFontInstall ? "Font(s) missing or need to be updated:\n" + neededFonts : "");
            msg += "\nCorel must be restarted before continuing.";
            msg += "\n\nPlease save all work and press OK to close Corel.";

            if(libraryInstalled || needFontInstall) {
                if(Lettering.corel.Visible) {
                    Messenger.Show(msg, "Corel Restart Required");

                    //NOTE(adam): set all documents as clean to prevent error on quit
                    foreach(VGCore.Document document in Lettering.corel.Documents) {
                        document.Dirty = false;
                    }

                    Lettering.corel.Quit();
                }

                if(needFontInstall) {
                    //NOTE(adam): open font folder and display message listing needed fonts
                    Process.Start(FilePaths.networkFontsPath);
                    System.Threading.Thread.Sleep(200);     //NOTE(adam): delay to ensure dialog on top of folder window
                    Messenger.Show("Font(s) need to be installed or updated:\n" + neededFonts, "Missing Fonts");
                    return false;   //NOTE(adam): prevent continuing without fonts installed
                } else {
                    return true;
                }
            } else {
                return true;
            }
        }


        // ensure the macro library and fonts are set up correctly
        // allow skiping if has been run before
        // takes MainWindow for display centering
        // takes bool for has been previously run
        // returns true if everything is correct

        internal static bool CheckSetup(MainWindow mainWindow, bool hasRunBefore) {
            //store result of installing the library
            bool libraryInstalled = LibraryInstaller.InstallLibrary();
            //store list of needed fonts
            string neededFonts = FontChecker.GetNeededFonts(mainWindow);
            //bool for convenience of need font install
            bool needFontInstall = neededFonts.Length > 0;

            //if either library installed or need fonts
            if(libraryInstalled || needFontInstall) {
                //NOTE(adam): allowing for issues with fonts, etc
                if(hasRunBefore) {
                    //        prompt for ignore errors
                    bool skipChecking = false; //Messenger yes/no dialog result
                    if(skipChecking) {
                        return true;
                    }
                }

                //    if corel is open
                if(Lettering.corel.Visible) {
                    //        if dirty documents are open
                    //            prompt for continuing
                    //            if ok to continue
                    //                close all open documents
                    //            else
                    //                return false
                    //        close corel
                    Lettering.corel.Quit();
                }
            //end if
            }

            //if need fonts
            //    open font folder
            //    show list of needed fonts
            //    return false
            //end if

            //everything is ok so return true
            return true;
        }
    }
}
