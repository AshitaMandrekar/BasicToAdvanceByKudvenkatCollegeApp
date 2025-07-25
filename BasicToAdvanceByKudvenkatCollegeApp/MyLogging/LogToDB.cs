namespace BasicToAdvanceByKudvenkatCollegeApp.MyLogging
{
    public class LogToDB : IMyLogger
    {
        public void Log(string message) // definition of interface
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToDB");
        }
    }
}
