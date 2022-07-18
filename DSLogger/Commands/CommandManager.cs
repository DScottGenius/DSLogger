namespace DSLogger.Commands
{
    class CommandManager
    {

        public void InvokeCommand(IWriteFileCommand command)
        {
            if (command.CanExecute())
            {
                command.Execute();
            }
        }

    }
}
