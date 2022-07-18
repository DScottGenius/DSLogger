using System;
using System.IO;
using System.Threading.Tasks;

namespace DSLogger.Commands
{
    class WriteToNewFile : IWriteFileCommand
    {
        public string FilePath;
        public string TextToWrite { get; set; }
        public WriteToNewFile(string pathIn, string textToWrite)
        {
            FilePath = pathIn;
            TextToWrite = textToWrite;
        }

        public bool CanExecute()
        {
            if (string.IsNullOrEmpty(TextToWrite) || string.IsNullOrEmpty(FilePath))
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
            await WriteFile(FilePath, TextToWrite);
        }

        private static async Task WriteFile(string pathToWrite, string textToWrite)
        {
            string line = $"{DateTime.Now.TimeOfDay} - {textToWrite}";
            await File.WriteAllTextAsync(pathToWrite, line);
        }
    }
}
