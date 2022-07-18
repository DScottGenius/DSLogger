using System;
using System.IO;

namespace DSLogger
{
    public class LogFile
    {
        public string Name { get; set; }
        public string FolderPath { get; set; }

        public string FullPath { get; set; }

        public LogFile(string path)
        {
            Name = $"{GetDateString(DateTime.Now)} - Exception Log.txt";
            FolderPath = path;
            FullPath = $"{path + Name}";
        }
        public LogFile(string path, string name)
        {
            Name = $"{GetDateString(DateTime.Now)} - {name}.txt";
            FolderPath = path;
            FullPath = $"{path + Name}";
        }

        public static string GetDateString(DateTime date)
        {
            return $"{date.Day}-{date.Month}-{date.Year}";
        }

        public bool FileExists()
        {
            return File.Exists(FullPath);
        }
    }
}
