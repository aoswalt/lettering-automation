using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lettering.Data {
    //NOTE(adam): must be publics for JsonConvert
    //NOTE(adam): cannot be structs because need to allow editing
    public class JsonConfigData {
        public Data_Setup Setup;
        public Dictionary<string, Data_Path> Paths;
    }

    public class Data_Setup {
        public Data_FilePaths FilePaths;
        public List<string> Prefixes;
        public List<Data_Trim> Trims;
        public List<Data_Export> Exports;
        public Dictionary<string, Data_TypeData> TypeData;
        public Dictionary<int, string> PathRules;
    }

    public class Data_FilePaths {
        public string NetworkFontsFolder;
        public string LibraryNetworkFile;
        public string LibraryInstalledFile;
    }

    public class Data_Trim {
        public string _Comment;
        public string Pattern;
    }

    public class Data_Export {
        public string StyleRegex;
        public string FileType;     //TODO(adam): use enum? use cdr enum value?
    }

    public class Data_TypeData {
        public string Root;
        public string Extension;
    }

    //public class Data_PathRule {
    //    public int Id;
    //    public string Rule;
    //}

    public class Data_Path {
        public Data_StyleData Cut;
        public Data_StyleData Sew;
        public Data_StyleData Stone;
    }

    public class Data_StyleData {
        public int Rule;
        public List<int> WordOrder;
        public string MirroredStyle;
        public List<Data_Exception> Exceptions;
    }

    public class Data_Exception {
        public string Path;
        public List<string> Conditions;
    }
}


/*
setup
    file_paths
        network_fonts_folder
        library_network_file
        library_installed_file
    prefixes []
    trims []
        _comment
        regex pattern
    exports []
        style
        type
    type_data []
        type
        root
        extension
    path_rules []
        id
        rule
paths []
    style
    cut/sew/stone
        rule
        word_order []
        mirrored_style
        exceptions []
            path
            conditions []
*/