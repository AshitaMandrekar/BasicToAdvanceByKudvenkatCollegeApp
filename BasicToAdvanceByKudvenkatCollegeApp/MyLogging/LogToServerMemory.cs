namespace BasicToAdvanceByKudvenkatCollegeApp.MyLogging
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message) // definition of interface
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToServerMemory"); 
        }
    }
}
