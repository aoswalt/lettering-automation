using System.Diagnostics;
using Lettering.Data;

namespace Lettering {
    internal class SetupManager {
        /*
        ensure the macro library and fonts are set up correctly
        allow skiping if has been run before
        takes MainWindow for display centering
        takes bool for has been previously run
        returns true if everything is correct
        */

        //NOTE(adam): returns true if safe to continue
        internal static bool CheckMacroSetup(MainWindow mainWindow, bool hasRunBefore) {
            bool libraryInstalled = LibraryInstaller.InstallLibrary();
            string neededFonts = FontChecker.GetNeededFonts(mainWindow);
            bool needFontInstall = neededFonts.Length > 0;
            
            if(libraryInstalled || needFontInstall) {
                //NOTE(adam): allowing for issues with fonts, etc
                if(hasRunBefore) {
                    string skipMessage = "";
                    skipMessage += "Setup was checked previously, but a problem was found again.\n";
                    skipMessage += "Ignore problem?\n";
                    skipMessage += "\n";
                    skipMessage += "WARNING: Only click Yes if you are sure everything is correct!";
                    bool skipChecking = Messenger.Show(skipMessage, "Skip Checking?", MessageButtons.YesNo);
                    if(skipChecking) {
                        return true;
                    }
                }
                
                if(Lettering.corel.Visible) {
                    //NOTE(adam): is at least one dirty document is open
                    int docs = Lettering.corel.Documents.Count;
                    int doci = 0;
                    bool dirtyFound = false;
                    while(!dirtyFound && (doci < docs)) {
                        if(Lettering.corel.Documents[doci].Dirty) {
                            dirtyFound = true;
                        }
                    }
                    
                    if(!dirtyFound) {
                        string closePromptMessage = "";
                        closePromptMessage += (libraryInstalled ? "The macro library was updated.\n" : "");
                        closePromptMessage += (needFontInstall ? "Fonts need to be installed.\n" : "");
                        closePromptMessage += "\n";
                        closePromptMessage += "A Corel restart is required, but unsaved documents were found.\n";
                        closePromptMessage += "Please save any unsaved documents and press Ok to continue.";

                        bool isOkayToCloseDocs = Messenger.Show(closePromptMessage, "Restart Required", MessageButtons.OkCancel);
                        if(isOkayToCloseDocs) {
                            while(Lettering.corel.Documents.Count > 0) {
                                Lettering.corel.ActiveDocument.Dirty = false;
                                Lettering.corel.ActiveDocument.Close();
                            }
                        } else {
                            return false;
                        }
                    }
                    Lettering.corel.Quit();
                }
            }

            //if need fonts
            if(needFontInstall) {
                //NOTE(adam): open font folder and display message listing needed fonts
                Process.Start(Lettering.Config.Setup.FilePaths.NetworkFontsFolderPath);
                System.Threading.Thread.Sleep(200);     //NOTE(adam): delay to ensure dialog on top of folder window
                Messenger.Show($"Font(s) need to be installed or updated:\n{neededFonts}", "Needed Fonts");
                return false;
            }

            //NOTE(adam): everything is ok so return true
            return true;
        }
    }
}
