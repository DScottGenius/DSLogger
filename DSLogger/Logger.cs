using DSLogger.Commands;

namespace DSLogger
{
    public class Logger
    {

        public LogFile log { get; private set; }
        CommandManager CmdManager;

        public Logger()
        {
            log = new LogFile("");
        }

        public Logger(string filePath)
        {
            log = new LogFile(filePath);
        }

        public Logger(string filePath, string name)
        {
            log = new LogFile(filePath, name);
        }

        //Returns if file write is successful or not
        public bool WriteToLog(string line)
        {
            CmdManager = new CommandManager();

            try
            {
                if (log.FileExists())
                {
                    CmdManager.InvokeCommand(new WriteToExistingFile(log.FullPath, line));
                    return true;
                }
                else
                {
                    CmdManager.InvokeCommand(new WriteToNewFile(log.FullPath, line));
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
