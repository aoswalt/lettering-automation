using System.Collections.Generic;
using System.IO;

namespace Lettering.IO {
    class TextWriter {
        internal static void CreateFile(string filePath) {
            if(File.Exists(filePath)) {
                File.Delete(filePath);
            }

            File.Create(filePath).Dispose();
        }

        internal static void AppendFile(string filePath, string text) {
            using(StreamWriter writer = new StreamWriter(filePath, true)) {
                writer.WriteLine(text);
                writer.Close();
            }
        }

        internal static void AppendFile(string filePath, List<string> textList) {
            using(StreamWriter writer = new StreamWriter(filePath, true)) {
                foreach(string line in textList) {
                    writer.WriteLine(line);
                }

                writer.Close();
            }
        }
    }
}
