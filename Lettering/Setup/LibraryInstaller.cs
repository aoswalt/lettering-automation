using System;
using System.IO;
using Lettering.Data;
using Lettering.Errors;

namespace Lettering {
    internal class LibraryInstaller {
        //NOTE(adam): returns true if library was installed
        internal static bool InstallLibrary() {
            bool needLibInstall = false;

            if(!File.Exists(FilePaths.installedLibraryFilePath)) {
                needLibInstall = true;
            } else {
                DateTime installedLibDT = File.GetLastWriteTime(FilePaths.installedLibraryFilePath);
                DateTime networkLibDT = File.GetLastWriteTime(FilePaths.networkLibraryFilePath);

                //NOTE(adam): is network file newer than installed
                needLibInstall = (networkLibDT > installedLibDT);
            }

            if(needLibInstall) {
                try {
                    //NOTE(adam): library can be updated with Corel running, but won't apply until restarted
                    File.Copy(FilePaths.networkLibraryFilePath, FilePaths.installedLibraryFilePath, true);
                } catch(IOException ex) {
                    ErrorHandler.HandleError(ErrorType.Critical, $"IO Exception at installing library.\n{ex.Message}");
                    return false;
                } catch(Exception ex) {
                    ErrorHandler.HandleError(ErrorType.Critical, $"Unhandled Exception at installing library.\n{ex.Message}");
                    return false;
                }
            }

            return needLibInstall;
        }
    }
}
