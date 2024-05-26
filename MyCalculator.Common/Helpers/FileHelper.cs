using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Common.Helpers
{
    public class FileHelper
    {
        public static void CreateFile(string fileRelativePath)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = directory + fileRelativePath;

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
        }

        public static string ReadAlltextInSelectedFile(string fileRelativePath)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = directory + fileRelativePath;

            return File.ReadAllText(path);
        }

        public static void WriteAlltextInSelectedFile(string text, string fileRelativePath)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = directory + fileRelativePath;

            File.WriteAllText(path, text);
        }
    }
}
