using System;
using System.IO;
using System.Threading.Tasks;

namespace DSLogger.Commands
{
    class WriteToExistingFile : IWriteFileCommand
    {
        public string TextToWrite { get; set; }
        public string FilePath;

        public WriteToExistingFile(string filePath, string textToWrite)
        {
            TextToWrite = textToWrite;
            FilePath = filePath;
        }

        public bool CanExecute()
        {
            if (string.IsNullOrEmpty(TextToWrite) || string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async void Execute()
        {
            await AppendToFile(FilePath, TextToWrite);
        }

        private static async Task AppendToFile(string filepath, string textToWrite)
        {
            string line = $"{DateTime.Now.TimeOfDay} - {textToWrite}";
            using StreamWriter streamWriter = new StreamWriter(filepath, append: true);
            await streamWriter.WriteAsync($"{Environment.NewLine}{line}");
        }
    }
}
