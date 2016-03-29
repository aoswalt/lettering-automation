using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lettering.Errors;

namespace Lettering.Data {
    public class GlobalConfigData {
        public List<string> stylePrefixes = new List<string>();
        public List<string> trims = new List<string>();

        internal void InsertStylePrefix(string prefix) {
            if(stylePrefixes.Contains(prefix)) {
                ErrorHandler.HandleError(ErrorType.Log, $"Config already contains stylePrefix.  prefix: {prefix}");
                return;
            }

            stylePrefixes.Add(prefix);
        }

        internal void InsertTrim(string trim) {
            if(trims.Contains(trim)) {
                ErrorHandler.HandleError(ErrorType.Log, $"Config already contains trim.  trim: {trim}");
                return;
            }

            trims.Add(trim);
        }
    }
}
