using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lettering.Data {
    //NOTE(adam): must be publics for JsonConvert
    //NOTE(adam): cannot be structs because need to allow editing
    public class JsonConfigData {
        public Data_Setup Setup;
        public Dictionary<string, Data_Style> Styles;
    }

    public class Data_Setup {
        public Data_FilePaths FilePaths;
        public List<StringData> StylePrefixes;
        public List<Data_Trim> Trims;
        public List<Data_Export> Exports;
        public Dictionary<string, Data_TypeData> TypeData;  //TODO(adam): change key to enum
        public List<Data_PathRule> PathRules;
    }

    public class Data_FilePaths {
        public string NetworkFontsFolderPath { get; set; }
        public string NetworkLibraryFilePath { get; set; }
        public string InstalledLibraryFilePath { get; set; }
    }

    public class Data_Trim {
        public string Pattern { get; set; }
        public string _Comment { get; set; }
    }

    public class Data_Export {
        public string StyleRegex { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ExportType FileType { get; set; }    //TODO(adam): use cdr enum value?
    }

    public class Data_TypeData {
        public string Root { get; set; }
        public string Extension { get; set; }
    }

    public class Data_PathRule {
        public string Id { get; set; }
        public string Rule { get; set; }
    }

    public class Data_Style {
        public Data_StyleData Cut;
        public Data_StyleData Sew;
        public Data_StyleData Stone;
    }

    public class Data_StyleData {
        public string Rule;
        public List<int> CustomWordOrder;
        public string MirroredStyle;
        public List<Data_Exception> Exceptions;
    }

    public class Data_Exception {
        public string Path;
        public List<string> Conditions;
    }

    public class StringData {
        public string Value { get; set; }
        public StringData() { }
        public StringData(string s) { Value = s; }
        public static implicit operator string(StringData s) { return s.Value; }
        public static implicit operator StringData(string s) { return new StringData(s); }
    }

    //TODO(adam): cutom JsonSerializer class(es)?
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