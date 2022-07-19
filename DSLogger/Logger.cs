using DSLogger.Commands;

namespace DSLogger
{
    public class Logger
    {

        public LogFile log { get; private set; }
        CommandManager CmdManager;

        public Logger()
        {
            CmdManager = new CommandManager();
            log = new LogFile("");
        }

        public Logger(string filePath)
        {
            CmdManager = new CommandManager();
            log = new LogFile(filePath);
        }

        public Logger(string filePath, string name)
        {
            CmdManager = new CommandManager();
            log = new LogFile(filePath, name);
        }

        //Returns if file write is successful or not
        public bool WriteToLog(string line)
        {
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

        public bool WriteToDatabase(string _tableName, string line)
        {
            try
            {
                CmdManager.InvokeCommand(new WriteToDatabase(_tableName, line));
                return true;
            }
            catch (System.Exception e) 
            {
                WriteToLog(e.Message);
                WriteToLog(line);
                return false;
            }
        }
    }
}
