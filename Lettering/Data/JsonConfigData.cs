﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lettering.Data {
    //NOTE(adam): must be publics for JsonConvert
    //NOTE(adam): cannot be structs because need to allow editing
    public class JsonConfigData {
        public Data_Setup Setup;
        public SortedDictionary<string, Data_Style> Styles;
    }

    public class Data_Setup {
        public Data_FilePaths FilePaths;
        public List<StringData> StylePrefixes;
        public List<Data_Trim> Trims;
        public List<Data_Export> Exports;
        public SortedDictionary<string, Data_TypeData> TypeData;  //TODO(adam): change key to enum
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
        public StringData Path { get; set; }
        public List<string> Conditions;
    }

    //NOTE(adam): string wrapper class for public accessors & mutators
    public class StringData {
        public string Value { get; set; }
        public StringData() { }
        public StringData(string s) { Value = s; }
        public static implicit operator string(StringData s) { return s.Value; }
        public static implicit operator StringData(string s) { return new StringData(s); }
        public override string ToString() { return Value; }
    }

    //TODO(adam): cutom JsonSerializer class(es)?
}
