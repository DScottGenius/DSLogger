namespace DSLogger.Commands
{
    interface IWriteFileCommand
    {
        string TextToWrite { get; set; }

        bool CanExecute();
        void Execute();
    }
}