namespace BasicToAdvanceByKudvenkatCollegeApp.MyLogging
{
    public class LogToFile :IMyLogger
    {
        public void Log(string message) // definition of interface
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToFile");
        }
    }
}
